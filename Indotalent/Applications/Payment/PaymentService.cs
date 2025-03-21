using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.Payment
{
    public class PaymentService
    {
        private readonly DbContext _context;

        public PaymentService(DbContext context)
        {
            _context = context;
        }

        public IQueryable<Domain.Entities.Payment> GetAll()
        {
            return _context.Set<Domain.Entities.Payment>();
        }

        public async Task<Domain.Entities.Payment?> GetByIdAsync(Guid id)
        {
            return await _context.Set<Domain.Entities.Payment>().FindAsync(id);
        }

        public async Task AddAsync(Domain.Entities.Payment payment)
        {
            await _context.Set<Domain.Entities.Payment>().AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.Payment payment)
        {
            _context.Set<Domain.Entities.Payment>().Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var payment = await GetByIdAsync(id);
            if (payment != null)
            {
                _context.Set<Domain.Entities.Payment>().Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
