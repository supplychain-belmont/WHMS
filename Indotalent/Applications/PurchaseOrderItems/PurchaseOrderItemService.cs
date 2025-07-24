using Indotalent.Application.PurchaseOrders;
using Indotalent.Applications.Products;
using Indotalent.Applications.PurchaseOrders;
using Indotalent.Data;
using Indotalent.Domain.Contracts;
using Indotalent.Domain.Entities;
using Indotalent.Infrastructures.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.PurchaseOrderItems
{
    public class PurchaseOrderItemService : Repository<PurchaseOrderItem>
    {
        private readonly PurchaseOrderService _purchaseOrderService;
        private readonly ProductService _productService;
        private readonly PurchaseOrderItemProcessor _purchaseOrderItemProcessor;

        public PurchaseOrderItemService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            PurchaseOrderService purchaseOrderService,
            ProductService productService,
            PurchaseOrderItemProcessor purchaseOrderItemProcessor) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _purchaseOrderService = purchaseOrderService;
            _productService = productService;
            _purchaseOrderItemProcessor = purchaseOrderItemProcessor;
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

                await ProcessPurchaseOrderItem(entity);
                await _context.Set<PurchaseOrderItem>().AddAsync(entity);
                await _context.SaveChangesAsync();

                await _purchaseOrderService.RecalculateParentAsync(entity.PurchaseOrderId);
            }
            else
            {
                throw new Exception("Unable to process, entity is null");
            }
        }

        public override async Task AddRangeAsync(ICollection<PurchaseOrderItem> entities)
        {
            foreach (PurchaseOrderItem entity in entities)
            {
                await ProcessPurchaseOrderItem(entity);
            }

            await base.AddRangeAsync(entities);

            if (entities.Count > 0)
                await _purchaseOrderService.RecalculateParentAsync(entities.First().PurchaseOrderId);
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

                await ProcessPurchaseOrderItem(entity);
                _context.Set<PurchaseOrderItem>().Update(entity);
                await _context.SaveChangesAsync();

                await _purchaseOrderService.RecalculateParentAsync(entity.PurchaseOrderId);
            }
            else
            {
                throw new Exception("Unable to process, entity is null");
            }
        }

        private async Task ProcessPurchaseOrderItem(PurchaseOrderItem entity)
        {
            var order = await _purchaseOrderService.GetAll()
                .Where(x => x.Id == entity.PurchaseOrderId)
                .Select(x => new { x.ContainerM3, x.TotalAgencyCost, x.TotalTransportContainerCost })
                .FirstOrDefaultAsync();
            var product = await _productService.GetAll()
                .Where(x => x.Id == entity.ProductId)
                .FirstOrDefaultAsync();

            if (product is { UnitCost: not null })
            {
                entity.UnitCost = product.UnitCost.Value;
                entity.UnitCostDiscounted = product.UnitCost.Value;
            }

            entity.ShowOrderItem = entity.AssemblyId == null;
            if (product is { IsNationalProduct: true })
            {
                _purchaseOrderItemProcessor.CalculateCostsWithoutShipping(entity);
            }
            else
            {
                _purchaseOrderItemProcessor.CalculateCosts(entity, product!.M3, order!.ContainerM3,
                    order.TotalTransportContainerCost, order.TotalAgencyCost);
            }
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
