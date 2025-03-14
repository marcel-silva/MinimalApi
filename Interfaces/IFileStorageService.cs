using System.Threading.Tasks;

namespace MinimalAPI.Interfaces
{
    /// <summary>
    /// Provides functionality for storing files.
    /// </summary>
    public interface IFileStorageService
    {
        /// <summary>
        /// Stores the specified content in a file under the provided base path.
        /// </summary>
        /// <param name="content">The content to be stored.</param>
        /// <param name="basePath">The base directory where the file should be saved.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the generated file name.
        /// </returns>
        Task<string> StoreFileAsync(string content, string basePath);
    }
}
