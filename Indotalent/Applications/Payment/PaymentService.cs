using Microsoft.EntityFrameworkCore;
using Indotalent.Models.Entities;

namespace Indotalent.Applications.Payment
{
    public class PaymentService
    {
        private readonly DbContext _context;

        public PaymentService(DbContext context)
        {
            _context = context;
        }

        public IQueryable<Models.Entities.Payment > GetAll()
        {
            return _context.Set<Models.Entities.Payment >();
        }

        public async Task<Models.Entities.Payment ?> GetByIdAsync(Guid id)
        {
            return await _context.Set<Models.Entities.Payment >().FindAsync(id);
        }

        public async Task AddAsync(Models.Entities.Payment  payment)
        {
            await _context.Set<Models.Entities.Payment >().AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Models.Entities.Payment payment)
        {
            _context.Set<Models.Entities.Payment >().Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var payment = await GetByIdAsync(id);
            if (payment != null)
            {
                _context.Set<Models.Entities.Payment >().Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }
}