using Indotalent.Application.PurchaseOrders;
using Indotalent.Applications.NumberSequences;
using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Domain.Contracts;
using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.PurchaseOrders
{
    public class PurchaseOrderService : Repository<PurchaseOrder>
    {
        private readonly NumberSequenceService _numberSequenceService;
        private readonly PurchaseOrderProcessor _purchaseOrderProcessor;

        public PurchaseOrderService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            PurchaseOrderProcessor purchaseOrderProcessor) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
            _purchaseOrderProcessor = purchaseOrderProcessor;
        }

        public override async Task AddAsync(PurchaseOrder? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(PurchaseOrder), "", "PO");
            entity.TaxAmount = 0.0m;
            entity.AfterTaxAmount = 0.0m;
            entity.BeforeTaxAmount = 0.0m;

            await base.AddAsync(entity);
        }

        public async Task RecalculateParentAsync(int? masterId)
        {
            var master = await _context.Set<PurchaseOrder>()
                .Include(x => x.Tax)
                .Where(x => x.Id == masterId && x.IsNotDeleted)
                .FirstOrDefaultAsync();

            var children = await _context.Set<PurchaseOrderItem>()
                .Where(x => x.PurchaseOrderId == masterId && x.IsNotDeleted && !x.IsAssembly)
                .AsNoTracking()
                .ToListAsync();

            if (master != null)
            {
                _purchaseOrderProcessor.RecalculateParent(master, children);
                _context.Set<PurchaseOrder>().Update(master);
                await _context.SaveChangesAsync();
            }
        }


        public override async Task UpdateAsync(PurchaseOrder? entity)
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

                _context.Set<PurchaseOrder>().Update(entity);
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
