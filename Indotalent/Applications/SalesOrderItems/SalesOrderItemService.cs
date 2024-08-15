using AutoMapper;

using Indotalent.Applications.SalesOrders;
using Indotalent.Data;
using Indotalent.DTOs;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Contracts;
using Indotalent.Models.Entities;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.SalesOrderItems
{
    public class SalesOrderItemService : Repository<SalesOrderItem>
    {
        private readonly IMapper _mapper;

        public SalesOrderItemService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            IMapper mapper) :
                base(context, httpContextAccessor, auditColumnTransformer)
        {
            _mapper = mapper;
        }

        public IQueryable<SalesOrderItemDto> GetAllDtos()
        {
            return _mapper.ProjectTo<SalesOrderItemDto>(
                _context.Set<SalesOrderItem>()
                    .Include(x => x.SalesOrder)
                        .ThenInclude(x => x!.Customer)
                    .Include(x => x.Product)
                    .AsQueryable()
            );
        }

        public async Task<SalesOrderItemDto?> GetDtoByIdAsync(int id)
        {
            var salesOrderItem = await _context.Set<SalesOrderItem>()
                .Include(x => x.SalesOrder)
                    .ThenInclude(x => x!.Customer)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<SalesOrderItemDto>(salesOrderItem);
        }

        public async Task<SalesOrderItemDto> CreateAsync(SalesOrderItemDto salesOrderItemDto)
        {
            var salesOrderItem = _mapper.Map<SalesOrderItem>(salesOrderItemDto);
            await AddAsync(salesOrderItem);
            return _mapper.Map<SalesOrderItemDto>(salesOrderItem);
        }

        public async Task<SalesOrderItemDto> UpdateAsync(SalesOrderItemDto salesOrderItemDto)
        {
            var salesOrderItem = await _context.Set<SalesOrderItem>().FindAsync(salesOrderItemDto.Id);
            if (salesOrderItem == null)
            {
                throw new Exception("SalesOrderItem not found.");
            }

            _mapper.Map(salesOrderItemDto, salesOrderItem);
            await UpdateAsync(salesOrderItem);
            return _mapper.Map<SalesOrderItemDto>(salesOrderItem);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var salesOrderItem = await _context.Set<SalesOrderItem>().FindAsync(id);
            if (salesOrderItem != null)
            {
                _context.Set<SalesOrderItem>().Remove(salesOrderItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("SalesOrderItem not found.");
            }
        }

        public async Task DeleteByRowGuidAsync(Guid rowGuid)
        {
            var salesOrderItem = await _context.Set<SalesOrderItem>().FirstOrDefaultAsync(x => x.RowGuid == rowGuid);
            if (salesOrderItem != null)
            {
                _context.Set<SalesOrderItem>().Remove(salesOrderItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("SalesOrderItem not found.");
            }
        }
    }
}
