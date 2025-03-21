using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Indotalent.Data;
using Indotalent.DTOs;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.SalesReturns
{
    public class SalesReturnService : Repository<SalesReturn>
    {
        private readonly IMapper _mapper;

        public SalesReturnService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            IMapper mapper) :
                base(
                    context,
                    httpContextAccessor,
                    auditColumnTransformer)
        {
            _mapper = mapper;
        }

        public IQueryable<SalesReturnDto> GetAllDtos()
        {
            return _mapper.ProjectTo<SalesReturnDto>(_context.Set<SalesReturn>()
                .Include(x => x.DeliveryOrder)
                    .ThenInclude(x => x!.SalesOrder)
                        .ThenInclude(x => x!.Customer)
                .AsQueryable());
        }

        public async Task<SalesReturnDto?> GetDtoByIdAsync(int id)
        {
            var salesReturn = await _context.Set<SalesReturn>()
                .Include(x => x.DeliveryOrder)
                    .ThenInclude(x => x!.SalesOrder)
                        .ThenInclude(x => x!.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<SalesReturnDto>(salesReturn);
        }

        public async Task<SalesReturnDto> CreateAsync(SalesReturnDto salesReturnDto)
        {
            var salesReturn = _mapper.Map<SalesReturn>(salesReturnDto);
            await AddAsync(salesReturn);
            return _mapper.Map<SalesReturnDto>(salesReturn);
        }

        public async Task<SalesReturnDto> UpdateAsync(SalesReturnDto salesReturnDto)
        {
            var salesReturn = await _context.Set<SalesReturn>().FindAsync(salesReturnDto.Id);
            if (salesReturn == null)
            {
                throw new Exception("SalesReturn not found.");
            }

            _mapper.Map(salesReturnDto, salesReturn);
            await UpdateAsync(salesReturn);
            return _mapper.Map<SalesReturnDto>(salesReturn);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await base.DeleteByIdAsync(id);
        }
    }
}
