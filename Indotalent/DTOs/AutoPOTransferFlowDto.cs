using Indotalent.DTOs;

namespace Indotalent.Applications.PurchaseOrders
{
    public class AutoPOTransferFlowDto : AutoPurchaseOrderDto
    {
        public int WarehouseId { get; set; }
        public DateTime? ReleaseDate { get; set; } 
        public DateTime? ReceiveDate { get; set; }
        public string? TransferDescription { get; set; }
    }
}
