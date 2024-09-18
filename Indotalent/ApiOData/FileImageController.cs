using System;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.DTOs;
using Indotalent.Infrastructures.Images;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData
{
    public class FileImageController : ODataController
    {
        private readonly IFileImageService _fileImageService;
        private readonly IMapper _mapper;

        public FileImageController(IFileImageService fileImageService, IMapper mapper)
        {
            _fileImageService = fileImageService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<FileImageDto> Get()
        {
            return _fileImageService.GetAll().ProjectTo<FileImageDto>(_mapper.ConfigurationProvider);
        }

        public async Task<ActionResult<FileImageDto>> Get([FromRoute] Guid key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var fileImage = await _fileImageService.GetByRowGuidAsync(key);

            if (fileImage == null) return NotFound();

            var fileImageDto = _mapper.Map<FileImageDto>(fileImage);
            return Ok(fileImageDto);
        }

        [HttpPost]
        public async Task<ActionResult<FileImageDto>> Post(IFormFile file)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (file == null || file.Length == 0) return BadRequest("Invalid file");

            var imageId = await _fileImageService.UploadImageAsync(file);

            var fileImage = await _fileImageService.GetByRowGuidAsync(imageId);

            if (fileImage == null) return NotFound();

            var fileImageDto = _mapper.Map<FileImageDto>(fileImage);
            return Created(fileImageDto);
        }

        [HttpPut("{key}")]
        public async Task<ActionResult<FileImageDto>> Put([FromRoute] Guid key, IFormFile newFile)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingImage = await _fileImageService.GetByRowGuidAsync(key);

            if (existingImage == null) return NotFound();

            var updatedImageId = await _fileImageService.UpdateImageAsync(key, newFile);

            var updatedImage = await _fileImageService.GetByRowGuidAsync(updatedImageId);

            if (updatedImage == null) return NotFound();

            var fileImageDto = _mapper.Map<FileImageDto>(updatedImage);
            return Ok(fileImageDto);
        }

        [HttpDelete("{key}")]
        public async Task<ActionResult> Delete([FromRoute] Guid key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var fileImage = await _fileImageService.GetByRowGuidAsync(key);

            if (fileImage == null) return NotFound();

            await _fileImageService.DeleteByRowGuidAsync(key);

            return NoContent();
        }

        [HttpGet("{key}/Download")]
        public async Task<IActionResult> DownloadImage([FromRoute] Guid key)
        {
            var fileImage = await _fileImageService.GetByRowGuidAsync(key);

            if (fileImage == null) return NotFound();

            return File(fileImage.ImageData, "application/octet-stream", fileImage.OriginalFileName);
        }

        [HttpGet("GetImageUrl/{id}")]
        public async Task<IActionResult> GetImageUrl([FromRoute] string id)
        {
            var imageUrl = await _fileImageService.GetImageUrlFromImageIdAsync(id);

            return Ok(imageUrl);
        }
    }
}
