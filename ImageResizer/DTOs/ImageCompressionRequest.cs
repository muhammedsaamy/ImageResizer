namespace ImageResizer.DTOs
{
    public class ImageCompressionRequest
    {
        public string FileName { get; set; }
        public IFormFile Data { get; set; }
    }
}
