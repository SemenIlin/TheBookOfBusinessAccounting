using DALTheBookBusinessAccounting.Entities;
using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.BuilderForProc
{
    public class ProcForImage
    {
        public void AddScreen(SqlCommand command, Image image)
        {
            SqlParameter screenParam = new SqlParameter
            {
                ParameterName = "@Screen",
                Value = image.Screen
            };
            command.Parameters.Add(screenParam); 
        }

        public void AddScreenFormat(SqlCommand command, Image image)
        {
            SqlParameter screenFormatParam = new SqlParameter
            {
                ParameterName = "@ScreenFormat",
                Value = image.ScreenFormat
            };
            command.Parameters.Add(screenFormatParam);
        }

        public void AddItemId(SqlCommand command, Image image)
        {
            SqlParameter itemIdParam = new SqlParameter
            {
                ParameterName = "@ItemId",
                Value = image.ItemId
            };
            command.Parameters.Add(itemIdParam);
        }

        public void AddImageId(SqlCommand command, int id)
        {
            SqlParameter idParam = new SqlParameter
            {
                ParameterName = "@Id",
                Value = id
            };
            command.Parameters.Add(idParam);
        }

        public void AddImageId(SqlCommand command, Image image)
        {
            SqlParameter idParam = new SqlParameter
            {
                ParameterName = "@Id",
                Value = image.Id
            };
            command.Parameters.Add(idParam);
        }
    }
}
