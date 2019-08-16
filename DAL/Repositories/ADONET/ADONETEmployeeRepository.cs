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
    public class ADONETEmployeeRepository : IEmployeeRepository
    {
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of <see cref="ADONETEmployeeRepository"/>.
        /// </summary>
        public ADONETEmployeeRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SupportServiceDb"].ConnectionString;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ADONETEmployeeRepository"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">When <see cref="connectionString"/> is null.</exception>
        /// <exception cref="ArgumentException">When <see cref="connectionString"/> is empty.</exception>
        /// <param name="connectionString">Specified connection string.</param>
        public ADONETEmployeeRepository(string connectionString)
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
        public string Create(EmployeeDTO employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException($"{nameof(employee)} is null.");
            }

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("CreateEmployee", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 50));
            sqlCommand.Parameters["@FirstName"].Value = employee.FirstName;
            sqlCommand.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 50));
            sqlCommand.Parameters["@LastName"].Value = employee.LastName;
            sqlCommand.Parameters.Add(new SqlParameter("@Patronymic", SqlDbType.NVarChar, 50));
            sqlCommand.Parameters["@Patronymic"].Value = employee.Patronymic;
            sqlCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50));
            sqlCommand.Parameters["@Email"].Value = employee.Email;
            sqlCommand.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 50));
            sqlCommand.Parameters["@Phone"].Value = employee.Phone;
            sqlCommand.Parameters.Add(new SqlParameter("@RoomName", SqlDbType.NVarChar, 10));
            sqlCommand.Parameters["@RoomName"].Value = employee.Room;
            sqlCommand.Parameters.Add(new SqlParameter("@RoleName", SqlDbType.NVarChar, 50));
            sqlCommand.Parameters["@RoleName"].Value = employee.Role;

            using (sqlConnection)
            {
                sqlConnection.Open();
                return sqlCommand.ExecuteScalar().ToString();
            }
        }

        /// <inheritdoc/>
        public void Delete(int EmployeeId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand("DeleteEmployee", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
            sqlCommand.Parameters["@Id"].Value = EmployeeId;

            using (sqlConnection)
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<EmployeeDTO> GetAll()
        {
            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("GetAllEmployees", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            var employees = new List<EmployeeDTO>();

            using (sqlConnection)
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var emp = new EmployeeDTO
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Patronymic = (string)reader["Patronymic"],
                        Email = (string)reader["Email"],
                        Phone = (string)reader["Phone"],
                        Role = (string)reader["Role"],
                        Room = (string)reader["Room"]
                    };

                    employees.Add(emp);
                }

                reader.Close();
                return employees;
            }
        }

        /// <inheritdoc/>
        public EmployeeDTO GetById(int employeeId)
        {
            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("GetEmployeeById", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
            sqlCommand.Parameters["@Id"].Value = employeeId;

            using (sqlConnection)
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow);

                if (!reader.HasRows) return null;
                reader.Read();

                var employee = new EmployeeDTO
                {
                    Id = employeeId,
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Patronymic = (string)reader["Patronymic"],
                    Email = (string)reader["Email"],
                    Phone = (string)reader["Phone"],
                    Role = (string)reader["Role"],
                    Room = (string)reader["Room"]
                };

                reader.Close();
                return employee;
            }
        }

        public IEnumerable<EmployeeDTO> GetByParameter(string parameter, string value)
        {
            var sqlConnection = new SqlConnection(connectionString);
            string sqlExpression = "SELECT Employees.Id, Employees.FirstName, Employees.LastName, Employees.Patronymic, " +
                "Employees.Email, Employees.Phone, Roles.Name AS Role, Rooms.Number AS Room " +
                "FROM [dbo].[Employees] " +
                "INNER JOIN [dbo].[Rooms] ON Employees.Room_Id = Rooms.Id " +
                "INNER JOIN [dbo].[Roles] ON Employees.Role_Id = Roles.Id " +
                $"WHERE Employees.{parameter} = '{value}'";
            SqlCommand sqlCommand = new SqlCommand(sqlExpression, sqlConnection);

            var employees = new List<EmployeeDTO>();

            using (sqlConnection)
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var emp = new EmployeeDTO
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Patronymic = (string)reader["Patronymic"],
                        Email = (string)reader["Email"],
                        Phone = (string)reader["Phone"],
                        Role = (string)reader["Role"],
                        Room = (string)reader["Room"]
                    };

                    employees.Add(emp);
                }

                reader.Close();
                return employees;
            }
        }

        /// <inheritdoc/>
        public void Update(EmployeeDTO employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException($"{nameof(employee)} is null.");
            }

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("UpdateEmployee", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
            sqlCommand.Parameters["@Id"].Value = employee.Id;
            sqlCommand.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 50));
            sqlCommand.Parameters["@FirstName"].Value = employee.FirstName;
            sqlCommand.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 50));
            sqlCommand.Parameters["@LastName"].Value = employee.LastName;
            sqlCommand.Parameters.Add(new SqlParameter("@Patronymic", SqlDbType.NVarChar, 50));
            sqlCommand.Parameters["@Patronymic"].Value = employee.Patronymic;
            sqlCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50));
            sqlCommand.Parameters["@Email"].Value = employee.Email;
            sqlCommand.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 50));
            sqlCommand.Parameters["@Phone"].Value = employee.Phone;
            sqlCommand.Parameters.Add(new SqlParameter("@RoomName", SqlDbType.NVarChar, 10));
            sqlCommand.Parameters["@RoomName"].Value = employee.Room;
            sqlCommand.Parameters.Add(new SqlParameter("@RoleName", SqlDbType.NVarChar, 50));
            sqlCommand.Parameters["@RoleName"].Value = employee.Role;

            using (sqlConnection)
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
