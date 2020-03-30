using DALTheBookBusinessAccounting.Entities;
using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.BuilderForProc
{
    public class ProcForItem
    {
        public void AddItemId(SqlCommand command, Item item)
        {
            SqlParameter idParam = new SqlParameter
            {
                ParameterName = "@Id",
                Value = item.Id
            };
            command.Parameters.Add(idParam);
        }

        public void AddName(SqlCommand command, string text)
        {
            SqlParameter textParam = new SqlParameter
            {
                ParameterName = "@Name",
                Value = text
            };
            command.Parameters.Add(textParam);
        }

        public void AddName(SqlCommand command, Item item)
        {
            SqlParameter nameParam = new SqlParameter// it's parameter for input title
            {
                ParameterName = "@Title",
                Value = item.Title
            };
            command.Parameters.Add(nameParam);  //the command for add parameter
        }

        public void AddInventoryNumber(SqlCommand command, Item item)
        {
            SqlParameter inventoryNumberParam = new SqlParameter
            {
                ParameterName = "@InventoryNumber",
                Value = item.InventoryNumber
            };
            command.Parameters.Add(inventoryNumberParam);
        }

        public void AddLocationOfItem(SqlCommand command, Item item)
        {
            SqlParameter locationOfItemParam = new SqlParameter
            {
                ParameterName = "@LocationOfItem",
                Value = item.LocationOfItem
            };
            command.Parameters.Add(locationOfItemParam);
        }

        public void AddAbout(SqlCommand command, Item item)
        {
            SqlParameter aboutParam = new SqlParameter
            {
                ParameterName = "@About",
                Value = item.About
            };
            command.Parameters.Add(aboutParam);
        }

        public void AddCategoryId(SqlCommand command, Item item)
        {
            SqlParameter categoryIdParam = new SqlParameter
            {
                ParameterName = "@CategoryId",
                Value = item.CategoryId
            };
            command.Parameters.Add(categoryIdParam);
        }

        public void AddCategoryId(SqlCommand command, int category)
        {
            SqlParameter categoryParam = new SqlParameter
            {
                ParameterName = "@CategoryId",
                Value = category
            };
            command.Parameters.Add(categoryParam);
        }

        public void AddStatusId(SqlCommand command, Item item)
        {
            SqlParameter statusIdParam = new SqlParameter
            {
                ParameterName = "@StatusId",
                Value = item.StatusId
            };
            command.Parameters.Add(statusIdParam);
        }

        public void AddStatusId(SqlCommand command, int status)
        {
            SqlParameter statusParam = new SqlParameter
            {
                ParameterName = "@StatusId",
                Value = status
            };
            command.Parameters.Add(statusParam);
        }

        public void AddItemId(SqlCommand command, int id)
        {
            SqlParameter idParam = new SqlParameter
            {
                ParameterName = "@Id",
                Value = id
            };

            command.Parameters.Add(idParam);  
        }
    }
}
