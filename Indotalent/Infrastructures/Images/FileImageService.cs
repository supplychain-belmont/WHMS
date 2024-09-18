using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.Http;

namespace Indotalent.Infrastructures.Images
{
    public class FileImageService : IFileImageService
    {
        private readonly Repository<FileImage> _fileImageRepository;

        public FileImageService(Repository<FileImage> fileImageRepository)
        {
            _fileImageRepository = fileImageRepository;
        }

        public IQueryable<FileImage> GetAll()
        {
            return _fileImageRepository.GetAll();
        }

        public async Task<FileImage?> GetByRowGuidAsync(Guid? rowGuid)
        {
            return await _fileImageRepository.GetByRowGuidAsync(rowGuid);
        }

        public async Task AddAsync(FileImage fileImage)
        {
            await _fileImageRepository.AddAsync(fileImage);
        }

        public async Task UpdateAsync(FileImage fileImage)
        {
            await _fileImageRepository.UpdateAsync(fileImage);
        }

        public async Task DeleteByRowGuidAsync(Guid? rowGuid)
        {
            await _fileImageRepository.DeleteByRowGuidAsync(rowGuid);
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

            var image = new FileImage
            {
                Id = Guid.NewGuid(),
                OriginalFileName = file.FileName
            };

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                image.ImageData = memoryStream.ToArray();
            }

            await _fileImageRepository.AddAsync(image);

            return image.Id;
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

            var existingImage = await _fileImageRepository.GetByRowGuidAsync(imageId);

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

            await _fileImageRepository.UpdateAsync(existingImage);

            return existingImage.Id;
        }

        public async Task<FileImage> GetImageAsync(Guid? id)
        {
            if (id == null)
            {
                id = new Guid(Guid.Empty.ToString());
            }

            var fileImage = await _fileImageRepository.GetByRowGuidAsync(id);

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
            var allowedImageTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/bmp", "image/webp" };
            return allowedImageTypes.Contains(file.ContentType);
        }
        public FileImage GetImage(Guid? id)
        {
            if (id == null)
            {
                id = new Guid(Guid.Empty.ToString());
            }

            var fileImage = _fileImageRepository.GetByRowGuidAsync(id).Result;

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
