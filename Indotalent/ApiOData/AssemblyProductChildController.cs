using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.Products;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class AssemblyProductChildController : ODataController
    {
        private readonly AssemblyProductService _assemblyProductService;
        private readonly IMapper _mapper;

        public AssemblyProductChildController(AssemblyProductService assemblyProductService, IMapper mapper)
        {
            _assemblyProductService = assemblyProductService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<AssemblyProductDto> Get()
        {
            return _assemblyProductService
                .GetAll()
                .ProjectTo<AssemblyProductDto>(_mapper.ConfigurationProvider);
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<ActionResult<AssemblyProductDto>> Get([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _assemblyProductService
                .GetByIdAsync(key, x => x.Assembly, x => x.Product)
                .FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<AssemblyProductDto>(product);
            return Ok(dto);
        }

        public async Task<ActionResult<AssemblyProductDto>> Post([FromBody] AssemblyProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<AssemblyProduct>(productDto);

            await _assemblyProductService.AddAsync(product);
            var createdDto = _mapper.Map<AssemblyProductDto>(product);

            return Created(createdDto);
        }

        public async Task<ActionResult<AssemblyProductDto>> Put([FromRoute] int key,
            [FromBody] AssemblyProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentProduct = await _assemblyProductService.GetByIdAsync(key);
            if (currentProduct == null)
            {
                return NotFound();
            }

            _mapper.Map(productDto, currentProduct);
            await _assemblyProductService.UpdateAsync(currentProduct);
            return NoContent();
        }

        public async Task<IActionResult> Patch([FromRoute] int key, [FromBody] Delta<AssemblyProductDto> patchDoc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var current = await _assemblyProductService.GetByIdAsync(key);
            if (current == null)
            {
                return NotFound();
            }


            var dto = _mapper.Map<AssemblyProductDto>(current);
            patchDoc.Patch(dto);

            var entity = _mapper.Map(dto, current);

            await _assemblyProductService.UpdateAsync(entity);
            return Updated(_mapper.Map<PurchaseOrderDto>(entity));
        }

        public async Task<ActionResult> Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _assemblyProductService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
