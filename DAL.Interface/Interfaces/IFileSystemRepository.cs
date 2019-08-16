using DAL.Interface.DTO;
using System.IO;

namespace DAL.Interface.Interfaces
{
    /// <summary>
    /// Has methods for work with paths in file system
    /// </summary>
    public interface IFileSystemRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskId"></param>
        /// <param name="fileName"></param>
        void CreateFilePathIfNotExists(int userId, int taskId, string fileName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        string GetFilePathByTaskId(int taskId);
    }
}
