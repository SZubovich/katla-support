using DAL.Interface.DTO;

namespace DAL.Interface.Interfaces
{
    /// <summary>
    /// Implements interface <see cref="IRepository{T}"/> for <see cref="AccountDTO"/>.
    /// </summary>
    public interface IAccountRepository : IRepository<AccountDTO>
    {
        /// <summary>
        /// Check login and password in storage.
        /// </summary>
        /// <param name="account">User account data.</param>
        /// <returns>Id of account.</returns>
        int CheckLoginData(AccountDTO account);
    }
}
