using Indotalent.Applications.Companies;
using Indotalent.Applications.InventoryTransactions;
using Indotalent.Applications.TransferIns;
using Indotalent.Models.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.Pages.TransferIns
{
    public class TransferInPdfModel : PageModel
    {
        private readonly TransferInService _transferInService;
        private readonly InventoryTransactionService _inventoryTransactionService;
        private readonly CompanyService _companyService;
        public TransferInPdfModel(
            TransferInService transferInService,
            InventoryTransactionService inventoryTransactionService,
            CompanyService companyService)
        {
            _transferInService = transferInService;
            _inventoryTransactionService = inventoryTransactionService;
            _companyService = companyService;
        }

        public TransferIn? TransferIn { get; set; }
        public List<InventoryTransaction>? InventoryTransactions { get; set; }
        public Company? Company { get; set; }
        public Warehouse? WarehouseFrom { get; set; }
        public Warehouse? WarehouseTo { get; set; }
        public string? CompanyAddress { get; set; }

        public async Task OnGetAsync(int? id)
        {
            Company = await _companyService.GetDefaultCompanyAsync();

            CompanyAddress = string.Join(", ", new List<string>()
            {
                Company?.Street ?? string.Empty,
                Company?.City ?? string.Empty,
                Company?.State ?? string.Empty,
                Company?.Country ?? string.Empty,
                Company?.ZipCode ?? string.Empty
            }.Where(s => !string.IsNullOrEmpty(s)));

            TransferIn = await _transferInService
                .GetAll()
                .Include(x => x.TransferOut)
                    .ThenInclude(x => x!.WarehouseFrom)
                .Include(x => x.TransferOut)
                    .ThenInclude(x => x!.WarehouseTo)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            WarehouseFrom = TransferIn?.TransferOut?.WarehouseFrom;
            WarehouseTo = TransferIn?.TransferOut?.WarehouseTo;

            InventoryTransactions = await _inventoryTransactionService
                .GetAll()
                .Where(x => x.ModuleId == id && x.ModuleName == nameof(TransferIn))
                .Include(x => x.Product)
                    .ThenInclude(x => x!.UnitMeasure)
                .ToListAsync();
        }
    }
}
