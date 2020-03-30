using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.BuilderForProc
{
    public class ProcForRole
    {
        public void AddRoleId(SqlCommand command, int id)
        {
            SqlParameter IdParam = new SqlParameter
            {
                ParameterName = "@Id",
                Value = id
            };

            command.Parameters.Add(IdParam);
        }

        public void AddLoginUser(SqlCommand command, string login)
        {
            SqlParameter loginParam = new SqlParameter
            {
                ParameterName = "@UserLogin",
                Value = login
            };
            command.Parameters.Add(loginParam);
        }
    }
}
