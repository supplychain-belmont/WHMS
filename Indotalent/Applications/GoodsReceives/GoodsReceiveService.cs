﻿using Indotalent.Applications.InventoryTransactions;
using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.PurchaseOrderItems;
using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Contracts;
using Indotalent.Models.Entities;
using Indotalent.Models.Enums;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.GoodsReceives
{
    public class GoodsReceiveService : Repository<GoodsReceive>
    {
        private readonly NumberSequenceService _numberSequenceService;
        private readonly PurchaseOrderItemService _purchaseOrderItemService;
        private readonly InventoryTransactionService _inventoryTransactionService;

        public GoodsReceiveService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            PurchaseOrderItemService purchaseOrderItemService,
            InventoryTransactionService inventoryTransactionService) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
            _purchaseOrderItemService = purchaseOrderItemService;
            _inventoryTransactionService = inventoryTransactionService;
        }

        public override async Task AddAsync(GoodsReceive? entity)
        {
            var moduleName = nameof(GoodsReceive) ?? string.Empty;
            await base.AddAsync(entity);
            var purchaseItems = await _purchaseOrderItemService.GetAll()
                .Where(item => item.PurchaseOrderId == entity!.PurchaseOrderId)
                .ToListAsync();

            var transactions = purchaseItems
                .Select(item => new InventoryTransaction()
                {
                    WarehouseId = 2,
                    ProductId = item.ProductId,
                    ModuleId = entity!.Id,
                    ModuleName = moduleName,
                    ModuleCode = "GR",
                    ModuleNumber = entity.Number ?? string.Empty,
                    MovementDate = entity.ReceiveDate!.Value,
                    Status = (InventoryTransactionStatus)entity.Status!,
                    RequestedMovement = item.Quantity,
                    Movement = item.Quantity,
                    Number = _numberSequenceService.GenerateNumber(nameof(InventoryTransaction), "", "IVT")
                }).ToList();

            foreach (InventoryTransaction inventoryTransaction in transactions)
            {
                await _inventoryTransactionService.AddAsync(inventoryTransaction);
            }
        }

        public override async Task UpdateAsync(GoodsReceive? entity)
        {
            await base.UpdateAsync(entity);
            await RecalculateChildAsync(entity?.Id);
        }


        private async Task RecalculateChildAsync(int? masterId)
        {
            var master = await _context.Set<GoodsReceive>()
                .Include(x => x.PurchaseOrder)
                .ThenInclude(x => x!.Vendor)
                .Where(x => x.Id == masterId && x.IsNotDeleted)
                .FirstOrDefaultAsync();

            var children = await _inventoryTransactionService.GetAll()
                .Where(x => x.ModuleId == masterId && x.ModuleName == nameof(GoodsReceive))
                .ToListAsync();

            if (master != null)
            {
                foreach (InventoryTransaction inventoryTransaction in children)
                {
                    inventoryTransaction.Status = (InventoryTransactionStatus)master.Status!;
                    await _inventoryTransactionService.UpdateAsync(inventoryTransaction);
                }
            }
        }
    }
}
