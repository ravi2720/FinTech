using FinTech_ApiPanel.Domain.Shared.Enums;
using Microsoft.AspNetCore.Http;

namespace FinTech_ApiPanel.Domain.Shared.Utils
{
    public class FileUtils
    {
        public static async Task<string> FileUpload(IFormFile file, FilePath filePathEnum)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("Invalid file.");
            }

            string fileName;
            string filePath;

            try
            {
                // Generate unique file name with extension
                var extension = Path.GetExtension(file.FileName);
                fileName = $"{DateTime.Now.Ticks}{extension}";

                // Construct the directory path
                var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePathEnum.ToString());

                // Ensure the directory exists
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }

                // Construct the full file path
                filePath = Path.Combine(rootPath, fileName);

                // Save the file
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                // Add logging or specific exception handling if needed
                throw new Exception("Can't Upload File", ex);
            }

            return fileName;
        }

        public static async Task DeleteFile(string fileName, FilePath filePathEnum)
        {
            string filePath;
            try
            {
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\\" + filePathEnum + "\\" + fileName);

                // Check if the file exists before deleting it
                if (File.Exists(pathBuilt))
                {
                    File.Delete(pathBuilt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
