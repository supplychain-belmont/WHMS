using Indotalent.Applications.Lots;
using Indotalent.Applications.Products;
using Indotalent.Applications.PurchaseOrders;
using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Contracts;
using Indotalent.Models.Entities;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.PurchaseOrderItems
{
    public class PurchaseOrderItemService : Repository<PurchaseOrderItem>
    {
        private readonly PurchaseOrderService _purchaseOrderService;
        private readonly ProductService _productService;
        private readonly AssemblyProductService _assemblyProductService;
        private readonly LotService _lotService;

        public PurchaseOrderItemService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            PurchaseOrderService purchaseOrderService,
            ProductService productService,
            AssemblyProductService assemblyProductService,
            LotService lotService) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _purchaseOrderService = purchaseOrderService;
            _productService = productService;
            _assemblyProductService = assemblyProductService;
            _lotService = lotService;
        }

        public override async Task AddAsync(PurchaseOrderItem? entity)
        {
            if (entity != null)
            {
                if (entity is IHasAudit auditEntity && !string.IsNullOrEmpty(_userId))
                {
                    auditEntity.CreatedAtUtc = DateTime.Now;
                    auditEntity.CreatedByUserId = _userId;
                }

                var order = await _purchaseOrderService.GetAll()
                    .Where(x => x.Id == entity.PurchaseOrderId)
                    .Select(x => new { x.ContainerM3, x.TotalAgencyCost, x.TotalTransportContainerCost })
                    .FirstOrDefaultAsync();
                var product = await _productService.GetAll()
                    .Where(x => x.Id == entity.ProductId)
                    .FirstOrDefaultAsync();

                if (product is { IsAssembly: true })
                {
                    entity.IsAssembly = true;
                    entity.AssemblyId = product.Id;
                    entity.ShowOrderItem = true;
                    var entityEntry = _context.Set<PurchaseOrderItem>().Add(entity);
                    await _context.SaveChangesAsync();

                    await CreateAssemblyProductChildAsync(product, entityEntry.Entity);
                    return;
                }

                entity.ShowOrderItem = entity.AssemblyId == null;
                entity.RecalculateWeightedM3(product!.M3, order!.ContainerM3);
                entity.RecalculateTransportCost(order!.TotalTransportContainerCost, order.TotalAgencyCost);
                entity.RecalculateTotal();
                _context.Set<PurchaseOrderItem>().Add(entity);
                await _context.SaveChangesAsync();

                await _purchaseOrderService.RecalculateParentAsync(entity.PurchaseOrderId);
            }
            else
            {
                throw new Exception("Unable to process, entity is null");
            }
        }

        private async Task CreateAssemblyProductChildAsync(Product product, PurchaseOrderItem purchaseOrderItem)
        {
            var purchaseOrderItemChild = await _assemblyProductService.GetAll()
                .Where(x => x.AssemblyId == product.Id)
                .Select(x =>
                    new PurchaseOrderItem
                    {
                        CreatedAtUtc = DateTime.Now,
                        ProductId = x.Product!.Id,
                        UnitCost = 0m,
                        UnitCostBrazil = 0m,
                        PurchaseOrderId = purchaseOrderItem.PurchaseOrderId,
                        UnitCostBolivia = 0m,
                        AssemblyId = purchaseOrderItem.Id,
                        Quantity = x.Quantity * purchaseOrderItem.Quantity,
                        Summary = x.Product!.Number,
                        ShowOrderItem = false,
                    })
                .ToListAsync();

            foreach (PurchaseOrderItem orderItem in purchaseOrderItemChild)
            {
                await AddAsync(orderItem);
            }
        }

        public override async Task UpdateAsync(PurchaseOrderItem? entity)
        {
            if (entity != null)
            {
                if (entity is IHasAudit auditEntity && !string.IsNullOrEmpty(_userId))
                {
                    auditEntity.UpdatedByUserId = _userId;
                }

                if (entity is IHasAudit auditedEntity)
                {
                    auditedEntity.UpdatedAtUtc = DateTime.Now;
                }

                var order = await _purchaseOrderService.GetAll()
                    .Where(x => x.Id == entity.PurchaseOrderId)
                    .Select(x => new { x.ContainerM3, x.TotalAgencyCost, x.TotalTransportContainerCost })
                    .FirstOrDefaultAsync();
                var product = await _productService.GetAll()
                    .Where(x => x.Id == entity.ProductId)
                    .Select(x => new { x.M3, x.IsAssembly })
                    .FirstOrDefaultAsync();

                if (product is { IsAssembly: true })
                {
                    await UpdateAssemblyProductChildAsync(entity);
                    return;
                }

                entity.RecalculateWeightedM3(product!.M3, order!.ContainerM3);
                entity.RecalculateTransportCost(order.TotalTransportContainerCost, order.TotalAgencyCost);
                entity.RecalculateTotal();
                _context.Set<PurchaseOrderItem>().Update(entity);
                await _context.SaveChangesAsync();

                await _purchaseOrderService.RecalculateParentAsync(entity.PurchaseOrderId);
            }
            else
            {
                throw new Exception("Unable to process, entity is null");
            }
        }

        private async Task UpdateAssemblyProductChildAsync(PurchaseOrderItem parent)
        {
            var purchaseOrderItemChild = await GetAll()
                .Where(poi => poi.AssemblyId == parent.Id)
                .AsNoTracking()
                .ToListAsync();

            foreach (PurchaseOrderItem purchaseOrderItem in purchaseOrderItemChild)
            {
                var assemblyProduct = await _assemblyProductService
                    .GetAll()
                    .Where(ap => ap.AssemblyId == parent.AssemblyId && ap.ProductId == purchaseOrderItem.ProductId)
                    .FirstOrDefaultAsync();
                purchaseOrderItem.Quantity = (assemblyProduct?.Quantity ?? 1) * parent.Quantity;
                purchaseOrderItem.ShowOrderItem = false;
                await UpdateAsync(purchaseOrderItem);
            }

            _context.Set<PurchaseOrderItem>().Update(parent);
            await _context.SaveChangesAsync();
        }

        public override async Task DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new Exception("Unable to process, id is null");
            }

            var entity = await _context.Set<PurchaseOrderItem>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                if (entity is IHasAudit auditEntity && !string.IsNullOrEmpty(_userId))
                {
                    auditEntity.UpdatedByUserId = _userId;
                }

                if (entity is IHasAudit auditedEntity)
                {
                    auditedEntity.UpdatedAtUtc = DateTime.Now;
                }

                if (entity is IHasSoftDelete softDeleteEntity)
                {
                    softDeleteEntity.IsNotDeleted = false;
                    _context.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    _context.Set<PurchaseOrderItem>().Remove(entity);
                }

                await _context.SaveChangesAsync();


                await _purchaseOrderService.RecalculateParentAsync(entity.PurchaseOrderId);
            }
        }

        public override async Task DeleteByRowGuidAsync(Guid? rowGuid)
        {
            if (!rowGuid.HasValue)
            {
                throw new Exception("Unable to process, row guid is null");
            }

            var entity = await _context.Set<PurchaseOrderItem>()
                .FirstOrDefaultAsync(x => x.RowGuid == rowGuid);

            if (entity != null)
            {
                if (entity is IHasAudit auditEntity && !string.IsNullOrEmpty(_userId))
                {
                    auditEntity.UpdatedByUserId = _userId;
                }

                if (entity is IHasAudit auditedEntity)
                {
                    auditedEntity.UpdatedAtUtc = DateTime.Now;
                }

                if (entity is IHasSoftDelete softDeleteEntity)
                {
                    softDeleteEntity.IsNotDeleted = false;
                    _context.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    _context.Set<PurchaseOrderItem>().Remove(entity);
                }

                await _context.SaveChangesAsync();


                await _purchaseOrderService.RecalculateParentAsync(entity.PurchaseOrderId);
            }
        }
    }
}
