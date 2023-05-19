using MyChat.Enums.File;

namespace MyChat.Services.Abstracts;

public interface IImageProfile
{
    ImageType ImageType { get; }
    string Folder { get; }

    int Width { get; }

    int Height { get; }

    int MaxSizeBytes { get; }

    IEnumerable<string> AllowedExtensions { get; }
}