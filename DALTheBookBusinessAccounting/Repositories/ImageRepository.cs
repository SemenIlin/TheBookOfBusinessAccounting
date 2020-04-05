using DALTheBookBusinessAccounting.BuilderForProc;
using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.Repositories
{
    public class ImageRepository : BaseRepository, IImageRepository
    {
        private const int ID = 0;
        private const int SCREEN_FORMAT = 2;
        private const int ITEM_ID = 3;

        private readonly ProcForImage _procForImage;

        public ImageRepository()
        {
            _procForImage = new ProcForImage();
        }

        public void Create(Image image, out int id)
        {
            const string SQL_EXPRESSION = "AddImage";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForImage.AddScreen(command, image);
                    _procForImage.AddScreenFormat(command, image);
                    _procForImage.AddItemId(command, image);
                    _procForImage.GetId(command);

                    command.ExecuteNonQuery();

                    id = (int)command.Parameters["@Id"].Value;
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
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForImage.AddImageId(command, id);

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
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForImage.AddImageId(command, id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        image = new Image();

                        while (reader.Read())
                        {
                            image.ScreenFormat = reader.GetString(SCREEN_FORMAT);
                            image.ItemId = reader.GetInt32(ITEM_ID);
                            image.Screen = (byte[])reader["Screen"];
                        }
                    }
                }
            }

            return image;
        }

        public IEnumerable<Image> GetAll()
        {
            const string SQL_EXPRESSION = "GetAllImages";

            var images = new List<Image>();
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
                    _procForImage.AddImageId(command, image);
                    _procForImage.AddScreen(command, image);
                    _procForImage.AddScreenFormat(command, image);
                    _procForImage.AddItemId(command, image);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
