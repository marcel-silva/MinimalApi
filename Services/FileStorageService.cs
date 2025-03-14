using System;
using System.IO;
using System.Threading.Tasks;
using MinimalAPI.Interfaces;

namespace MinimalAPI.Services
{
    public class FileStorageService : IFileStorageService
    {
        public async Task<string> StoreFileAsync(string content, string basePath)
        {
            // Define directories
            string usersDir = Path.Combine(basePath, "Users");
            string inDir = Path.Combine(usersDir, "IN");

            // Create directories if they do not exist
            if (!Directory.Exists(usersDir))
            {
                Directory.CreateDirectory(usersDir);
            }
            if (!Directory.Exists(inDir))
            {
                Directory.CreateDirectory(inDir);
            }

            // Create a unique filename using a timestamp
            string fileName = $"data_{DateTime.Now:yyyyMMddHHmmssfff}.json";
            string filePath = Path.Combine(inDir, fileName);

            try
            {
                await File.WriteAllTextAsync(filePath, content);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error writing file: {ex.Message}", ex);
            }

            return fileName;
        }
    }
}
