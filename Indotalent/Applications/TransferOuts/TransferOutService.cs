using Indotalent.Applications.InventoryTransactions;
using Indotalent.Applications.NumberSequences;
using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Entities;
using Indotalent.Models.Enums;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.TransferOuts
{
    public class TransferOutService : Repository<TransferOut>
    {
        private readonly NumberSequenceService _numberSequenceService;
        private readonly InventoryTransactionService _inventoryTransactionService;

        public TransferOutService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            InventoryTransactionService inventoryTransactionService) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
            _inventoryTransactionService = inventoryTransactionService;
        }

        public override Task AddAsync(TransferOut? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(TransferOut), "", "OUT");
            return base.AddAsync(entity);
        }

        public override async Task UpdateAsync(TransferOut? entity)
        {
            await base.UpdateAsync(entity);
            await RecalculateChildAsync(entity!.Id);
        }

        private async Task RecalculateChildAsync(int entityId)
        {
            var master = await _context.TransferOut
                .Include(x => x.WarehouseFrom)
                .Include(x => x.WarehouseTo)
                .Where(x => x.Id == entityId && x.IsNotDeleted)
                .FirstOrDefaultAsync();

            var children = await _inventoryTransactionService
                .GetAll()
                .Where(x => x.ModuleId == entityId)
                .ToListAsync();

            if (master == null) return;

            foreach (var child in children)
            {
                child.Status = (InventoryTransactionStatus)master.Status!;
                await _inventoryTransactionService.UpdateAsync(child);
            }
        }
    }
}
