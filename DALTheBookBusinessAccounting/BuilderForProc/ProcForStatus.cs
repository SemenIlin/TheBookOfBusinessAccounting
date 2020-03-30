using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.BuilderForProc
{
    public class ProcForStatus
    {
        public void AddStatusId(SqlCommand command, int id)
        {
            SqlParameter IdParam = new SqlParameter
            {
                ParameterName = "@Id",
                Value = id
            };

            command.Parameters.Add(IdParam);
        }
    }
}
