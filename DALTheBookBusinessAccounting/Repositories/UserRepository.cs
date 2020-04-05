using DALTheBookBusinessAccounting.BuilderForProc;
using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private const int ID = 0;
        private const int USER_LOGIN = 1;
        private const int USER_PASSWORD = 2;
        private const int EMAIL = 3;
        private const int USER_NAME = 4;

        private const int ROLE_ID = 0;

        private readonly ProcForUser _procForUser;

        public UserRepository()
        {
            _procForUser = new ProcForUser();
        }

        public void AddRole(int userId, int roleId)
        {
            const string SQL_EXPRESSION = "AddRoleForUser";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForUser.AddUserId(command, userId);
                    _procForUser.AddRoleId(command, roleId);

                    command.ExecuteNonQuery();                
                }
            }
        }
        public void Create(User user, out int id)
        {
            const string SQL_EXPRESSION = "AddUser";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForUser.AddLogin(command, user);
                    _procForUser.AddUserName(command, user);
                    _procForUser.AddPassword(command, user);
                    _procForUser.AddEmail(command, user);
                    _procForUser.GetId(command);

                    command.ExecuteNonQuery();

                    id = (int)command.Parameters["@Id"].Value;
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
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForUser.AddUserId(command, id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRole(int userId, int roleId)
        {
            const string SQL_EXPRESSION = "DelRoleFromUser";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForUser.AddUserId(command, userId);
                    _procForUser.AddRoleId(command, roleId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<User> Find(string userLogin, int pageSize, int skip)
        {
            const string SQL_EXPRESSION = "GetUsersForPage";

            var users = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForUser.AddLogin(command, userLogin);
                    _procForUser.AddPageSize(command, pageSize);
                    _procForUser.AddSkip(command, skip);

                    SqlDataReader reader = command.ExecuteReader();
                    users = GetUsers(reader);
                }
            }

            return users;
        }

        public IEnumerable<User> Find(string userLogin)
        {
            const string SQL_EXPRESSION = "GetUsers";

            var users = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForUser.AddLogin(command, userLogin);

                    SqlDataReader reader = command.ExecuteReader();
                    users = GetUsers(reader);                  
                }
            }

            return users;
        }

        public User FindUser(string login)
        {
            const string SQL_EXPRESSION = "FindUser";

            User user = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForUser.AddLogin(command, login);

                    SqlDataReader reader = command.ExecuteReader();

                    user = GetUserFromDb(reader);
                }
            }

            return user;
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
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForUser.AddLogin(command, login);
                    _procForUser.AddPassword(command, password);                    

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
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForUser.AddUserId(command, id);

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
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    SqlDataReader reader = command.ExecuteReader();
                    users = GetUsers(reader);
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
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForUser.AddUserId(command, user);
                    _procForUser.AddPassword(command, user);
                    _procForUser.UpdateUserName(command, user);

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
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForUser.AddUserId(command, id);

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
                    user.UserName = reader.GetString(USER_NAME);
                    user.UsersRoles = GetCollectionRoles(reader.GetInt32(ID));
                }
            }

            return user;
        }

        private List<User> GetUsers(SqlDataReader reader)
        {
            var users = new List<User>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = reader.GetInt32(ID),
                        UserLogin = reader.GetString(USER_LOGIN),
                        UserPassword = reader.GetString(USER_PASSWORD),
                        UserName = reader.GetString(USER_NAME),
                        Email = reader.GetString(EMAIL),
                        UsersRoles = GetCollectionRoles(reader.GetInt32(ID))
                    });
                }
            }

            return users;
        }
    }
}
