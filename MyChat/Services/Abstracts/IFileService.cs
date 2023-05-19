using MyChat.Enums.File;

namespace MyChat.Services.Abstracts;

public interface IFileService
{
    public bool FileValid(IFormFile uploadedFile, ImageType imageType);
    public string SaveImage(IFormFile uploadedFile, ImageType imageType);
    public string GetPrimalImgPath();
}