using DAL.Interface.DTO;

namespace DAL.Interface.Interfaces
{
    /// <summary>
    /// Implements interface <see cref="IRepository{T}"/> for <see cref="EmployeeDTO"/>.
    /// </summary>
    public interface IEmployeeRepository : IRepository<EmployeeDTO>
    {
    }
}
