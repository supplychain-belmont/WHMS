using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.Taxes;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData
{
    public class TaxController : ODataController
    {
        private readonly TaxService _taxService;
        private readonly IMapper _mapper;

        public TaxController(TaxService taxService, IMapper mapper)
        {
            _taxService = taxService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<TaxDto> Get()
        {
            return _taxService
                .GetAll()
                .ProjectTo<TaxDto>(_mapper.ConfigurationProvider);
        }

        public async Task<ActionResult<TaxDto>> Get([FromRoute] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tax = await _taxService.GetByIdAsync(key);
            return Ok(_mapper.Map<TaxDto>(tax));
        }

        public async Task<ActionResult<TaxDto>> Post([FromBody] TaxDto taxDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tax = _mapper.Map<Tax>(taxDto);
            await _taxService.AddAsync(tax);
            return Created();
        }

        public async Task<ActionResult<TaxDto>> Put([FromRoute] int key, [FromBody] TaxDto taxDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentTax = await _taxService.GetByIdAsync(key);
            if (currentTax == null)
            {
                return NotFound();
            }

            _mapper.Map(taxDto, currentTax);
            await _taxService.UpdateAsync(currentTax);
            return NoContent();
        }

        public async Task<ActionResult<TaxDto>> Patch([FromRoute] int key,
            [FromBody] Delta<TaxDto> taxDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentTax = await _taxService.GetByIdAsync(key);
            if (currentTax == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<TaxDto>(currentTax);
            taxDto.Patch(dto);

            var entity = _mapper.Map(dto, currentTax);

            await _taxService.UpdateAsync(entity);
            return Updated(_mapper.Map<TaxDto>(entity));
        }

        public async Task<ActionResult> Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _taxService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
