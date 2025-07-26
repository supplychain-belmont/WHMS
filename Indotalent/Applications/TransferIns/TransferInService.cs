using Indotalent.Applications.InventoryTransactions;
using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.TransferOuts;
using Indotalent.Domain.Entities;
using Indotalent.Domain.Enums;
using Indotalent.Persistence;
using Indotalent.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.TransferIns
{
    public class TransferInService : Repository<TransferIn>
    {
        private readonly NumberSequenceService _numberSequenceService;
        private readonly TransferOutService _transferOutService;
        private readonly InventoryTransactionService _inventoryTransactionService;

        public TransferInService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            TransferOutService transferOutService,
            InventoryTransactionService inventoryTransactionService) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
            _transferOutService = transferOutService;
            _inventoryTransactionService = inventoryTransactionService;
        }

        public override async Task AddAsync(TransferIn? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(TransferIn), "", "IN");
            await base.AddAsync(entity);

            var transferOut = await _transferOutService.GetByIdAsync(entity.TransferOutId);

            var transferOutTransactions = await _inventoryTransactionService
                .GetAll()
                .Where(item => item.ModuleId == entity.TransferOutId && item.ModuleName == nameof(TransferOut))
                .ToListAsync();

            var transactions = transferOutTransactions.Select(item => new InventoryTransaction
            {
                WarehouseId = transferOut!.WarehouseToId!.Value,
                WarehouseToId = transferOut!.WarehouseToId!.Value,
                WarehouseFromId = item.WarehouseToId,
                ProductId = item.ProductId,
                ModuleId = entity.Id,
                ModuleName = nameof(TransferIn),
                ModuleCode = "TO-IN",
                ModuleNumber = entity.Number ?? string.Empty,
                MovementDate = entity.TransferReceiveDate!.Value,
                Status = (InventoryTransactionStatus)entity.Status!,
                RequestedMovement = item.Movement,
                Movement = item.Movement,
                Number = _numberSequenceService.GenerateNumber(nameof(InventoryTransaction), "", "IVT")
            }).ToList();

            foreach (InventoryTransaction inventoryTransaction in transactions)
            {
                await _inventoryTransactionService.AddAsync(inventoryTransaction);
            }
        }

        public override async Task UpdateAsync(TransferIn? entity)
        {
            await base.UpdateAsync(entity);
            await RecalculateChildAsync(entity?.Id);
        }

        private async Task RecalculateChildAsync(int? entityId)
        {
            var master = await _context.TransferIn
                .Where(x => x.Id == entityId && x.IsNotDeleted)
                .FirstOrDefaultAsync();

            var children = await _inventoryTransactionService
                .GetAll()
                .Where(x => x.ModuleId == entityId && x.ModuleName == nameof(TransferIn))
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
