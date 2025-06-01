using Indotalent.Application.Products;
using Indotalent.Application.SalesOrders;
using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.Products;
using Indotalent.Data;
using Indotalent.Domain.Contracts;
using Indotalent.Domain.Entities;
using Indotalent.Infrastructures.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.SalesOrders
{
    public class SalesOrderService : Repository<SalesOrder>
    {
        private readonly NumberSequenceService _numberSequenceService;
        private readonly SalesOrderProcessor _salesOrderProcessor;
        private readonly AssemblyService _assemblyService;
        private readonly AssemblyChildService _assemblyChildService;
        private readonly AssemblyProcessor _assemblyProcessor;

        public SalesOrderService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            SalesOrderProcessor salesOrderProcessor,
            AssemblyService assemblyService,
            AssemblyChildService assemblyChildService,
            AssemblyProcessor assemblyProcessor) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
            _salesOrderProcessor = salesOrderProcessor;
            _assemblyService = assemblyService;
            _assemblyChildService = assemblyChildService;
            _assemblyProcessor = assemblyProcessor;
        }

        public override Task AddAsync(SalesOrder? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(SalesOrder), "", "SO");
            entity.TaxAmount = 0.0m;
            entity.AfterTaxAmount = 0.0m;
            entity.BeforeTaxAmount = 0.0m;
            return base.AddAsync(entity);
        }

        public async Task<SalesOrder> CreateOrderFromAssemblyAsync(int assemblyId, int quantity = 1)
        {
            var assembly = await _assemblyService.GetByIdAsync(assemblyId);

            var children = await _assemblyChildService
                .GetAll()
                .Where(x => x.AssemblyId == assemblyId)
                .ToListAsync();

            var codeNumber = _numberSequenceService.GenerateNumber(nameof(SalesOrder), "", "SO");

            var salesOrder = _assemblyProcessor.CreateSalesOrder(assembly, codeNumber);

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await base.AddAsync(salesOrder);
                var assemblyOrderItem = _assemblyProcessor.CreateSalesOrderItem(salesOrder.Id, assembly, quantity);
                await _context.Set<SalesOrderItem>().AddAsync(assemblyOrderItem);
                await _context.SaveChangesAsync();

                var salesOrderItems = _assemblyProcessor.CreateSalesOrderItem(salesOrder.Id, children, quantity);
                await _context.Set<SalesOrderItem>().AddRangeAsync(salesOrderItems);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

            return salesOrder;
        }

        public async Task RecalculateParentAsync(int? masterId)
        {
            var master = await _context.Set<SalesOrder>()
                .Include(x => x.Tax)
                .Where(x => x.Id == masterId && x.IsNotDeleted == true)
                .FirstOrDefaultAsync();

            var children = await _context.Set<SalesOrderItem>()
                .Where(x => x.SalesOrderId == masterId && x.IsNotDeleted == true)
                .ToListAsync();

            if (master != null)
            {
                _salesOrderProcessor.RecalculateParent(master, children);
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
