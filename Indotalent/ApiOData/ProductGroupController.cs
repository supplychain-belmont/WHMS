using AutoMapper;

using Indotalent.Applications.ProductGroups;
using Indotalent.DTOs;
using Indotalent.Domain.Entities;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData
{
    public class ProductGroupController : ODataController
    {
        private readonly ProductGroupService _productGroupService;
        private readonly IMapper _mapper;

        public ProductGroupController(ProductGroupService productGroupService, IMapper mapper)
        {
            _productGroupService = productGroupService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<ProductGroupDto> Get()
        {
            return _productGroupService
                .GetAll()
                .Select(rec => new ProductGroupDto
                {
                    Id = rec.Id,
                    RowGuid = rec.RowGuid,
                    Name = rec.Name,
                    Description = rec.Description,
                    CreatedAtUtc = rec.CreatedAtUtc
                });
        }
        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<ActionResult<ProductGroupDto>> Get([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productGroup = await _productGroupService.GetByIdAsync(key);
            if (productGroup == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<ProductGroupDto>(productGroup);
            return Ok(dto);
        }

        public async Task<ActionResult<ProductGroupDto>> Post([FromBody] ProductGroupDto productGroupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productGroup = _mapper.Map<ProductGroup>(productGroupDto);
            await _productGroupService.AddAsync(productGroup);
            var createdDto = _mapper.Map<ProductGroupDto>(productGroup);

            return Created(createdDto);
        }

        public async Task<ActionResult<ProductGroupDto>> Put([FromRoute] int key, [FromBody] ProductGroupDto productGroupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentProductGroup = await _productGroupService.GetByIdAsync(key);
            if (currentProductGroup == null)
            {
                return NotFound();
            }

            _mapper.Map(productGroupDto, currentProductGroup);
            await _productGroupService.UpdateAsync(currentProductGroup);
            return NoContent();
        }

        public async Task<ActionResult> Patch([FromRoute] int key, [FromBody] JsonPatchDocument<ProductGroupDto> patchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentProductGroup = await _productGroupService.GetByIdAsync(key);
            if (currentProductGroup == null)
            {
                return NotFound();
            }

            var productGroupDto = _mapper.Map<ProductGroupDto>(currentProductGroup);
            patchDto.ApplyTo(productGroupDto, (error) => ModelState.AddModelError(string.Empty, error.ErrorMessage));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(productGroupDto, currentProductGroup);
            await _productGroupService.UpdateAsync(currentProductGroup);

            return NoContent();
        }


        public async Task<ActionResult> Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productGroupService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
