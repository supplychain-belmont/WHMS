using Indotalent.Applications.NumberSequences;
using Indotalent.Data;
using Indotalent.Domain.Entities;
using Indotalent.Infrastructures.Repositories;

namespace Indotalent.Applications.Lots;

public class LotService : Repository<Lot>
{
    private readonly NumberSequenceService _numberSequenceService;

    public LotService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor,
        IAuditColumnTransformer auditColumnTransformer, NumberSequenceService numberSequenceService) : base(context,
        httpContextAccessor, auditColumnTransformer)
    {
        _numberSequenceService = numberSequenceService;
    }

    public override Task AddAsync(Lot? entity)
    {
        entity!.Number = _numberSequenceService.GenerateNumber(nameof(Lot), "", "LOT");
        return base.AddAsync(entity);
    }
}
