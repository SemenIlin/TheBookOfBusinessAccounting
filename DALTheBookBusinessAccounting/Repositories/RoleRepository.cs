using DALTheBookBusinessAccounting.BuilderForProc;
using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        private const int ID = 0;
        private const int ROLE = 1;

       private readonly ProcForRole _procForRole;

        public  RoleRepository()
        {
            _procForRole = new ProcForRole();
        }

        public Role Get(int id)
        {
            const string SQL_EXPRESSION = "GetRole";

            Role role = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForRole.AddRoleId(command, id);

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
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) 
                    {
                        while (reader.Read()) 
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
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForRole.AddLoginUser(command, login);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) 
                    {
                        while (reader.Read()) 
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
