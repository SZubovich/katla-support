using System.IO;
using System.Threading.Tasks;

namespace DAL.Interface.Interfaces
{
    public interface IBlobCreator
    {
        Task<bool> Create(int userId, int taskId, string fileName, Stream stream);
    }
}
