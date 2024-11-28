using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Entities;

namespace Indotalent.Applications.InventoryTransactions;

public class InventoryStockService : Repository<InventoryStock>
{
    public InventoryStockService(ApplicationDbContext context,
        IHttpContextAccessor httpContextAccessor,
        IAuditColumnTransformer auditColumnTransformer) : base(context, httpContextAccessor, auditColumnTransformer)
    {
    }
}
