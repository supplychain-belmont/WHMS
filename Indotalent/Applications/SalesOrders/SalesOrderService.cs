using AutoMapper;

using Indotalent.Data;
using Indotalent.DTOs;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Contracts;
using Indotalent.Applications.NumberSequences;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.SalesOrders
{
    public class SalesOrderService : Repository<SalesOrder>
    {
        private readonly IMapper _mapper;
        private readonly NumberSequenceService _numberSequenceService;

        public SalesOrderService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            IMapper mapper) :
                base(
                    context,
                    httpContextAccessor,
                    auditColumnTransformer)
        {
            _mapper = mapper;
            _numberSequenceService = numberSequenceService;
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
            salesOrder.Number = _numberSequenceService.GenerateNumber(nameof(SalesOrder), "", "SO");
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
        public async Task<SalesOrderDto?> PatchAsync(int id, JsonPatchDocument<SalesOrderDto> patchDoc)
        {
            var salesOrder = await _context.Set<SalesOrder>()
                .Include(x => x.Customer)
                .Include(x => x.Tax)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (salesOrder == null)
            {
                return null;
            }

            var salesOrderDto = _mapper.Map<SalesOrderDto>(salesOrder);
            patchDoc.ApplyTo(salesOrderDto);

            _mapper.Map(salesOrderDto, salesOrder);

            await UpdateAsync(salesOrder);

            return _mapper.Map<SalesOrderDto>(salesOrder);
        }


    }
}
