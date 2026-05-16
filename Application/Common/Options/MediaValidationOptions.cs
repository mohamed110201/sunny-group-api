namespace Application.Common.Options;

public class MediaValidationOptions
{
    public string[] AllowedImageTypes { get; set; } = [];
    public string[] AllowedVideoTypes { get; set; } = [];
    public int MaxImageSizeMb { get; set; }
    public int MaxVideoSizeMb { get; set; }
    public int MaxImagesPerUpload { get; set; }
    public int MaxSvgSizeMb { get; set; }
}