namespace ImageResizer.Repository.Interfaces
{
    public interface IImageRepository
    {
        Task CompressAndSaveImageAsync(string inputImagePath, string outputImagePath);
    }
}
