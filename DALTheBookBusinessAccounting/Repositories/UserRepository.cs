using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const int ID = 0;
        private const int USER_LOGIN = 1;
        private const int USER_PASSWORD = 2;
        private const int EMAIL = 3;

        private const int ROLE_ID = 0;

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["TheBookOfBusinessAccountingContext"].ConnectionString;

        public void Create(User user)
        {
            const string SQL_EXPRESSION = "AddUser";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure// указываем, что команда представляет хранимую процедуру
                })
                {
                    SqlParameter loginParam = new SqlParameter// параметр для ввода login
                    {
                        ParameterName = "@UserLogin",
                        Value = user.UserLogin
                    };
                    command.Parameters.Add(loginParam);  // добавляем параметр

                    SqlParameter passwordNumberParam = new SqlParameter
                    {
                        ParameterName = "@UserPassword",
                        Value = user.UserPassword
                    };
                    command.Parameters.Add(passwordNumberParam);

                    SqlParameter emailParam = new SqlParameter
                    {
                        ParameterName = "@Email",
                        Value = user.Email
                    };
                    command.Parameters.Add(emailParam);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            const string SQL_EXPRESSION = "DelUser";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure// указываем, что команда представляет хранимую процедуру
                })
                {
                    SqlParameter idParam = new SqlParameter// параметр для ввода категории
                    {
                        ParameterName = "@Id",
                        Value = id
                    };

                    command.Parameters.Add(idParam);  // добавляем параметр

                    command.ExecuteNonQuery();
                }
            }
        }

        public User FindUserIsLogin(string login, string password)
        {
            const string SQL_EXPRESSION = "FindUserIsLogin";

            User user = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure// указываем, что команда представляет хранимую процедуру
                })
                {
                    SqlParameter loginParam = new SqlParameter // параметр для ввода Login
                    {
                        ParameterName = "@UserLogin",
                        Value = login
                    };
                    command.Parameters.Add(loginParam);// добавляем параметр

                    SqlParameter passwordParam = new SqlParameter // параметр для ввода password
                    {
                        ParameterName = "@UserPassword",
                        Value = password
                    };
                    command.Parameters.Add(passwordParam);// добавляем параметр

                    SqlDataReader reader = command.ExecuteReader();

                    user = GetUserFromDb(reader);
                }
            }

            return user;
        }

        public User Get(int id)
        {
            const string SQL_EXPRESSION = "GetUser";

            User user = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure// указываем, что команда представляет хранимую процедуру
                })
                {
                    SqlParameter IdParam = new SqlParameter // параметр для ввода id
                    {
                        ParameterName = "@Id",
                        Value = id
                    };
                    command.Parameters.Add(IdParam);// добавляем параметр

                    SqlDataReader reader = command.ExecuteReader();

                    user = GetUserFromDb(reader);
                }
            }

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            const string SQL_EXPRESSION = "GetAllUsers";

            var users = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure// указываем, что команда представляет хранимую процедуру
                })
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            users.Add(new User
                            {
                                Id = reader.GetInt32(ID),
                                UserLogin = reader.GetString(USER_LOGIN),
                                UserPassword = reader.GetString(USER_PASSWORD),                               
                                UsersRoles = GetCollectionRoles(reader.GetInt32(ID))
                            });
                        }
                    }
                }
            }

            return users;
        }

        public void Update(User user)
        {
            const string SQL_EXPRESSION = "UpdateUser";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure// указываем, что команда представляет хранимую процедуру
                })
                {
                    SqlParameter passwordNumberParam = new SqlParameter
                    {
                        ParameterName = "@UserPassword",
                        Value = user.UserPassword
                    };
                    command.Parameters.Add(passwordNumberParam);

                    SqlParameter emailParam = new SqlParameter
                    {
                        ParameterName = "@Email",
                        Value = user.Email
                    };
                    command.Parameters.Add(emailParam);

                    command.ExecuteNonQuery();
                }
            }        
        }

        private ICollection<UsersRole> GetCollectionRoles(int id)
        {
            const string SQL_EXPRESSION = "GetListRolesOfUser";

            var userRoles = new List<UsersRole>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure// указываем, что команда представляет хранимую процедуру
                })
                {
                    SqlParameter IdParam = new SqlParameter // параметр для ввода id
                    {
                        ParameterName = "@Id",
                        Value = id
                    };
                    command.Parameters.Add(IdParam);// добавляем параметр

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var userRole = new UsersRole();

                        while (reader.Read())
                        {
                            userRole.RoleId = reader.GetInt32(ROLE_ID);

                            userRoles.Add(userRole);
                        }
                    }
                }
            }

            return userRoles;
        }

        private User GetUserFromDb(SqlDataReader reader)
        {
            User user = null;

            if (reader.HasRows)
            {
                user = new User();

                while (reader.Read())
                {
                    user.Id = reader.GetInt32(ID);
                    user.UserLogin = reader.GetString(USER_LOGIN);
                    user.UserPassword = reader.GetString(USER_PASSWORD);
                    user.Email = reader.GetString(EMAIL);
                    user.UsersRoles = GetCollectionRoles(reader.GetInt32(ID));
                }
            }

            return user;
        }
    }
}
