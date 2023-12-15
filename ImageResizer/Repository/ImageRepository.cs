using FluentFTP;
using ImageResizer.Repository.Interfaces;
using TinifyAPI;

namespace ImageResizer.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly FtpClient _ftpClient;
        //private readonly string ApiKey;


        public ImageRepository(string tinyPngApiKey, string ftpHost, string ftpUsername, string ftpPassword)
        {
            _ftpClient = new FtpClient(ftpHost, ftpUsername, ftpPassword);
            Tinify.Key = tinyPngApiKey;

        }

        public async Task<byte[]> OptimizeImageAsync(IFormFile imageData)
        {

            try
            {
                using (var stream = new MemoryStream())
                {
                    await imageData.CopyToAsync(stream);
                    var optimizedImage = await Tinify.FromBuffer(stream.ToArray()).ToBuffer();
                    return optimizedImage;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error optimizing image: {ex.Message}");
                throw; // Rethrow the exception or handle it as appropriate for your application
            }
        }

        public async Task UploadImageToFtpAsync(string fileName, byte[] imageData)
        {
            try
            {

                // Specify the directory on the server to save the image
                var uploadDirectory = @"E\CompresseImages";
                var filePath = Path.Combine(uploadDirectory, fileName);

                // Save the file to the specified directory
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    stream.Write(imageData, 0, imageData.Length);
                }


                using (var stream = new MemoryStream(imageData))
                {
                    _ftpClient.UploadStream(stream, $"/path/to/ftp/{fileName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading image to FTP: {ex.Message}");
                throw; // Rethrow the exception or handle it as appropriate for your application
            }
        }



        //private const string ApiKey = "API KEY FROM Link https://tinypng.com/developers"; // Replace with your TinyPNG API key

        public async Task CompressAndSaveImageAsync(string inputImagePath, string outputImagePath)
        {
            try
            {
                var source = Tinify.FromFile(inputImagePath);
                await source.ToFile(outputImagePath);

                Console.WriteLine("Image compressed and saved successfully!");
            }
            catch (TinifyException ex)
            {
                Console.WriteLine($"Error compressing image: {ex.Message}");
                throw; // Rethrow the exception for handling at the API level
            }
        }

        //public async Task UploadImageToFtpAsync(string fileName, byte[] imageData)
        //{
        //    using (var stream = new MemoryStream(imageData))
        //    {
        //        await _ftpClient.UploadFileAsync(stream, $"/path/to/ftp/{fileName}");
        //    }
        //}

    }
}
