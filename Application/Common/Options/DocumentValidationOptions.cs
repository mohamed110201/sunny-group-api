namespace Application.Common.Options;

public class DocumentValidationOptions
{
    public string[] AllowedTypes { get; set; } = [];
    public int MaxSizeMb { get; set; } = 10;
    public int MaxDocumentsPerUpload { get; set; } = 5;
}