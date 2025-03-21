using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.Payment
{
    public class CreditDuesService
    {
        private readonly DbContext _context;

        public CreditDuesService(DbContext context)
        {
            _context = context;
        }

        public IQueryable<CreditDues> GetAll()
        {
            return _context.Set<CreditDues>();
        }

        public async Task<CreditDues?> GetByIdAsync(int id)
        {
            return await _context.Set<CreditDues>().FindAsync(id);
        }

        public async Task AddAsync(CreditDues creditDues)
        {
            await _context.Set<CreditDues>().AddAsync(creditDues);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CreditDues creditDues)
        {
            _context.Set<CreditDues>().Update(creditDues);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var creditDues = await GetByIdAsync(id);
            if (creditDues != null)
            {
                _context.Set<CreditDues>().Remove(creditDues);
                await _context.SaveChangesAsync();
            }
        }
    }
}
