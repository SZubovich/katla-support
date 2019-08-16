using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;
using BLL.Interface.Entities;

namespace WebAPI.Infrastructure.Mappers
{
    public static class ViewMapper
    {
        #region Account Mapping
        
        public static Account ToBLL(this AccountModel account)
        {
            return new Account()
            {
                Login = account.Login,
                Password = account.Password
            };
        }

        #endregion

        #region Task Mapping

        public static Task ToBLL(this FullTaskInfo task)
        {
            return new Task()
            {
                Id = task.Id,
                BlobPath = task.BlobPath,
                Category = task.Category,
                CreatingDate = task.CreatingDate,
                ClosingDate = task.ClosingDate,
                Comment = task.Comment,
                IsClosed = task.IsClosed,
                Engineer = new EmployeeInfo() { Id = task.EngineerId},
                Text = task.Text,
                File = task.File,
                Creator = new EmployeeInfo() { Id = task.CreatorId}
                //TODO Engineer and Creator?
            };
        }

        public static ShortTaskInfo ToViewShort(this Task task)
        {
            return new ShortTaskInfo()
            {
                Id = task.Id,
                Category = task.Category,
                CreatingDate = task.CreatingDate,
                Text = task.Text,
                IsClosed = task.IsClosed
            };
        }

        public static FullTaskInfo ToViewFull(this Task task)
        {
            return new FullTaskInfo()
            {
                Id = task.Id,
                Category = task.Category,
                CreatingDate = task.CreatingDate,
                ClosingDate = task.ClosingDate,
                Text = task.Text,
                CreatorName = $"{task.Creator.LastName} {task.Creator.FirstName}",
                EngineerName = $"{task.Engineer.LastName} {task.Engineer.FirstName}",
                EngineerId = task.Engineer.Id,
                BlobPath = task.BlobPath,
                Comment = task.Comment,
                IsClosed = task.IsClosed
            };
        }

        #endregion

        #region Profile Mapping

        public static EmployeeInfo ToBLL(this ProfileModel profileInfo)
        {
            return new EmployeeInfo()
            {
                Id = profileInfo.Id,
                FirstName = profileInfo.FirstName,
                LastName = profileInfo.LastName,
                Patronymic = profileInfo.Patronymic,
                Phone = profileInfo.Phone,
                Email = profileInfo.Email,
                Role = profileInfo.Role,
                Room = profileInfo.Room
            };
        }

        public static ProfileModel ToView(this EmployeeInfo employeeInfo)
        {
            return new ProfileModel()
            {
                Id = employeeInfo.Id,
                FirstName = employeeInfo.FirstName,
                LastName = employeeInfo.LastName,
                Patronymic = employeeInfo.Patronymic,
                Email = employeeInfo.Email,
                Phone = employeeInfo.Phone,
                Role = employeeInfo.Role,
                Room = employeeInfo.Room
            };
        }

        #endregion
    }
}