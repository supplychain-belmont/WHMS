using Indotalent.Applications.InventoryTransactions;
using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.Products;
using Indotalent.Applications.SalesOrderItems;
using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Entities;
using Indotalent.Models.Enums;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.DeliveryOrders
{
    public class DeliveryOrderService : Repository<DeliveryOrder>
    {
        private readonly NumberSequenceService _numberSequenceService;
        private readonly SalesOrderItemService _salesOrderItemService;
        private readonly InventoryTransactionService _inventoryTransactionService;
        private readonly AssemblyProductService _assemblyProductService;

        public DeliveryOrderService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            SalesOrderItemService salesOrderItemService,
            InventoryTransactionService inventoryTransactionService,
            AssemblyProductService assemblyProductService) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
            _salesOrderItemService = salesOrderItemService;
            _inventoryTransactionService = inventoryTransactionService;
            _assemblyProductService = assemblyProductService;
        }

        public override async Task AddAsync(DeliveryOrder? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(DeliveryOrder), "", "DO");
            await base.AddAsync(entity);

            var salesItems = await _salesOrderItemService.GetAll()
                .Include(item => item.Product)
                .Where(item => item.SalesOrderId == entity!.SalesOrderId)
                .ToListAsync();

            var assemblyProductIds = salesItems
                .Where(item => item.Product!.IsAssembly)
                .Select(item => item.ProductId)
                .ToList();

            var assemblyProducts = await _assemblyProductService
                .GetAll()
                .Include(ap => ap.Product)
                .Where(ap => assemblyProductIds.Contains(ap.AssemblyId))
                .ToListAsync();

            foreach (SalesOrderItem salesOrderItem in salesItems)
            {
                if (salesOrderItem.Product!.IsAssembly)
                {
                    var productsFromAssembly = assemblyProducts
                        .Where(ap => ap.AssemblyId == salesOrderItem.ProductId)
                        .ToList();

                    if (productsFromAssembly.Count == 0) continue;

                    foreach (AssemblyProduct assemblyProduct in productsFromAssembly)
                    {
                        var assemblyTransaction = await CreateTransaction(assemblyProduct.ProductId,
                            assemblyProduct.Quantity * salesOrderItem.Quantity, entity);
                        await _inventoryTransactionService.AddAsync(assemblyTransaction);
                    }

                    continue;
                }

                var transaction = await CreateTransaction(salesOrderItem.ProductId, salesOrderItem.Quantity, entity);
                await _inventoryTransactionService.AddAsync(transaction);
            }
        }

        private Task<InventoryTransaction> CreateTransaction(int productId, decimal quantity,
            DeliveryOrder entity)
        {
            return Task.FromResult(new InventoryTransaction
            {
                WarehouseId = 1,
                ProductId = productId,
                ModuleId = entity.Id,
                ModuleName = nameof(DeliveryOrder),
                ModuleCode = "DO",
                ModuleNumber = entity.Number ?? string.Empty,
                MovementDate = entity.DeliveryDate!.Value,
                Status = (InventoryTransactionStatus)entity.Status!,
                RequestedMovement = quantity,
                Movement = quantity,
                Number = _numberSequenceService.GenerateNumber(nameof(InventoryTransaction), "", "IVT")
            });
        }

        public override async Task UpdateAsync(DeliveryOrder? entity)
        {
            await base.UpdateAsync(entity);
            await RecalculateChildAsync(entity?.Id);
        }

        private async Task RecalculateChildAsync(int? masterId)
        {
            var master = await _context.Set<DeliveryOrder>()
                .Include(x => x.SalesOrder)
                .ThenInclude(x => x!.Customer)
                .Where(x => x.Id == masterId && x.IsNotDeleted)
                .FirstOrDefaultAsync();

            var children = await _inventoryTransactionService
                .GetAll()
                .Where(x => x.ModuleId == masterId && x.ModuleName == nameof(DeliveryOrder))
                .ToListAsync();

            if (master != null)
            {
                foreach (var item in children)
                {
                    item.Status = (InventoryTransactionStatus)master.Status!;
                    await _inventoryTransactionService.UpdateAsync(item);
                }
            }
        }
    }
}
