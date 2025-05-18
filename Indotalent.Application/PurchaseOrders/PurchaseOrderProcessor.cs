using Indotalent.Domain.Entities;

namespace Indotalent.Application.PurchaseOrders;

public class PurchaseOrderProcessor
{
    public void RecalculateParent(PurchaseOrder master, List<PurchaseOrderItem> children)
    {
        master.BeforeTaxAmount = 0;
        foreach (var item in children)
        {
            master.BeforeTaxAmount += item.Total;
        }

        if (master.Tax != null)
        {
            master.TaxAmount = (master.Tax.Percentage / 100.0m) * master.BeforeTaxAmount;
        }

        master.AfterTaxAmount = master.BeforeTaxAmount + master.TaxAmount;
    }
}
