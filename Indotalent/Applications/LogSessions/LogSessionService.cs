using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using AutoMapper;

using Indotalent.Data;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;
using Indotalent.Infrastructures.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.LogSessions
{
    public class LogSessionService : Repository<LogSession>
    {
        private readonly IMapper _mapper;

        public LogSessionService(
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

        public IQueryable<LogSessionDto> GetAllDtos()
        {
            return _mapper.ProjectTo<LogSessionDto>(_context.Set<LogSession>().AsQueryable());
        }

        public async Task<LogSessionDto?> GetDtoByIdAsync(int id)
        {
            var logSession = await _context.Set<LogSession>().FindAsync(id);
            return _mapper.Map<LogSessionDto>(logSession);
        }

        public async Task<LogSessionDto> CreateAsync(LogSessionDto logSessionDto)
        {
            var logSession = _mapper.Map<LogSession>(logSessionDto);
            await AddAsync(logSession);
            return _mapper.Map<LogSessionDto>(logSession);
        }

        public async Task<LogSessionDto> UpdateAsync(LogSessionDto logSessionDto)
        {
            var logSession = await _context.Set<LogSession>().FindAsync(logSessionDto.Id);
            if (logSession == null)
            {
                throw new Exception("LogSession not found.");
            }

            _mapper.Map(logSessionDto, logSession);
            await UpdateAsync(logSession);
            return _mapper.Map<LogSessionDto>(logSession);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await base.DeleteByIdAsync(id);
        }

        public async Task CollectLoginSessionDataAsync()
        {
            var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            var userId = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var ipAddress = _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            var data = new LogSession
            {
                UserId = userId,
                UserName = userName,
                IPAddress = ipAddress,
                Action = "Login"
            };

            await AddAsync(data);
        }

        public async Task CollectLogoutSessionDataAsync()
        {
            var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            var userId = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var ipAddress = _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            var data = new LogSession
            {
                UserId = userId,
                UserName = userName,
                IPAddress = ipAddress,
                Action = "Logout"
            };

            await AddAsync(data);
        }

        public void PurgeAllData()
        {
            _context.LogSession.RemoveRange(_context.LogSession);
            _context.SaveChanges();
        }
    }
}
