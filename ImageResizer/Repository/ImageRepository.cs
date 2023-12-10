using ImageResizer.Repository.Interfaces;
using TinifyAPI;

namespace ImageResizer.Repository
{
    public class ImageRepository : IImageRepository
    {
        private const string ApiKey = "API KEY FROM Link https://tinypng.com/developers"; // Replace with your TinyPNG API key

        public async Task CompressAndSaveImageAsync(string inputImagePath, string outputImagePath)
        {
            try
            {
                Tinify.Key = ApiKey;

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
    }
}
