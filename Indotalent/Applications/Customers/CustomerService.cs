using Indotalent.Applications.NumberSequences;
using Indotalent.Domain.Entities;
using Indotalent.Persistence;
using Indotalent.Persistence.Repositories;

namespace Indotalent.Applications.Customers
{
    public class CustomerService : Repository<Customer>
    {
        private readonly NumberSequenceService _numberSequenceService;

        public CustomerService(
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

        public override Task AddAsync(Customer? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(Customer), "", "CST");
            return base.AddAsync(entity);
        }
    }
}
