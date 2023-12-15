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

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] string FileName, IFormFile Data)
        {
            try
            {
                var optimizedImage = await _imageRepository.OptimizeImageAsync(Data);
                await _imageRepository.UploadImageToFtpAsync(FileName, optimizedImage);

                return Ok("Image uploaded successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("compress")]
        public async Task<IActionResult> CompressImage([FromBody] ImageRequest request)
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
