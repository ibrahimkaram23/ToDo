using Microsoft.AspNetCore.Http;
using ToDo.Domain.Models.Enums;

namespace ToDo.Infrastructure.Helper
{
    public class ImageSettings
    {
        public static string UploadFile(IFormFile file, ImageType type)
        {
            string folderName = GetFolderName(type);
            //Get Located Folder Path
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var binIndex = baseDirectory.IndexOf("\\bin\\", StringComparison.OrdinalIgnoreCase);
            string solutionDirectory = "";
            if (binIndex > 0)
                solutionDirectory = baseDirectory.Substring(0, binIndex);
            var wwwrootDirectory = Path.Combine(solutionDirectory, "wwwroot", "images");
            var folderPath = Path.Combine(wwwrootDirectory, folderName);
            //Get File Name And Make its Name Unique
            var fileName = $"{Guid.NewGuid()}{Path.GetFileName(file.FileName)}";
            //Get File Path
            var filePath = Path.Combine(folderPath, fileName);
            //Save File As Streams
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            return fileName;
        }
        public static void DeleteFile(string fileName, ImageType type)
        {
            string folderName = GetFolderName(type);

            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var binIndex = baseDirectory.IndexOf("\\bin\\", StringComparison.OrdinalIgnoreCase);
            string solutionDirectory = "";
            if (binIndex > 0)
                solutionDirectory = baseDirectory.Substring(0, binIndex);
            var wwwrootDirectory = Path.Combine(solutionDirectory, "wwwroot", "Images");
            var folderPath = Path.Combine(wwwrootDirectory, folderName);

            var filePath = Path.Combine(folderPath, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public static string GetFolderName(ImageType type)
        {
            switch (type)
            {
                case ImageType.UserPicture:
                    return "UserPicture";
                default:
                    return "Other";
            }
        }
    }


}
