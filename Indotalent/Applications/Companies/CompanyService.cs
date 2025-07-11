﻿using Indotalent.Data;
using Indotalent.Domain.Entities;
using Indotalent.Infrastructures.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.Companies
{
    public class CompanyService : Repository<Company>
    {
        public CompanyService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer) :
                base(
                    context,
                    httpContextAccessor,
                    auditColumnTransformer)
        {
        }

        public async Task<Company?> GetDefaultCompanyAsync()
        {
            return await _context.Company.FirstOrDefaultAsync();
        }

    }
}
