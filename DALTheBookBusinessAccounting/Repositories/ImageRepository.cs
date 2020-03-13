using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.Repositories
{
    public class ImageRepository : IRepository<Image>, IReadRepository<Image>
    {
        private const int ID = 0;
        private const int SCREEN_FORMAT = 2;
        private const int ITEM_ID = 3;

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["TheBookOfBusinessAccountingContext"].ConnectionString;

        public void Create(Image image)
        {
            const string SQL_EXPRESSION = "AddImage";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure// указываем, что команда представляет хранимую процедуру
                })
                {
                    SqlParameter screenParam = new SqlParameter
                    {
                        ParameterName = "@Screen",
                        Value = image.Screen
                    };
                    command.Parameters.Add(screenParam);  // добавляем параметр

                    SqlParameter screenFormatParam = new SqlParameter
                    {
                        ParameterName = "@ScreenFormat",
                        Value = image.ScreenFormat
                    };
                    command.Parameters.Add(screenFormatParam);

                    SqlParameter itemIdParam = new SqlParameter
                    {
                        ParameterName = "@ItemId",
                        Value = image.ItemId
                    };
                    command.Parameters.Add(itemIdParam);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            const string SQL_EXPRESSION = "DelImage";

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
                        Value = id
                    };

                    command.Parameters.Add(idParam);  // добавляем параметр

                    command.ExecuteNonQuery();
                }
            }
        }
        
        public Image Get(int id)
        {
            const string SQL_EXPRESSION = "GetImages";

            Image image = null;

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
                            image.Id = reader.GetInt32(ID);
                            image.Screen = (byte [])reader["Screen"];
                            image.ScreenFormat = reader.GetString(SCREEN_FORMAT);
                            image.ItemId = reader.GetInt32(ITEM_ID);                            
                        }
                    }
                }
            }

            return image;
        }

        public IEnumerable<Image> GetAll()
        {
            const string SQL_EXPRESSION = "GetAllImages";

            List<Image> images = new List<Image>();
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
                            images.Add(new Image
                            {
                                Id = reader.GetInt32(ID),
                                Screen = (byte [])reader["Screen"],
                                ScreenFormat = reader.GetString(SCREEN_FORMAT),
                                ItemId = reader.GetInt32(ITEM_ID)
                            });
                        }
                    }
                }
            }

            return images;
        }

        public void Update(Image image)
        {
            const string SQL_EXPRESSION = "UpdateImage";

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
                        Value = image.Id
                    };
                    command.Parameters.Add(idParam);

                    SqlParameter screenParam = new SqlParameter
                    {
                        ParameterName = "@Screen",
                        Value = image.Screen
                    };
                    command.Parameters.Add(screenParam);  // добавляем параметр

                    SqlParameter screenFormatParam = new SqlParameter
                    {
                        ParameterName = "@ScreenFormat",
                        Value = image.ScreenFormat
                    };
                    command.Parameters.Add(screenFormatParam);

                    SqlParameter itemIdParam = new SqlParameter
                    {
                        ParameterName = "@ItemId",
                        Value = image.ItemId
                    };
                    command.Parameters.Add(itemIdParam);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
