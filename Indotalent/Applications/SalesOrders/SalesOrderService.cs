using AutoMapper;

using Indotalent.Application.SalesOrders;
using Indotalent.Applications.NumberSequences;
using Indotalent.Data;
using Indotalent.Domain.Contracts;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;
using Indotalent.Infrastructures.Repositories;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.SalesOrders
{
    public class SalesOrderService : Repository<SalesOrder>
    {
        private readonly NumberSequenceService _numberSequenceService;
        private readonly SalesOrderProcessor _salesOrderProcessor;

        public SalesOrderService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            SalesOrderProcessor salesOrderProcessor
        ) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
            _salesOrderProcessor = salesOrderProcessor;
        }

        public override Task AddAsync(SalesOrder? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(SalesOrder), "", "SO");
            entity.TaxAmount = 0.0m;
            entity.AfterTaxAmount = 0.0m;
            entity.BeforeTaxAmount = 0.0m;
            return base.AddAsync(entity);
        }

        public async Task RecalculateParentAsync(int? masterId)
        {
            var master = await _context.Set<SalesOrder>()
                .Include(x => x.Tax)
                .Where(x => x.Id == masterId && x.IsNotDeleted == true)
                .FirstOrDefaultAsync();

            var childs = await _context.Set<SalesOrderItem>()
                .Where(x => x.SalesOrderId == masterId && x.IsNotDeleted == true)
                .ToListAsync();

            if (master != null)
            {
                _salesOrderProcessor.RecalculateParent(master, childs);
                _context.Set<SalesOrder>().Update(master);
                await _context.SaveChangesAsync();
            }
        }


        public override async Task UpdateAsync(SalesOrder? entity)
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

                _context.Set<SalesOrder>().Update(entity);
                await _context.SaveChangesAsync();

                await RecalculateParentAsync(entity.Id);
            }
            else
            {
                throw new Exception("Unable to process, entity is null");
            }
        }
    }
}
