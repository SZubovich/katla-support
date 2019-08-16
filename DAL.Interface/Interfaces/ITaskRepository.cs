using DAL.Interface.DTO;

namespace DAL.Interface.Interfaces
{
    /// <summary>
    /// Implements interface <see cref="IRepository{T}"/> for <see cref="TaskDTO"/>.
    /// </summary>
    public interface ITaskRepository : IRepository<TaskDTO>
    {
    }
}
