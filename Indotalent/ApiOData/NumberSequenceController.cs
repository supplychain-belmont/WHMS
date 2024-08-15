using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.NumberSequences;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData
{
    public class NumberSequenceController : ODataController
    {
        private readonly NumberSequenceService _numberSequenceService;
        private readonly IMapper _mapper;

        public NumberSequenceController(NumberSequenceService numberSequenceService, IMapper mapper)
        {
            _numberSequenceService = numberSequenceService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<NumberSequenceDto> Get()
        {
            return _numberSequenceService
                .GetAll()
                .ProjectTo<NumberSequenceDto>(_mapper.ConfigurationProvider);
        }

        public async Task<ActionResult<NumberSequenceDto>> Get([FromRoute] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var numberSequence = await _numberSequenceService.GetByIdAsync(key);
            return Ok(_mapper.Map<NumberSequenceDto>(numberSequence));
        }

        public async Task<ActionResult<NumberSequenceDto>> Post([FromBody] NumberSequenceDto numberSequenceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var numberSequence = _mapper.Map<NumberSequence>(numberSequenceDto);
            await _numberSequenceService.AddAsync(numberSequence);
            return Created();
        }

        public async Task<ActionResult<NumberSequenceDto>> Put([FromRoute] int key,
            [FromBody] NumberSequenceDto numberSequenceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currenNumberSequence = await _numberSequenceService.GetByIdAsync(key);
            if (currenNumberSequence == null)
            {
                return NotFound();
            }

            _mapper.Map(numberSequenceDto, currenNumberSequence);
            await _numberSequenceService.UpdateAsync(currenNumberSequence);
            return NoContent();
        }

        public async Task<ActionResult<NumberSequenceDto>> Patch([FromRoute] int key,
            [FromBody] Delta<NumberSequenceDto> numberSequenceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentNumberSequence = await _numberSequenceService.GetByIdAsync(key);
            if (currentNumberSequence == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<NumberSequenceDto>(currentNumberSequence);
            numberSequenceDto.Patch(dto);

            var entity = _mapper.Map(dto, currentNumberSequence);

            await _numberSequenceService.UpdateAsync(entity);
            return Updated(_mapper.Map<NumberSequenceDto>(entity));
        }

        public async Task<ActionResult> Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _numberSequenceService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
