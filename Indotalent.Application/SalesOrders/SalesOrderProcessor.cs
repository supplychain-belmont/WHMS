using Indotalent.Domain.Entities;

namespace Indotalent.Application.SalesOrders;

public class SalesOrderProcessor
{
    public void RecalculateParent(SalesOrder master, List<SalesOrderItem> children)
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
