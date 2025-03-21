using AutoMapper;

using Indotalent.Applications.Products;
using Indotalent.Applications.SalesOrders;
using Indotalent.Data;
using Indotalent.DTOs;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Domain.Contracts;
using Indotalent.Domain.Entities;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.SalesOrderItems
{
    public class SalesOrderItemService : Repository<SalesOrderItem>
    {
        private readonly SalesOrderService _salesOrderService;
        private readonly ProductService _productService;

        public SalesOrderItemService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            SalesOrderService salesOrderService,
            ProductService productService) :
            base(context, httpContextAccessor, auditColumnTransformer)
        {
            _salesOrderService = salesOrderService;
            _productService = productService;
        }

        public override async Task AddAsync(SalesOrderItem? entity)
        {
            if (entity != null)
            {
                if (entity is IHasAudit auditEntity && !string.IsNullOrEmpty(_userId))
                {
                    auditEntity.CreatedAtUtc = DateTime.Now;
                    auditEntity.CreatedByUserId = _userId;
                }

                var product = await _productService.GetByIdAsync(entity.ProductId);

                if (product!.UnitPrice60 != null)
                {
                    entity.UnitPrice = product.UnitPrice60.Value;
                }

                entity.UnitPrice40 = product.UnitPrice40;
                entity.UnitPrice50 = product.UnitPrice50;
                entity.UnitPrice60 = product.UnitPrice60;
                entity.UnitCost = product.UnitCost ?? 0;

                entity.RecalculateTotal();
                _context.Set<SalesOrderItem>().Add(entity);
                await _context.SaveChangesAsync();

                await _salesOrderService.RecalculateParentAsync(entity.SalesOrderId);
            }
            else
            {
                throw new Exception("Unable to process, entity is null");
            }
        }

        public override async Task UpdateAsync(SalesOrderItem? entity)
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

                entity.RecalculateTotal();
                _context.Set<SalesOrderItem>().Update(entity);
                await _context.SaveChangesAsync();


                await _salesOrderService.RecalculateParentAsync(entity.SalesOrderId);
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

            var entity = await _context.Set<SalesOrderItem>()
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
                    _context.Set<SalesOrderItem>().Remove(entity);
                }

                await _context.SaveChangesAsync();


                await _salesOrderService.RecalculateParentAsync(entity.SalesOrderId);
            }
        }

        public override async Task DeleteByRowGuidAsync(Guid? rowGuid)
        {
            if (!rowGuid.HasValue)
            {
                throw new Exception("Unable to process, row guid is null");
            }

            var entity = await _context.Set<SalesOrderItem>()
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
                    _context.Set<SalesOrderItem>().Remove(entity);
                }

                await _context.SaveChangesAsync();


                await _salesOrderService.RecalculateParentAsync(entity.SalesOrderId);
            }
        }
    }
}
