using Indotalent.Applications.InventoryTransactions;
using Indotalent.Applications.NumberSequences;
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

        public DeliveryOrderService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            SalesOrderItemService salesOrderItemService, InventoryTransactionService inventoryTransactionService) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
            _salesOrderItemService = salesOrderItemService;
            _inventoryTransactionService = inventoryTransactionService;
        }

        public override async Task AddAsync(DeliveryOrder? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(DeliveryOrder), "", "DO");
            await base.AddAsync(entity);

            var salesItems = await _salesOrderItemService.GetAll()
                .Where(item => item.SalesOrderId == entity!.SalesOrderId)
                .ToListAsync();

            var transactions = salesItems
                .Select(item => new InventoryTransaction()
                {
                    WarehouseId = 1,
                    ProductId = item.ProductId,
                    ModuleId = entity.Id,
                    ModuleName = nameof(DeliveryOrder),
                    ModuleCode = "DO",
                    ModuleNumber = entity.Number ?? string.Empty,
                    MovementDate = entity.DeliveryDate!.Value,
                    Status = (InventoryTransactionStatus)entity.Status!,
                    RequestedMovement = item.Quantity,
                    Movement = 0,
                    Number = _numberSequenceService.GenerateNumber(nameof(InventoryTransaction), "", "IVT")
                }).ToList();

            foreach (var transaction in transactions)
            {
                await _inventoryTransactionService.AddAsync(transaction);
            }
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
                .Where(x => x.ModuleId == masterId)
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
