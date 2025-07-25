﻿using Microsoft.AspNetCore.Http;

namespace Indotalent.Persistence.Docs
{
    public interface IFileDocumentService
    {
        Task<Guid> UploadDocumentAsync(IFormFile? file);
        Task<Guid> UpdateDocumentAsync(Guid? documentId, IFormFile? newFile);
        Task<FileDocument> GetDocumentAsync(Guid? id);
    }
}
