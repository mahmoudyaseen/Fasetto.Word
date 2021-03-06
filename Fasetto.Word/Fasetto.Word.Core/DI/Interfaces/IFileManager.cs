using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    /// <summary>
    /// Handles reading/writing and querying the file system
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Writes the text to the specified file
        /// </summary>
        /// <param name="text">Text to write</param>
        /// <param name="path">Path of the file to write to</param>
        /// <param name="append">If true, writes the text to the end of the file, otherwise overrides any existing file</param>
        /// <returns></returns>
        Task WriteTextTofileAsync(string text, string path, bool append = false);

        /// <summary>
        /// Normalizing a path based on current operating system
        /// </summary>
        /// <param name="path">The path to normalize</param>
        /// <returns></returns>
        string NormalizePath(string path);
        
        /// <summary>
        /// Resolves any relative elements of the path to absolute
        /// </summary>
        /// <param name="path">The path to resolve</param>
        /// <returns></returns>
        string ResolvePath(string path);
    }
}
