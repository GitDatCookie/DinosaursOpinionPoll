using AI_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AI_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly string uploadFolder;

        public VideoController()
        {
            uploadFolder = Environment.GetEnvironmentVariable("VIDEO_UPLOAD_PATH")
                           ?? throw new InvalidOperationException("VIDEO_UPLOAD_PATH not set");
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile videoFile)
        {
            if (videoFile == null || videoFile.Length == 0)
                return BadRequest("No file provided.");

            Directory.CreateDirectory(uploadFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(videoFile.FileName)}";
            var filePath = Path.Combine(uploadFolder, fileName);

            await using var stream = System.IO.File.Create(filePath);
            await videoFile.CopyToAsync(stream);

            var url = $"/uploads/videos/{fileName}";
            return Ok(new { FileName = fileName, Url = url });
        }

    }
}
