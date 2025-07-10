using Indotalent.Data;
using Indotalent.Domain.Contracts;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Infrastructures.Repositories
{
    public class AuditColumnTransformer : IAuditColumnTransformer
    {
        private readonly string _dateFormat = "yyyy-MM-dd HH:ss";

        public async Task TransformAsync(IHasAudit entity, ApplicationDbContext context)
        {
            if (entity != null)
            {
                if (!String.IsNullOrEmpty(entity?.CreatedByUserId) && entity?.CreatedAtUtc is not null)
                {
                    var selectedCompanyId = 1;
                    var company = await context.Company.FirstOrDefaultAsync(x => x.Id == selectedCompanyId);
                    var localDateTime = ConvertUtcToTimeZone(entity.CreatedAtUtc.Value,
                        company?.TimeZone ?? "Pacific Standard Time");
                    entity.CreatedAtString = localDateTime.ToString(_dateFormat);
                }

                if (!String.IsNullOrEmpty(entity?.UpdatedByUserId) && entity?.UpdatedAtUtc is not null)
                {
                    var selectedCompanyId = 1;
                    var company = await context.Company.FirstOrDefaultAsync(x => x.Id == selectedCompanyId);
                    var localDateTime = ConvertUtcToTimeZone(entity.UpdatedAtUtc.Value,
                        company?.TimeZone ?? "Pacific Standard Time");
                    entity.UpdatedAtString = localDateTime.ToString(_dateFormat);
                }
            }
        }

        private DateTime ConvertUtcToTimeZone(DateTime utcDateTime, string timeZoneId)
        {
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timeZone);
        }
    }
}
