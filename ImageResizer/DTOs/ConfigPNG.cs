using FluentFTP;

namespace ImageResizer.DTOs
{
    public class ConfigPNG
    {
        public TinyPngConfig TinyPng { get; set; }
        public FtpConfig Ftp { get; set; }
    }
}
