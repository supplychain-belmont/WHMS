using AutoMapper;
using Indotalent.Applications.SalesReturns;
using Indotalent.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.OData.Formatter;

namespace Indotalent.ApiOData
{
    public class SalesReturnController : ODataController
    {
        private readonly SalesReturnService _salesReturnService;
        private readonly IMapper _mapper;

        public SalesReturnController(SalesReturnService salesReturnService, IMapper mapper)
        {
            _salesReturnService = salesReturnService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<SalesReturnDto> Get()
        {
            return _salesReturnService.GetAllDtos();
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<SingleResult<SalesReturnDto>> Get([FromODataUri] int key)
        {
            var result = await _salesReturnService.GetDtoByIdAsync(key);
            return SingleResult.Create(result != null ? new[] { result }.AsQueryable() : Enumerable.Empty<SalesReturnDto>().AsQueryable());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SalesReturnDto salesReturnDto)
        {
            var result = await _salesReturnService.CreateAsync(salesReturnDto);
            return Created(result);
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] SalesReturnDto salesReturnDto)
        {
            if (key != salesReturnDto.Id)
            {
                return BadRequest();
            }

            var result = await _salesReturnService.UpdateAsync(salesReturnDto);
            return Updated(result);
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            await _salesReturnService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
