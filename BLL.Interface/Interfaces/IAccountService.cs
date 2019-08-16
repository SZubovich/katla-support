using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Interfaces
{
    /// <summary>
    /// Service which contains basic methods for working with <see cref="Account"/>.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Checks account login and password on existing in the account storage.
        /// </summary>
        /// <param name="account">Entity of <see cref="Account"/> that contains account data.</param>
        /// <returns>Id of account</returns>
        int CheckAccountData(Account account);

        /// <summary>
        /// Creates a new account in repository. 
        /// </summary>
        /// <param name="account">Entity of <see cref="Account"/> that contains account data.</param>
        int Create(Account account);

        /// <summary>
        /// Removes user by specified ID.
        /// </summary>
        /// <param name="id">Id of removed user.</param>
        void Remove(int id);

        /// <summary>
        /// Returns account by Id.
        /// </summary>
        /// <param name="id">Specified Id of user.</param>
        /// <returns>Instance of <see cref="Account"/>.</returns>
        Account GetAccount(int id);

        /// <summary>
        /// Returns account by specified parameter.
        /// </summary>
        /// <param name="parameterName">Name of parameter.</param>
        /// <param name="parameterValue">Value of parameter.</param>
        /// <returns>Instance of <see cref="Account"/>.</returns>
        Account GetAccountByParameter(string parameterName, string parameterValue);

        /// <summary>
        /// Returns all accounts.
        /// </summary>
        /// <returns>All accounts.</returns>
        IEnumerable<Account> GetAllAccounts();
    }
}
