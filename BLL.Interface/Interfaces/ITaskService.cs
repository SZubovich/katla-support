using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Interfaces
{
    /// <summary>
    /// Service which contains basic methods for working with <see cref="Task"/>.
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Creates a new <see cref="Task"/> in storage.
        /// </summary>
        /// <param name="task">Task which will be created.</param>
        int Create(Task task);

        /// <summary>
        /// Removes <see cref="Task"/> instance from repository.
        /// </summary>
        /// <param name="id">Id of removed task.</param>
        void Remove(int id);

        /// <summary>
        /// Returns all tasks from storage.
        /// </summary>
        /// <returns>All instances of <see cref="Task"/> from repository.</returns>
        IEnumerable<Task> GetAll();

        /// <summary>
        /// Returns instance of <see cref="Task"/> with specified Id.
        /// </summary>
        /// <param name="id">Id of task.</param>
        /// <returns>Instance of <see cref="Task"/> with specified Id.</returns>
        Task GetById(int id);

        /// <summary>
        /// Returns all tasks for current user.
        /// </summary>
        /// <param name="id">User Id.</param>
        /// <returns>All tasks for current user.</returns>
        IEnumerable<Task> GetAllForUser(int id);

        /// <summary>
        /// Saves changes in existing task.
        /// </summary>
        /// <param name="task">Instance of <see cref="Task"/> which will be save.</param>
        void SaveChanges(Task task);
    }
}
