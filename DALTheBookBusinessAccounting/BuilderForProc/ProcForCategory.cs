using DALTheBookBusinessAccounting.Entities;
using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.BuilderForProc
{
    public class ProcForCategory
    {
        public void AddTitle(SqlCommand command, Category category)
        {
            SqlParameter nameParam = new SqlParameter
            {
                ParameterName = "@Title",
                Value = category.Title
            };
            command.Parameters.Add(nameParam);
        }

        public void AddCategoryId(SqlCommand command, int id)
        {
            SqlParameter idParam = new SqlParameter
            {
                ParameterName = "@Id",
                Value = id
            };
            command.Parameters.Add(idParam);
        }

        public void AddCategoryId(SqlCommand command, Category category)
        {
            SqlParameter IdParam = new SqlParameter
            {
                ParameterName = "@Id",
                Value = category.Id
            };
            command.Parameters.Add(IdParam);
        }
    }
}
