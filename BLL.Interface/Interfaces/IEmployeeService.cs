using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Interfaces
{
    /// <summary>
    /// Service which contains basic methods for working with <see cref="EmployeeInfo"/>.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Creates a new account in repository. 
        /// </summary>
        /// <param name="account">Entity of <see cref="EmployeeInfo"/> that contains account data.</param>
        int Create(EmployeeInfo info);

        /// <summary>
        /// Removes user by specified ID.
        /// </summary>
        /// <param name="id">Id of removed user.</param>
        void Remove(int id);

        /// <summary>
        /// Returns employee info for specified Id.
        /// </summary>
        /// <param name="id">Specified Id of user.</param>
        /// <returns>Instance of <see cref="EmployeeInfo"/>.</returns>
        EmployeeInfo Get(int id);

        /// <summary>
        /// Returns info about all employees.
        /// </summary>
        /// <returns>All accounts.</returns>
        IEnumerable<EmployeeInfo> Get();

        /// <summary>
        /// Updates info about employee.
        /// </summary>
        void Update(EmployeeInfo info);
    }
}
