using Indotalent.Applications.NumberSequences;
using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Domain.Entities;

namespace Indotalent.Applications.CustomerContacts
{
    public class CustomerContactService : Repository<CustomerContact>
    {
        private readonly NumberSequenceService _numberSequenceService;

        public CustomerContactService(
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

        public override Task AddAsync(CustomerContact? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(CustomerContact), "", "CC");
            return base.AddAsync(entity);
        }
    }
}
