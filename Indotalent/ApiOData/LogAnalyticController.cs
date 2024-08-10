using AutoMapper;
using Indotalent.Applications.LogAnalytics;
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
    public class LogAnalyticController : ODataController
    {
        private readonly LogAnalyticService _logAnalyticService;
        private readonly IMapper _mapper;

        public LogAnalyticController(LogAnalyticService logAnalyticService, IMapper mapper)
        {
            _logAnalyticService = logAnalyticService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<LogAnalyticDto> Get()
        {
            return _logAnalyticService.GetAllDtos();
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<SingleResult<LogAnalyticDto>> Get([FromODataUri] int key)
        {
            var result = await _logAnalyticService.GetDtoByIdAsync(key);
            return SingleResult.Create(result != null ? new[] { result }.AsQueryable() : Enumerable.Empty<LogAnalyticDto>().AsQueryable());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LogAnalyticDto logAnalyticDto)
        {
            var result = await _logAnalyticService.CreateAsync(logAnalyticDto);
            return Created(result);
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] LogAnalyticDto logAnalyticDto)
        {
            if (key != logAnalyticDto.Id)
            {
                return BadRequest();
            }

            var result = await _logAnalyticService.UpdateAsync(logAnalyticDto);
            return Updated(result);
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            await _logAnalyticService.DeleteByIdAsync(key);
            return NoContent();
        }

        [HttpPost("Collect")]
        public async Task<IActionResult> CollectAnalyticData()
        {
            await _logAnalyticService.CollectAnalyticDataAsync();
            return NoContent();
        }

        [HttpPost("Purge")]
        public IActionResult Purge()
        {
            _logAnalyticService.PurgeAllData();
            return NoContent();
        }
    }
}
