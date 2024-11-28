using System.Runtime.CompilerServices;

namespace Demo.API.Services;

public class PhotoSetting
{
    public static HashSet<string> AllowedExtensions { get; } = new HashSet<string>() { ".jpg", ".png" };
    public static long MaxSize { get; } = 5 * 1024 * 1024;
    public static string CurrentDirectory { get; } = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Photos");

    public static string VolunteerIcon = "VolunteerIcon.jpg"; 
    public static string OrganizationIcon = "OrganizationIcon.png"; 

    public static string UploadPhoto(IFormFile formFile, string folderName)
    {

        if (!AllowedExtensions.Contains(Path.GetExtension(formFile.FileName).ToLowerInvariant()))
        {
            throw new Exception("File format should be .png or .jpg"); 
        }

        //1. Locate folder path 
        string folderPath = Path.Combine(CurrentDirectory, folderName);

        //2. Get file name and make it UNIQUE
        string fileName = $"{Guid.NewGuid()}{formFile.FileName}";

        //3. Get full file path
        string filePath = Path.Combine(folderPath, fileName);

        //Save the file
        using var stream = new FileStream(filePath, FileMode.Create);
        formFile.CopyTo(stream);


        return fileName;
    }

    public static bool Delete(string folderName, string fileName)
    {
        string filePath = Path.Combine(CurrentDirectory, folderName, fileName);

        if (!File.Exists(filePath) || fileName == VolunteerIcon || fileName == OrganizationIcon) 
            return false;

        File.Delete(filePath);
        return true;
    }
}


