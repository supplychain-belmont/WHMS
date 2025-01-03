﻿using Indotalent.Applications.NumberSequences;
using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Entities;

namespace Indotalent.Applications.VendorContacts
{
    public class VendorContactService : Repository<VendorContact>
    {
        private readonly NumberSequenceService _numberSequenceService;

        public VendorContactService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
        }

        public override Task AddAsync(VendorContact? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(VendorContact), "", "VC");
            return base.AddAsync(entity);
        }
    }
}
