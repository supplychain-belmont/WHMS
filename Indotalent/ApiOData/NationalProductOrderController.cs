using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Indotalent.Applications.NationalProductOrders;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData
{
    public class NationalProductOrderController : ODataController
    {
        private readonly NationalProductOrderService _nationalProductOrderService;
        private readonly IMapper _mapper;

        public NationalProductOrderController(
            IMapper mapper,
            NationalProductOrderService nationalProductOrderService)
        {
            _mapper = mapper;
            _nationalProductOrderService = nationalProductOrderService;
        }

        [EnableQuery]
        public IQueryable<NationalProductOrderDto> Get()
        {
            return _nationalProductOrderService
                .GetAll()
                .Select(x => _mapper.Map<NationalProductOrderDto>(x));
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public SingleResult<NationalProductOrderDto> Get([FromODataUri] int key)
        {
            return SingleResult.Create(_nationalProductOrderService
                .GetAll()
                .Where(x => x.Id == key)
                .Select(x => _mapper.Map<NationalProductOrderDto>(x)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NationalProductOrderDto nationalProductOrder)
        {
            try
            {
                var entity = _mapper.Map<NationalProductOrder>(nationalProductOrder);
                await _nationalProductOrderService.AddAsync(entity);
                var dto = _mapper.Map<NationalProductOrderDto>(entity);

                return Created("NationalProductOrder", dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<NationalProductOrderDto> delta)
        {
            try
            {
                var nationalProductOrder = await _nationalProductOrderService.GetByIdAsync(key);
                if (nationalProductOrder == null)
                {
                    return NotFound();
                }

                var dto = _mapper.Map<NationalProductOrderDto>(nationalProductOrder);
                delta.Patch(dto);
                var entity = _mapper.Map(dto, nationalProductOrder);
                await _nationalProductOrderService.UpdateAsync(entity);

                return Ok(_mapper.Map<NationalProductOrderDto>(entity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            try
            {
                var nationalProductOrder = await _nationalProductOrderService.GetByIdAsync(key);
                if (nationalProductOrder == null)
                {
                    return BadRequest();
                }

                await _nationalProductOrderService.DeleteByIdAsync(nationalProductOrder.Id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
