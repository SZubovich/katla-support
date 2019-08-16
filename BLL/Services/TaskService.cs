using System;
using System.Linq;
using System.Collections.Generic;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using DAL.Interface.Interfaces;
using BLL.Mappers;

namespace BLL.Services
{
    /// <summary>
    /// Service that allows to work with <see cref="Task"/>. Implements <see cref="ITaskService"/>.
    /// </summary>
    public class TaskService : ITaskService
    {
        private const int EngineerId = 7;

        ITaskRepository taskRepository;
        IEmployeeRepository employeeRepository;
        IFileSystemRepository fileSystemRepository;
        IBlobCreator blob;
        Random rand;

        /// <summary>
        /// Initializes a new instatnce of <see cref="TaskService"/>.
        /// </summary>
        /// <param name="taskRepository">Implementation of <see cref="ITaskRepository"/>.</param>
        /// <param name="employeeRepository">Implementation of <see cref="IEmployeeRepository"/>.</param>
        public TaskService(ITaskRepository taskRepository, IEmployeeRepository employeeRepository,
            IFileSystemRepository fileSystemRepository, IBlobCreator blob)
        {
            this.taskRepository = taskRepository;
            this.employeeRepository = employeeRepository;
            this.fileSystemRepository = fileSystemRepository;
            this.blob = blob;
            rand = new Random();
        }

        /// <inheritdoc/>
        public int Create(Task task)
        {
            var engineers = employeeRepository.GetByParameter("Role_Id", EngineerId.ToString()).ToArray();
            task.Engineer = engineers[rand.Next(engineers.Length)].ToBLL();
            task.CreatingDate = DateTime.Now;
            task.ClosingDate = DateTime.Now;
            var taskId = int.Parse(taskRepository.Create(task.ToDAL()));
            
            blob.Create(task.Creator.Id, taskId, task.BlobPath, task.File);
            fileSystemRepository.CreateFilePathIfNotExists(task.Creator.Id, taskId, task.BlobPath);

            return taskId;
        }

        /// <inheritdoc/>
        public IEnumerable<Task> GetAll()
        {
            var tasks = taskRepository.GetAll().Select(x => x.ToBLL()).ToArray();

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i].Creator = employeeRepository.GetById(tasks[i].Creator.Id).ToBLL();
                tasks[i].Engineer = employeeRepository.GetById(tasks[i].Engineer.Id).ToBLL();
            }

            return tasks;
        }

        /// <inheritdoc/>
        public IEnumerable<Task> GetAllForUser(int id)
        {
            IEnumerable<Task> tasks;
            var emp = employeeRepository.GetById(id);

            if (emp is null)
            {
                return null;
            }
            //TODO Initialize fields Engineer and Creator.
            if (emp.Role == "SupportEngineer")
            {
                tasks = taskRepository.GetByParameter("Engineer_Id", id.ToString()).Select( x => x.ToBLL());
            }
            else
            {
                tasks = taskRepository.GetByParameter("TaskCreator_Id", id.ToString()).Select(x => x.ToBLL());
            }

            return tasks;
        }

        /// <inheritdoc/>
        public Task GetById(int id)
        {
            var task = taskRepository.GetById(id).ToBLL();
            task.Engineer = employeeRepository.GetById(task.Engineer.Id).ToBLL();
            task.Creator = employeeRepository.GetById(task.Creator.Id).ToBLL();
            task.BlobPath = fileSystemRepository.GetFilePathByTaskId(task.Id);

            return task;
        }

        /// <inheritdoc/>
        public void Close(Task task)
        {
            task.IsClosed = true;
            taskRepository.Update(task.ToDAL());
        }

        /// <inheritdoc/>
        public void SaveChanges(Task task)
        {
            taskRepository.Update(task.ToDAL());
        }

        /// <inheritdoc/>
        public void Remove(int id)
        {
            taskRepository.Delete(id);
        }
    }
}
