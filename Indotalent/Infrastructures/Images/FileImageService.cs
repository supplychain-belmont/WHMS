using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Contracts;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Infrastructures.Images
{
    public class FileImageService : IFileImageService
    {
        private readonly ApplicationDbContext _context;

        public FileImageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<FileImage> GetAll()
        {
            return _context.FileImages.ApplyIsDeletedFilter().AsNoTracking();
        }

        public async Task<FileImage?> GetByRowGuidAsync(Guid? rowGuid)
        {
            var entity = await _context.FileImages.ApplyIsNotDeletedFilter()
                .FirstOrDefaultAsync(x => x.Id == rowGuid);
            if (entity == null)
            {
                throw new Exception("Unable to process, entity is null");
            }

            return entity;
        }

        public async Task AddAsync(FileImage fileImage)
        {
            if (fileImage == null)
            {
                throw new Exception("Unable to process, entity is null");
            }

            _context.FileImages.Add(fileImage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FileImage fileImage)
        {
            if (fileImage == null)
            {
                throw new Exception("Unable to process, entity is null");
            }

            _context.FileImages.Update(fileImage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByRowGuidAsync(Guid? rowGuid)
        {
            if (!rowGuid.HasValue)
            {
                throw new Exception("Unable to process, id is null");
            }

            var entity = await _context.FileImages.ApplyIsNotDeletedFilter()
                .FirstOrDefaultAsync(x => x.Id == rowGuid);

            if (entity != null)
            {
                if (entity is IHasAudit auditEntity)
                {
                    auditEntity.UpdatedAtUtc = DateTime.Now;
                }

                if (entity is IHasSoftDelete softDeleteEntity)
                {
                    softDeleteEntity.IsNotDeleted = false;
                    _context.SetModifiedState(entity);
                }
                else
                {
                    _context.FileImages.Remove(entity);
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Guid> UploadImageAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            if (!IsImageFile(file))
            {
                throw new ArgumentException("Invalid file type. Only image files are allowed.");
            }

            if (file.Length > 5 * 1024 * 1024) // 5 MB in bytes
            {
                throw new ArgumentException("File size exceeds the maximum allowed size of 5 MB.");
            }

            var image = new FileImage { Id = Guid.NewGuid(), OriginalFileName = file.FileName };

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                image.ImageData = memoryStream.ToArray();
            }

            await _context.FileImages.AddAsync(image);

            return image.RowGuid;
        }

        public async Task<Guid> UpdateImageAsync(Guid? imageId, IFormFile? newFile)
        {
            if (imageId == null)
            {
                throw new ArgumentException("Invalid imageId");
            }

            if (newFile == null || newFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            if (!IsImageFile(newFile))
            {
                throw new ArgumentException("Invalid file type. Only image files are allowed.");
            }

            if (newFile.Length > 5 * 1024 * 1024) // 5 MB in bytes
            {
                throw new ArgumentException("File size exceeds the maximum allowed size of 5 MB.");
            }

            var existingImage = await GetByRowGuidAsync(imageId);

            if (existingImage == null)
            {
                throw new ArgumentException($"Image with ID {imageId} not found");
            }

            // Update the existing image with the new data
            existingImage.OriginalFileName = newFile.FileName;

            using (var memoryStream = new MemoryStream())
            {
                await newFile.CopyToAsync(memoryStream);
                existingImage.ImageData = memoryStream.ToArray();
            }

            await UpdateAsync(existingImage);

            return existingImage.Id;
        }

        public async Task<FileImage> GetImageAsync(Guid? id)
        {
            if (id == null)
            {
                id = new Guid(Guid.Empty.ToString());
            }

            var fileImage = await GetByRowGuidAsync(id);

            if (fileImage != null)
            {
                return fileImage;
            }

            var defaultImagePath = Path.Combine("wwwroot", "noimage.png");

            fileImage = new FileImage
            {
                Id = Guid.Empty,
                OriginalFileName = "NoImage.png",
                ImageData = File.ReadAllBytes(defaultImagePath)
            };

            return fileImage;
        }

        public string GetImageUrlFromImageId(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = Guid.Empty.ToString();
            }

            var image = GetImage(new Guid(id));
            var url = $"data:image/png;base64,{Convert.ToBase64String(image.ImageData)}";
            return url;
        }

        public async Task<string> GetImageUrlFromImageIdAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = Guid.Empty.ToString();
            }

            var image = await GetImageAsync(new Guid(id));
            var url = $"data:image/png;base64,{Convert.ToBase64String(image.ImageData)}";
            return url;
        }

        private bool IsImageFile(IFormFile file)
        {
            var allowedImageTypes = new[]
            {
                "image/jpeg", "image/jpg", "image/png", "image/gif", "image/bmp", "image/webp"
            };
            return allowedImageTypes.Contains(file.ContentType);
        }

        public FileImage GetImage(Guid? id)
        {
            if (id == null)
            {
                id = new Guid(Guid.Empty.ToString());
            }

            var fileImage = GetByRowGuidAsync(id).Result;

            if (fileImage == null)
            {
                var defaultImagePath = Path.Combine("wwwroot", "noimage.png");

                fileImage = new FileImage
                {
                    Id = Guid.Empty,
                    OriginalFileName = "NoImage.png",
                    ImageData = File.ReadAllBytes(defaultImagePath)
                };
            }

            return fileImage;
        }
    }
}
