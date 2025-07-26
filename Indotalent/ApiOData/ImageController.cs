using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;

namespace Indotalent.ApiOData;

[Route("upload")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;

    public ImageController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpGet]
    [Route("/{folder}/{name}")]
    public Task<IActionResult> Get(string folder, string name)
    {
        var filePath = Path.Combine(_environment.WebRootPath, "uploads", folder, $"{name}.jpg");

        if (!System.IO.File.Exists(filePath))
        {
            return Task.FromResult<IActionResult>(NotFound("File not found."));
        }

        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return Task.FromResult<IActionResult>(File(fileStream, "image/jpeg"));
    }

    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] ImageDto image)
    {
        var file = image.File;
        var name = image.Name;
        var folder = image.Folder;

        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var uploadFolder = Path.Combine(_environment.WebRootPath, "uploads", folder!);
        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        var filePath = Path.Combine(uploadFolder, $"{name}.jpg");

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("File not found.");
        }

        var fileUrl = Url.Content($"~/uploads/{folder}/{name}");
        return CreatedAtAction(nameof(Upload), new { name }, new { fileUrl });
    }
}
