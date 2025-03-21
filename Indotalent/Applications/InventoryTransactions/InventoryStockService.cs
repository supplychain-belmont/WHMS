using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Domain.Entities;

namespace Indotalent.Applications.InventoryTransactions;

public class InventoryStockService : Repository<InventoryStock>
{
    public InventoryStockService(ApplicationDbContext context,
        IHttpContextAccessor httpContextAccessor,
        IAuditColumnTransformer auditColumnTransformer) : base(context, httpContextAccessor, auditColumnTransformer)
    {
    }
}
