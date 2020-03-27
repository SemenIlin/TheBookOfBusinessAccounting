using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private const int ID = 0;
        private const int ROLE = 1;

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["TheBookOfBusinessAccountingContext"].ConnectionString;

        public Role Get(int id)
        {
            const string SQL_EXPRESSION = "GetRole";

            Role role = null;

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
                        role = new Role();

                        while (reader.Read())
                        {
                            role.Id = reader.GetInt32(ID);
                            role.RoleName = reader.GetString(ROLE);
                        }
                    }
                }
            }

            return role;
        }

        public IEnumerable<Role> GetAll()
        {
            const string SQL_EXPRESSION = "GetListRoles";

            var roles = new List<Role>();
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
                            roles.Add(new Role
                            {
                                Id = reader.GetInt32(ID),
                                RoleName = reader.GetString(ROLE)
                            });
                        }
                    }
                }
            }

            return roles;
        }

        public ICollection<Role> GetAllRolesOfUser(string login)
        {
            const string SQL_EXPRESSION = "GetRoles";

            var roles = new List<Role>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure// указываем, что команда представляет хранимую процедуру
                })
                {
                    SqlParameter loginParam = new SqlParameter // параметр для ввода login
                    {
                        ParameterName = "@UserLogin",
                        Value = login
                    };
                    command.Parameters.Add(loginParam);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            roles.Add(new Role
                            {
                                Id = reader.GetInt32(ID),
                                RoleName = reader.GetString(ROLE)
                            });
                        }
                    }
                }
            }

            return roles;
        }
    }
}
