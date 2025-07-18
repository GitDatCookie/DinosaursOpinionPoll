using AI_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AI_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly string uploadFolder;

        public ImageController()
        {
            uploadFolder = Environment.GetEnvironmentVariable("IMAGE_UPLOAD_PATH") 
                ?? throw new InvalidOperationException("IMAGE_UPLOAD_PATH not set");
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("No file provided.");
            }

            Directory.CreateDirectory(uploadFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var filePath = Path.Combine(uploadFolder, fileName);

            await using var stream = System.IO.File.OpenWrite(filePath);
            await imageFile.CopyToAsync(stream);

            return Ok(new { FileName = fileName, Url = $"/uploads/images/{fileName}" });
        }
    }
}
