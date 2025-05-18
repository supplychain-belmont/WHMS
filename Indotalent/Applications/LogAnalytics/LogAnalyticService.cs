using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using AutoMapper;

using DeviceDetectorNET;

using Indotalent.Data;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;
using Indotalent.Infrastructures.Repositories;

using Microsoft.EntityFrameworkCore;

using UAParser;

namespace Indotalent.Applications.LogAnalytics
{
    public class LogAnalyticService : Repository<LogAnalytic>
    {
        private readonly IMapper _mapper;

        public LogAnalyticService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            IMapper mapper) :
                base(
                    context,
                    httpContextAccessor,
                    auditColumnTransformer)
        {
            _mapper = mapper;
        }

        public IQueryable<LogAnalyticDto> GetAllDtos()
        {
            return _mapper.ProjectTo<LogAnalyticDto>(_context.LogAnalytic.AsQueryable());
        }

        public async Task<LogAnalyticDto?> GetDtoByIdAsync(int id)
        {
            var logAnalytic = await GetByIdAsync(id);
            return _mapper.Map<LogAnalyticDto>(logAnalytic);
        }

        public async Task<LogAnalyticDto> CreateAsync(LogAnalyticDto logAnalyticDto)
        {
            var logAnalytic = _mapper.Map<LogAnalytic>(logAnalyticDto);
            await AddAsync(logAnalytic);
            return _mapper.Map<LogAnalyticDto>(logAnalytic);
        }

        public async Task<LogAnalyticDto> UpdateAsync(LogAnalyticDto logAnalyticDto)
        {
            var logAnalytic = await GetByIdAsync(logAnalyticDto.Id);
            if (logAnalytic == null)
            {
                throw new Exception("LogAnalytic not found.");
            }

            _mapper.Map(logAnalyticDto, logAnalytic);
            await UpdateAsync(logAnalytic);
            return _mapper.Map<LogAnalyticDto>(logAnalytic);
        }

        public new async Task DeleteByIdAsync(int id)
        {
            await base.DeleteByIdAsync(id);
        }

        public async Task CollectAnalyticDataAsync()
        {
            var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            var userId = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userAgentString = _httpContextAccessor?.HttpContext?.Request.Headers["User-Agent"].ToString();
            var userIpAddress = _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString();
            var url = _httpContextAccessor?.HttpContext?.Request.Path.ToString();

            var deviceDetector = new DeviceDetector(userAgentString);
            deviceDetector.Parse();
            var deviceType = deviceDetector.GetDeviceName();

            var uaParser = Parser.GetDefault();
            var clientInfo = uaParser.Parse(userAgentString);
            var browserName = clientInfo?.UA?.Family;
            var browserVersion = clientInfo?.UA?.Major;

            var logAnalytic = new LogAnalytic
            {
                UserId = userId,
                UserName = userName,
                IPAddress = userIpAddress,
                Url = url,
                Device = deviceType,
                GeographicLocation = "",
                Browser = $"{browserName} {browserVersion}"
            };

            await AddAsync(logAnalytic);
        }

        public void PurgeAllData()
        {
            _context.LogAnalytic.RemoveRange(_context.LogAnalytic);
            _context.SaveChanges();
        }
    }
}
