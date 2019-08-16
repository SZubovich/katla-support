using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;

namespace DAL.Repositories.ADONET
{
    /// <summary>
    /// Contains methods for work with Accounts database.
    /// </summary>
    public class ADONETAccountRepository : IAccountRepository
    {
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of <see cref="ADONETAccountRepository"/>.
        /// </summary>
        public ADONETAccountRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SupportServiceDb"].ConnectionString;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ADONETAccountRepository"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">When <see cref="connectionString"/> is null.</exception>
        /// <exception cref="ArgumentException">When <see cref="connectionString"/> is empty.</exception>
        /// <param name="connectionString">Specified connection string.</param>
        public ADONETAccountRepository(string connectionString)
        {
            if (connectionString is null)
            {
                throw new ArgumentNullException($"{nameof(connectionString)} is null.");
            }
            if (connectionString.Length == 0)
            {
                throw new ArgumentException($"{nameof(connectionString)} is empty.");
            }

            this.connectionString = connectionString;
        }

        /// <inheritdoc/>
        public int CheckLoginData(AccountDTO account)
        {
            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("CheckLoginData", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.Add(new SqlParameter("@Login", SqlDbType.NVarChar, 20));
            sqlCommand.Parameters["@Login"].Value = account.Login;
            sqlCommand.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 20));
            sqlCommand.Parameters["@Password"].Value = account.Password;

            using (sqlConnection)
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow);

                if (!reader.HasRows) return -1;
                reader.Read();

                int id = (int)reader["Id"];

                reader.Close();
                return id;
            }
        }

        /// <inheritdoc/>
        public string Create(AccountDTO account)
        {
            if (account is null)
            {
                throw new ArgumentNullException($"{nameof(account)} is null.");
            }

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("CreateAccount", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.Add(new SqlParameter("@Login", SqlDbType.NVarChar, 20));
            sqlCommand.Parameters["@Login"].Value = account.Login;
            sqlCommand.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 20));
            sqlCommand.Parameters["@Password"].Value = account.Password;

            using (sqlConnection)
            {
                sqlConnection.Open();
                return sqlCommand.ExecuteScalar().ToString();
            }
        }

        /// <inheritdoc/>
        public void Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand("DeleteAccount", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
            sqlCommand.Parameters["@Id"].Value = id;

            using (sqlConnection)
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<AccountDTO> GetAll()
        {
            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("GetAllAccounts", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            var accounts = new List<AccountDTO>();

            using (sqlConnection)
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var acc = new AccountDTO
                    {
                        Id = (int)reader["Id"],
                        Login = (string)reader["Login"],
                        Password = (string)reader["Password"]
                    };

                    accounts.Add(acc);
                }
                reader.Close();

                return accounts;
            }
        }

        /// <inheritdoc/>
        public AccountDTO GetById(int accountId)
        {
            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("GetAccountById", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
            sqlCommand.Parameters["@Id"].Value = accountId;

            using (sqlConnection)
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow);

                if (!reader.HasRows) return null;
                reader.Read();
                var account = new AccountDTO
                {
                    Id = (int)reader["Id"],
                    Login = (string)reader["Login"],
                    Password = (string)reader["Password"]
                };
                reader.Close();
                return account;
            }
        }

        public IEnumerable<AccountDTO> GetByParameter(string parameter, string value)
        {
            var sqlConnection = new SqlConnection(connectionString);
            string sqlExpression = "SELECT Accounts.Id, Accounts.Login, Accounts.Password" +
                " FROM [dbo].[Accounts] " + 
                $"WHERE Accounts.{parameter} = '{value}'";
            SqlCommand sqlCommand = new SqlCommand(sqlExpression, sqlConnection);

            var accounts = new List<AccountDTO>();

            using (sqlConnection)
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var acc = new AccountDTO
                    {
                        Id = (int)reader["Id"],
                        Login = (string)reader["Login"],
                        Password = (string)reader["Password"]
                    };

                    accounts.Add(acc);
                }
                reader.Close();

                return accounts;
            }
        }

        /// <inheritdoc/>
        public void Update(AccountDTO account)
        {
            if (account is null)
            {
                throw new ArgumentNullException($"{nameof(account)} is null.");
            }

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("UpdateAccount", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.Add(new SqlParameter(@"Id", SqlDbType.Int, 4));
            sqlCommand.Parameters[@"Id"].Value = account.Id;
            sqlCommand.Parameters.Add(new SqlParameter("@Login", SqlDbType.NVarChar, 20));
            sqlCommand.Parameters["@Login"].Value = account.Login;
            sqlCommand.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 20));
            sqlCommand.Parameters["@Password"].Value = account.Password;

            using (sqlConnection)
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
