using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Interfaces;
using BLL.Interface.Entities;
using BLL.Mappers;
using System.Linq;
using DAL.Interface.Interfaces;

namespace BLL.Services
{
    /// <summary>
    /// Service that contains basic methods for working with <see cref="Account"/>.
    /// </summary>
    public class AccountService : IAccountService
    {
        IAccountRepository repository;

        /// <summary>
        /// Initializes a new instance of <see cref="AccountService"/>.
        /// </summary>
        /// <param name="repository">Implementation of <see cref="IAccountRepository"/>.</param>
        public AccountService(IAccountRepository repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc/>
        public int Create(Account account)
        {
            var accounts = repository.GetByParameter("Login", account.Login);

            if (accounts.Count() > 0)
            {
                return -1;
            }
            else
            {
                return int.Parse(repository.Create(account.ToDAL()));
            }
        }

        /// <inheritdoc/>
        public int CheckAccountData(Account account)
        {
            return repository.CheckLoginData(account.ToDAL());
        }

        /// <inheritdoc/>
        public Account GetAccount(int id)
        {
            return repository.GetById(id).ToBLL();
        }

        /// <inheritdoc/>
        public Account GetAccountByParameter(string parameterName, string parameterValue)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<Account> GetAllAccounts()
        {
            return repository.GetAll().Select(x => x.ToBLL());
        }

        /// <inheritdoc/>
        public void Remove(int id)
        {
            repository.Delete(id);
        }
    }
}
