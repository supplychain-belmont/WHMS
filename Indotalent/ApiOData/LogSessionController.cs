using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Indotalent.Applications.LogSessions;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData
{
    public class LogSessionController : ODataController
    {
        private readonly LogSessionService _logSessionService;
        private readonly IMapper _mapper;

        public LogSessionController(LogSessionService logSessionService, IMapper mapper)
        {
            _logSessionService = logSessionService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<LogSessionDto> Get()
        {
            return _logSessionService.GetAllDtos();
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<SingleResult<LogSessionDto>> Get([FromODataUri] int key)
        {
            var result = await _logSessionService.GetDtoByIdAsync(key);
            return SingleResult.Create(result != null ? new[] { result }.AsQueryable() : Enumerable.Empty<LogSessionDto>().AsQueryable());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LogSessionDto logSessionDto)
        {
            var result = await _logSessionService.CreateAsync(logSessionDto);
            return Created(result);
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] LogSessionDto logSessionDto)
        {
            if (key != logSessionDto.Id)
            {
                return BadRequest();
            }

            var result = await _logSessionService.UpdateAsync(logSessionDto);
            return Updated(result);
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            await _logSessionService.DeleteByIdAsync(key);
            return NoContent();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LogLoginSession()
        {
            await _logSessionService.CollectLoginSessionDataAsync();
            return NoContent();
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> LogLogoutSession()
        {
            await _logSessionService.CollectLogoutSessionDataAsync();
            return NoContent();
        }

        [HttpPost("Purge")]
        public IActionResult Purge()
        {
            _logSessionService.PurgeAllData();
            return NoContent();
        }
    }
}
