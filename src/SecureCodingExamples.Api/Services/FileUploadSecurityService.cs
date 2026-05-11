namespace SecureCodingExamples.Api.Services;

public class FileUploadSecurityService
{
    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".pdf",
        ".png",
        ".jpg",
        ".jpeg"
    };

    private const long MaxFileSizeBytes = 2 * 1024 * 1024;

    public bool IsAllowedFile(IFormFile file)
    {
        if (file.Length <= 0 || file.Length > MaxFileSizeBytes)
        {
            return false;
        }

        var extension = Path.GetExtension(file.FileName);

        if (!AllowedExtensions.Contains(extension))
        {
            return false;
        }

        return true;
    }

    public string CreateSafeStorageName(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName);
        return $"{Guid.NewGuid():N}{extension}";
    }
}
