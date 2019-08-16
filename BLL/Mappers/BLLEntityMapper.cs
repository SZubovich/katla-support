using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    /// <summary>
    /// Contains methods for mapping objects between DA and BL layers.
    /// </summary>
    public static class BLLEntityMapper
    {
        #region Task Mapping Part

        public static Task ToBLL(this TaskDTO taskDTO)
        {
            return new Task
            {
                Id = taskDTO.Id,
                Text = taskDTO.Text,
                Category = taskDTO.Category,
                CreatingDate = taskDTO.CreatingDate,
                ClosingDate = taskDTO.ClosingDate,
                Comment = taskDTO.Comment,
                Creator = new EmployeeInfo() { Id = taskDTO.TaskCreator },
                Engineer = new EmployeeInfo() { Id = taskDTO.Engineer },
                IsClosed = taskDTO.IsClosed
            };
        }

        public static TaskDTO ToDAL(this Task task)
        {
            return new TaskDTO
            {
                Id = task.Id,
                Text = task.Text,
                Category = task.Category,
                CreatingDate = task.CreatingDate,
                ClosingDate = task.ClosingDate,
                Comment = task.Comment,
                TaskCreator = task.Creator.Id,
                Engineer = task.Engineer.Id,
                IsClosed = task.IsClosed
            };
        }
        
        #endregion

        #region Account Mapping Part

        public static Account ToBLL(this AccountDTO account)
        {
            return new Account
            {
                Login = account.Login,
                Password = account.Password
            };
        }

        public static AccountDTO ToDAL(this Account account)
        {
            return new AccountDTO
            {
                Login = account.Login,
                Password = account.Password
            };
        }

        #endregion

        #region Employee Mapping Part

        public static EmployeeInfo ToBLL(this EmployeeDTO employee)
        {
            return new EmployeeInfo
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Email = employee.Email,
                Role = employee.Role,
                Room = employee.Room,
                Phone = employee.Phone
            };
        }

        public static EmployeeDTO ToDAL(this EmployeeInfo employee)
        {
            return new EmployeeDTO
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Email = employee.Email,
                Role = employee.Role,
                Room = employee.Room,
                Phone = employee.Phone
            };
        }

        #endregion
    }
}
