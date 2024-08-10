using AutoMapper;
using Indotalent.Data;
using Indotalent.DTOs;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Indotalent.Applications.LogErrors
{
    public class LogErrorService : Repository<LogError>
    {
        private readonly IMapper _mapper;

        public LogErrorService(
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

        public IQueryable<LogErrorDto> GetAllDtos()
        {
            return _mapper.ProjectTo<LogErrorDto>(_context.Set<LogError>().AsQueryable());
        }

        public async Task<LogErrorDto?> GetDtoByIdAsync(int id)
        {
            var logError = await _context.Set<LogError>().FindAsync(id);
            return _mapper.Map<LogErrorDto>(logError);
        }

        public async Task<LogErrorDto> CreateAsync(LogErrorDto logErrorDto)
        {
            var logError = _mapper.Map<LogError>(logErrorDto);
            await AddAsync(logError);
            return _mapper.Map<LogErrorDto>(logError);
        }

        public async Task<LogErrorDto> UpdateAsync(LogErrorDto logErrorDto)
        {
            var logError = await _context.Set<LogError>().FindAsync(logErrorDto.Id);
            if (logError == null)
            {
                throw new Exception("LogError not found.");
            }

            _mapper.Map(logErrorDto, logError);
            await UpdateAsync(logError);
            return _mapper.Map<LogErrorDto>(logError);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await base.DeleteByIdAsync(id);
        }

        public async Task CollectErrorDataAsync(string? exceptionMessage, string? stackTrace, string? additionalInfo)
        {
            var data = new LogError
            {
                ExceptionMessage = exceptionMessage,
                StackTrace = stackTrace,
                AdditionalInfo = additionalInfo
            };

            await AddAsync(data);
        }

        public void PurgeAllData()
        {
            _context.LogError.RemoveRange(_context.LogError);
            _context.SaveChanges();
        }
    }
}
