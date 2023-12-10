using ImageResizer.DTOs;
using ImageResizer.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageResizer.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost("CompressImage")]
        public async Task<IActionResult> CompressImage([FromBody] ImageCompressionRequest request)
        {
            try
            {
                string inputImagePath = request.InputImagePath;
                string outputImagePath = request.OutputImagePath;

                await _imageRepository.CompressAndSaveImageAsync(inputImagePath, outputImagePath);

                return Ok("Image compressed and saved successfully!");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error in image compression endpoint: {ex.Message}");
                return StatusCode(500, $"Error in image compression endpoint: {ex.Message}");
            }
        }
    }
}
