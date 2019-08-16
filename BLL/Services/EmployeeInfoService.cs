using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Interfaces;
using BLL.Interface.Entities;
using BLL.Mappers;
using DAL.Interface.Interfaces;

namespace BLL.Services
{
    public class EmployeeInfoService : IEmployeeService
    {
        IEmployeeRepository repository;

        /// <summary>
        /// Initializes a new instance of <see cref="EmployeeInfoService"/>.
        /// </summary>
        /// <param name="repository">Implementation of <see cref="IEmployeeRepository"/>.</param>
        public EmployeeInfoService(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc/>
        public int Create(EmployeeInfo info)
        {
            return int.Parse(repository.Create(info.ToDAL()));
        }

        /// <inheritdoc/>
        public EmployeeInfo Get(int id)
        {
            return repository.GetById(id).ToBLL();
        }

        /// <inheritdoc/>
        public IEnumerable<EmployeeInfo> Get()
        {
            return repository.GetAll().Select(x => x.ToBLL());
        }

        /// <inheritdoc/>
        public void Remove(int id)
        {
            repository.Delete(id);
        }

        /// <inheritdoc/>
        public void Update(EmployeeInfo info)
        {
            repository.Update(info.ToDAL());
        }
    }
}
