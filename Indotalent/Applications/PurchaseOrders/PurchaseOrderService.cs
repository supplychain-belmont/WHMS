using Indotalent.Application.PurchaseOrders;
using Indotalent.Applications.Lots;
using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.PurchaseOrderItems;
using Indotalent.Data;
using Indotalent.Domain.Contracts;
using Indotalent.Domain.Entities;
using Indotalent.Infrastructures.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.PurchaseOrders
{
    public class PurchaseOrderService : Repository<PurchaseOrder>
    {
        private readonly NumberSequenceService _numberSequenceService;
        private readonly PurchaseOrderProcessor _purchaseOrderProcessor;
        private readonly LotService _lotService;
        private readonly LotItemService _lotItemService;
        private readonly IServiceProvider _serviceProvider;

        public PurchaseOrderService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            PurchaseOrderProcessor purchaseOrderProcessor,
            LotService lotService,
            LotItemService lotItemService,
            IServiceProvider serviceProvider) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
            _purchaseOrderProcessor = purchaseOrderProcessor;
            _lotService = lotService;
            _lotItemService = lotItemService;
            _serviceProvider = serviceProvider;
        }

        public override async Task AddAsync(PurchaseOrder? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(PurchaseOrder), "", "PO");
            entity.TaxAmount = 0.0m;
            entity.AfterTaxAmount = 0.0m;
            entity.BeforeTaxAmount = 0.0m;

            await base.AddAsync(entity);

            if (entity.LotId.HasValue)
            {
                var lot = await _lotService.GetByIdAsync(entity.LotId.Value);
                _purchaseOrderProcessor.CalculatePurchaseOrder(lot!, entity);
                await CreatePurchaseOrderItemAsync(entity);
            }
        }

        private async Task CreatePurchaseOrderItemAsync(PurchaseOrder purchaseOrder)
        {
            using var scope = _serviceProvider.CreateScope();
            var purchaseOrderItemService = scope.ServiceProvider.GetRequiredService<PurchaseOrderItemService>();
            var lotItems = await _lotItemService.GetAll()
                .Include(li => li.Product)
                .Include(li => li.Lot)
                .Where(li => li.LotId == purchaseOrder.LotId)
                .ToListAsync();

            var orderItems = _purchaseOrderProcessor.CreatePurchaseOrderItems(lotItems, purchaseOrder);
            await purchaseOrderItemService.AddRangeAsync(orderItems);
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
