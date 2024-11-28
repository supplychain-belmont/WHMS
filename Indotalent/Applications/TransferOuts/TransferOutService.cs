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
        private readonly InventoryStockService _inventoryStockService;

        public TransferOutService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            InventoryTransactionService inventoryTransactionService,
            InventoryStockService inventoryStockService) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
            _inventoryTransactionService = inventoryTransactionService;
            _inventoryStockService = inventoryStockService;
        }

        public override Task AddAsync(TransferOut? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(TransferOut), "", "OUT");
            return base.AddAsync(entity);
        }

        public override async Task UpdateAsync(TransferOut? entity)
        {
            var children = await _inventoryTransactionService
                .GetAll()
                .Where(x => x.ModuleId == entity!.Id && x.ModuleName == nameof(TransferOut))
                .ToListAsync();

            if (entity == null) return;

            if (entity.Status == TransferStatus.Confirmed)
            {
                var stockInfo = await _inventoryStockService
                    .GetAll()
                    .Where(it =>
                        children.Select(x => x.ProductId).Contains(it.ProductId!.Value) &&
                        children.Select(x => x.WarehouseId).Contains(it.WarehouseId!.Value))
                    .Select(it =>
                        new { it.ProductId, it.Product, it.WarehouseId, InventoryStock.Parse(it).Stock })
                    .ToListAsync();

                var products = new List<string>();
                foreach (InventoryTransaction item in children)
                {
                    var inventoryStock = stockInfo
                        .Find(it =>
                            it.ProductId == item.ProductId && it.WarehouseId == item.WarehouseId
                        );

                    if (inventoryStock is null) continue;
                    if (item.Movement <= inventoryStock.Stock)
                    {
                        continue;
                    }

                    entity.Status = TransferStatus.Draft;
                    products.Add(inventoryStock.Product!);
                }

                if (entity.Status == TransferStatus.Draft)
                {
                    throw new ArgumentException($"Stock is not enough for product(s): {string.Join(", ", products)}");
                }
            }

            foreach (var child in children)
            {
                child.Status = (InventoryTransactionStatus)entity.Status!;
                await _inventoryTransactionService.UpdateAsync(child);
            }

            await base.UpdateAsync(entity);
        }
    }
}
