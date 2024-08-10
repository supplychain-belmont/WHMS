using AutoMapper;
using Indotalent.Data;
using Indotalent.DTOs;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Contracts;
using Indotalent.Models.Entities;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.SalesOrders
{
    public class SalesOrderService : Repository<SalesOrder>
    {
        private readonly IMapper _mapper;

        public SalesOrderService(
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

        public IQueryable<SalesOrderDto> GetAllDtos()
        {
            return _mapper.ProjectTo<SalesOrderDto>(_context.Set<SalesOrder>()
                .Include(x => x.Customer)
                .Include(x => x.Tax)
                .AsQueryable());
        }

        public async Task<SalesOrderDto?> GetDtoByIdAsync(int id)
        {
            var salesOrder = await _context.Set<SalesOrder>()
                .Include(x => x.Customer)
                .Include(x => x.Tax)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<SalesOrderDto>(salesOrder);
        }

        public async Task<SalesOrderDto> CreateAsync(SalesOrderDto salesOrderDto)
        {
            var salesOrder = _mapper.Map<SalesOrder>(salesOrderDto);
            await AddAsync(salesOrder);
            return _mapper.Map<SalesOrderDto>(salesOrder);
        }

        public async Task<SalesOrderDto> UpdateAsync(SalesOrderDto salesOrderDto)
        {
            var salesOrder = await _context.Set<SalesOrder>().FindAsync(salesOrderDto.Id);
            if (salesOrder == null)
            {
                throw new Exception("SalesOrder not found.");
            }

            _mapper.Map(salesOrderDto, salesOrder);
            await UpdateAsync(salesOrder);
            return _mapper.Map<SalesOrderDto>(salesOrder);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await base.DeleteByIdAsync(id);
        }

    }
}
