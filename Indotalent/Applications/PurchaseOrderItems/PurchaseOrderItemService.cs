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

                var order = await _purchaseOrderService.GetAll()
                    .Where(x => x.Id == entity.PurchaseOrderId)
                    .Select(x => new { x.ContainerM3, x.TotalAgencyCost, x.TotalTransportContainerCost })
                    .FirstOrDefaultAsync();
                var product = await _productService.GetAll()
                    .Where(x => x.Id == entity.ProductId)
                    .FirstOrDefaultAsync();

                entity.ShowOrderItem = entity.AssemblyId == null;
                _purchaseOrderItemProcessor.CalculateCosts(entity, product!.M3, order!.ContainerM3,
                    order.TotalTransportContainerCost, order.TotalAgencyCost);
                _context.Set<PurchaseOrderItem>().Add(entity);
                await _context.SaveChangesAsync();

                await _purchaseOrderService.RecalculateParentAsync(entity.PurchaseOrderId);
            }
            else
            {
                throw new Exception("Unable to process, entity is null");
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
                    .FirstOrDefaultAsync();

                _purchaseOrderItemProcessor.CalculateCosts(entity, product!.M3, order!.ContainerM3,
                    order.TotalTransportContainerCost, order.TotalAgencyCost);
                _context.Set<PurchaseOrderItem>().Update(entity);
                await _context.SaveChangesAsync();

                await _purchaseOrderService.RecalculateParentAsync(entity.PurchaseOrderId);
            }
            else
            {
                throw new Exception("Unable to process, entity is null");
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
