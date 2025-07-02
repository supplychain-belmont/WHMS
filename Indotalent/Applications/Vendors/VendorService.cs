using Indotalent.Applications.NumberSequences;
using Indotalent.Data;
using Indotalent.Domain.Entities;
using Indotalent.Infrastructures.Repositories;

namespace Indotalent.Applications.Vendors
{
    public class VendorService : Repository<Vendor>
    {
        private readonly NumberSequenceService _numberSequenceService;

        public VendorService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
        }

        public override Task AddAsync(Vendor? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(
                nameof(Vendor), "", "VND");
            return base.AddAsync(entity);
        }
    }
}
