using Microsoft.AspNetCore.Http;

namespace Indotalent.Persistence.Images
{
    public interface IFileImageService
    {
        IQueryable<FileImage> GetAll();
        Task<FileImage?> GetByRowGuidAsync(Guid? rowGuid);
        Task AddAsync(FileImage fileImage);
        Task UpdateAsync(FileImage fileImage);
        Task DeleteByRowGuidAsync(Guid? rowGuid);
        Task<Guid> UploadImageAsync(IFormFile? file);
        Task<Guid> UpdateImageAsync(Guid? imageId, IFormFile? newFile);
        Task<FileImage> GetImageAsync(Guid? id);
        string GetImageUrlFromImageId(string? id);
        Task<string> GetImageUrlFromImageIdAsync(string? id);
        string GetImageContentType(string fileImageOriginalFileName);
    }
}
