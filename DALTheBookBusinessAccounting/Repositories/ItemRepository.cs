using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.Repositories
{
    public class ItemRepository : IRepository<Item>, IReadRepository<Item>, IFindRepository<Item>
    {
        private const int ID = 0;
        private const int TITLE = 1;
        private const int INVENTORY_NUMBER = 2;
        private const int LOCATION_OF_ITEM = 3;
        private const int ABOUT = 4;
        private const int CATEGORY_ID = 5;
        private const int STATUS_ID = 6;

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["TheBookOfBusinessAccountingContext"].ConnectionString;

        public void Create(Item item)
        {
            const string SQL_EXPRESSION = "AddItem";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure// указываем, что команда представляет хранимую процедуру
                })
                {
                    SqlParameter nameParam = new SqlParameter// параметр для ввода title
                    {
                        ParameterName = "@Title",
                        Value = item.Title
                    };
                    command.Parameters.Add(nameParam);  // добавляем параметр

                    SqlParameter inventoryNumberParam = new SqlParameter
                    {
                        ParameterName = "@InventoryNumber",
                        Value = item.InventoryNumber
                    };
                    command.Parameters.Add(inventoryNumberParam);

                    SqlParameter locationOfItemParam = new SqlParameter
                    {
                        ParameterName = "@LocationOfItem",
                        Value = item.LocationOfItem
                    };
                    command.Parameters.Add(locationOfItemParam);

                    SqlParameter aboutParam = new SqlParameter
                    {
                        ParameterName = "@About",
                        Value = item.About
                    };
                    command.Parameters.Add(aboutParam);

                    SqlParameter categoryIdParam = new SqlParameter
                    {
                        ParameterName = "@CategoryId",
                        Value = item.CategoryId
                    };
                    command.Parameters.Add(categoryIdParam);

                    SqlParameter statusIdParam = new SqlParameter
                    {
                        ParameterName = "@StatusId",
                        Value = item.StatusId
                    };
                    command.Parameters.Add(statusIdParam);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            const string SQL_EXPRESSION = "DelItem";

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

        public IEnumerable<Item> Find(string text)
        {
            return Find(text, 0, 0);
        }


        public IEnumerable<Item> Find(string text, int status = 0, int category = 0)
        {
            const string SQL_EXPRESSION = "FindItem";

            List<Item> items = new List<Item>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure// указываем, что команда представляет хранимую процедуру
                })
                {
                    SqlParameter textParam = new SqlParameter // параметр для ввода id
                    {
                        ParameterName = "@Name",
                        Value = text
                    };
                    command.Parameters.Add(textParam);// добавляем параметр

                    SqlParameter categoryParam = new SqlParameter // параметр для ввода id
                    {
                        ParameterName = "@CategoryId",
                        Value = category
                    };
                    command.Parameters.Add(categoryParam);// добавляем параметр

                    SqlParameter statusParam = new SqlParameter // параметр для ввода id
                    {
                        ParameterName = "@StatusId",
                        Value = status
                    };
                    command.Parameters.Add(statusParam);// добавляем параметр

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            items.Add(new Item
                            {
                                Id = reader.GetInt32(ID),
                                Title = reader.GetString(TITLE),
                                InventoryNumber = reader.GetString(INVENTORY_NUMBER),
                                LocationOfItem = reader.GetString(LOCATION_OF_ITEM),
                                About = reader.GetString(ABOUT),
                                CategoryId = reader.GetInt32(CATEGORY_ID),
                                StatusId = reader.GetInt32(STATUS_ID)
                            });
                        }
                    }
                }
            }

            return items;
        }

        public Item Get(int id)
        {
            const string SQL_EXPRESSION = "GetItem";

            Item item = null;

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
                        while (reader.Read())
                        {
                            item.Id = reader.GetInt32(ID);
                            item.Title = reader.GetString(TITLE);
                            item.InventoryNumber = reader.GetString(INVENTORY_NUMBER);
                            item.LocationOfItem = reader.GetString(LOCATION_OF_ITEM);
                            item.About = reader.GetString(ABOUT);
                            item.CategoryId = reader.GetInt32(CATEGORY_ID);
                            item.StatusId = reader.GetInt32(STATUS_ID);
                        }
                    }
                }
            }

            return item;
        }

        public IEnumerable<Item> GetAll()
        {
            const string SQL_EXPRESSION = "GetAllItems";

            List<Item> items = new List<Item>();
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
                            items.Add(new Item
                            {
                                Id = reader.GetInt32(ID),
                                Title = reader.GetString(TITLE),
                                InventoryNumber = reader.GetString(INVENTORY_NUMBER),
                                LocationOfItem = reader.GetString(LOCATION_OF_ITEM),
                                About = reader.GetString(ABOUT),
                                CategoryId = reader.GetInt32(CATEGORY_ID),
                                StatusId = reader.GetInt32(STATUS_ID)
                            });
                        }
                    }
                }
            }

            return items;
        }    

        public void Update(Item item)
        {
            const string SQL_EXPRESSION = "AddItem";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure// указываем, что команда представляет хранимую процедуру
                })
                {
                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = item.Id
                    };
                    command.Parameters.Add(idParam);

                    SqlParameter nameParam = new SqlParameter// параметр для ввода title
                    {
                        ParameterName = "@Title",
                        Value = item.Title
                    };
                    command.Parameters.Add(nameParam);  // добавляем параметр

                    SqlParameter inventoryNumberParam = new SqlParameter
                    {
                        ParameterName = "@InventoryNumber",
                        Value = item.InventoryNumber
                    };
                    command.Parameters.Add(inventoryNumberParam);

                    SqlParameter locationOfItemParam = new SqlParameter
                    {
                        ParameterName = "@LocationOfItem",
                        Value = item.LocationOfItem
                    };
                    command.Parameters.Add(locationOfItemParam);

                    SqlParameter aboutParam = new SqlParameter
                    {
                        ParameterName = "@About",
                        Value = item.About
                    };
                    command.Parameters.Add(aboutParam);

                    SqlParameter categoryIdParam = new SqlParameter
                    {
                        ParameterName = "@CategoryId",
                        Value = item.CategoryId
                    };
                    command.Parameters.Add(categoryIdParam);

                    SqlParameter statusIdParam = new SqlParameter
                    {
                        ParameterName = "@StatusId",
                        Value = item.StatusId
                    };
                    command.Parameters.Add(statusIdParam);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
