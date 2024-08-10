using AutoMapper;
using Indotalent.Applications.LogErrors;
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
    public class LogErrorController : ODataController
    {
        private readonly LogErrorService _logErrorService;
        private readonly IMapper _mapper;

        public LogErrorController(LogErrorService logErrorService, IMapper mapper)
        {
            _logErrorService = logErrorService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<LogErrorDto> Get()
        {
            return _logErrorService.GetAllDtos();
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<SingleResult<LogErrorDto>> Get([FromODataUri] int key)
        {
            var result = await _logErrorService.GetDtoByIdAsync(key);
            return SingleResult.Create(result != null ? new[] { result }.AsQueryable() : Enumerable.Empty<LogErrorDto>().AsQueryable());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LogErrorDto logErrorDto)
        {
            var result = await _logErrorService.CreateAsync(logErrorDto);
            return Created(result);
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] LogErrorDto logErrorDto)
        {
            if (key != logErrorDto.Id)
            {
                return BadRequest();
            }

            var result = await _logErrorService.UpdateAsync(logErrorDto);
            return Updated(result);
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            await _logErrorService.DeleteByIdAsync(key);
            return NoContent();
        }

        [HttpPost("Collect")]
        public async Task<IActionResult> CollectErrorData([FromBody] LogErrorDto logErrorDto)
        {
            await _logErrorService.CollectErrorDataAsync(logErrorDto.ExceptionMessage, logErrorDto.StackTrace, logErrorDto.AdditionalInfo);
            return NoContent();
        }

        [HttpPost("Purge")]
        public IActionResult Purge()
        {
            _logErrorService.PurgeAllData();
            return NoContent();
        }
    }
}
