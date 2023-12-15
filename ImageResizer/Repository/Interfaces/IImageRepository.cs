namespace ImageResizer.Repository.Interfaces
{
    public interface IImageRepository
    {
        Task<byte[]> OptimizeImageAsync(IFormFile imageData);
        Task UploadImageToFtpAsync(string fileName, byte[] imageData);
        Task CompressAndSaveImageAsync(string inputImagePath, string outputImagePath);

        //Task UploadImageToFtpAsync(string fileName, byte[] imageData);
    }
}
