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
    /// Contains methods for work with Tasks database.
    /// </summary>
    public class ADONETTaskRepository : ITaskRepository
    {
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of <see cref="ADONETTaskRepository"/>.
        /// </summary>
        public ADONETTaskRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SupportServiceDb"].ConnectionString;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ADONETTaskRepository"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">When <see cref="connectionString"/> is null.</exception>
        /// <exception cref="ArgumentException">When <see cref="connectionString"/> is empty.</exception>
        /// <param name="connectionString">Specified connection string.</param>
        public ADONETTaskRepository(string connectionString)
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
        public string Create(TaskDTO task)
        {
            if (task is null)
            {
                throw new ArgumentNullException($"{nameof(task)} is null.");
            }

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("CreateTask", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.Add(new SqlParameter("@Text", SqlDbType.Text));
            sqlCommand.Parameters["@Text"].Value = task.Text;
            sqlCommand.Parameters.Add(new SqlParameter("@Category", SqlDbType.NVarChar, 50));
            sqlCommand.Parameters["@Category"].Value = task.Category;
            sqlCommand.Parameters.Add(new SqlParameter("@CreatingDate", SqlDbType.DateTime));
            sqlCommand.Parameters["@CreatingDate"].Value = task.CreatingDate;
            sqlCommand.Parameters.Add(new SqlParameter("@ClosingDate", SqlDbType.DateTime));
            sqlCommand.Parameters["@ClosingDate"].Value = DateTime.Now;
            sqlCommand.Parameters.Add(new SqlParameter("@Priority", SqlDbType.Int, 4));
            sqlCommand.Parameters["@Priority"].Value = 0;
            sqlCommand.Parameters.Add(new SqlParameter("@TaskCreatorId", SqlDbType.Int, 4));
            sqlCommand.Parameters["@TaskCreatorId"].Value = task.TaskCreator;
            sqlCommand.Parameters.Add(new SqlParameter("@EngineerId", SqlDbType.Int, 4));
            sqlCommand.Parameters["@EngineerId"].Value = task.Engineer;
            sqlCommand.Parameters.Add(new SqlParameter("@Comment", SqlDbType.NVarChar, 50));
            sqlCommand.Parameters["@Comment"].Value = string.Empty;
            sqlCommand.Parameters.Add(new SqlParameter("@Closed", SqlDbType.Bit));
            sqlCommand.Parameters["@Closed"].Value = false;

            using (sqlConnection)
            {
                sqlConnection.Open();
                return sqlCommand.ExecuteScalar().ToString();
            }
        }

        /// <inheritdoc/>
        public void Delete(int taskId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand("DeleteTask", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
            sqlCommand.Parameters["@Id"].Value = taskId;

            using (sqlConnection)
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<TaskDTO> GetAll()
        {
            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("GetAllTasks", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            var tasks = new List<TaskDTO>();

            using (sqlConnection)
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var task = new TaskDTO
                    {
                        Id = (int)reader["Id"],
                        Text = (string)reader["Text"],
                        Category = (string)reader["Category"],
                        CreatingDate = (DateTime)reader["CreatingDate"],
                        ClosingDate = (DateTime)reader["ClosingDate"],
                        TaskCreator = (int)reader["Creator"],
                        Engineer = (int)reader["Engineer"],
                        Comment = (string)reader["Comment"],
                        IsClosed = (bool)reader["Closed"]
                    };

                    tasks.Add(task);
                }

                reader.Close();
                return tasks;
            }
        }

        /// <inheritdoc/>
        public TaskDTO GetById(int taskId)
        {
            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("GetTaskById", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
            sqlCommand.Parameters["@Id"].Value = taskId;

            using (sqlConnection)
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow);

                if (!reader.HasRows) return null;
                reader.Read();

                var task = new TaskDTO
                {
                    Id = (int)reader["Id"],
                    Text = (string)reader["Text"],
                    Category = (string)reader["Category"],
                    CreatingDate = (DateTime)reader["CreatingDate"],
                    ClosingDate = (DateTime)reader["ClosingDate"],
                    TaskCreator = (int)reader["Creator"],
                    Engineer = (int)reader["Engineer"],
                    Comment = (string)reader["Comment"],
                    IsClosed = (bool)reader["Closed"]
                };

                reader.Close();
                return task;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<TaskDTO> GetByParameter(string parameter, string value)
        {
            var sqlConnection = new SqlConnection(connectionString);
            string sqlExpression = $"SELECT Tasks.Id, Tasks.Text, Categories.Name AS Category, Tasks.CreatingDate, Tasks.ClosingDate," +
                "Tasks.Engineer_Id AS Engineer, Tasks.TaskCreator_Id AS Creator, Tasks.Comment, Tasks.Closed " +
                "FROM [dbo].[Tasks] INNER JOIN[dbo].[Categories] ON Categories.Id = Tasks.Category_Id "+
                $"WHERE Tasks.{parameter} = '{value}'";
            SqlCommand sqlCommand = new SqlCommand(sqlExpression, sqlConnection);

            var tasks = new List<TaskDTO>();

            using (sqlConnection)
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var task = new TaskDTO
                    {
                        Id = (int)reader["Id"],
                        Text = (string)reader["Text"],
                        Category = (string)reader["Category"],
                        CreatingDate = (DateTime)reader["CreatingDate"],
                        ClosingDate = (DateTime)reader["ClosingDate"],
                        TaskCreator = (int)reader["Creator"],
                        Engineer = (int)reader["Engineer"],
                        Comment = (string)reader["Comment"],
                        IsClosed = (bool)reader["Closed"]
                    };

                    tasks.Add(task);
                }

                reader.Close();
                return tasks;
            }
        }

        /// <inheritdoc/>
        public void Update(TaskDTO task)
        {
            if (task is null)
            {
                throw new ArgumentNullException($"{nameof(task)} is null.");
            }

            var sqlConnection = new SqlConnection(connectionString);
            var sqlCommand = new SqlCommand("UpdateTask", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
            sqlCommand.Parameters["@Id"].Value = task.Id;
            sqlCommand.Parameters.Add(new SqlParameter("@ClosingDate", SqlDbType.DateTime));
            sqlCommand.Parameters["@ClosingDate"].Value = DateTime.Now;
            sqlCommand.Parameters.Add(new SqlParameter("@Comment", SqlDbType.NVarChar, 50));
            sqlCommand.Parameters["@Comment"].Value = task.Comment;
            sqlCommand.Parameters.Add(new SqlParameter("@IsClosed", SqlDbType.Bit));
            sqlCommand.Parameters["@IsClosed"].Value = task.IsClosed;

            using (sqlConnection)
            {
                sqlConnection.Open();
                sqlCommand.ExecuteScalar();
            }
        }
    }
}
