using DALTheBookBusinessAccounting.BuilderForProc;
using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.Repositories
{
    public class ItemRepository : BaseRepository, IItemRepository
    {
        private const int ID = 0;
        private const int TITLE = 1;
        private const int INVENTORY_NUMBER = 2;
        private const int LOCATION_OF_ITEM = 3;
        private const int ABOUT = 4;
        private const int CATEGORY_ID = 5;
        private const int STATUS_ID = 6;
        private const int CATEGORY_NAME = 7;
        private const int STATUS_NAME = 8;

        private const int SCREEN_FORMAT = 2;
                
        private readonly ProcForItem _procForItem;

        public ItemRepository()
        {
            _procForItem = new ProcForItem();
        }        

        public void Create(Item item, out int id)
        {
            const string SQL_EXPRESSION = "AddItem";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure // The command is Procedure.
                })
                {
                    _procForItem.AddName(command, item);
                    _procForItem.AddInventoryNumber(command, item);
                    _procForItem.AddLocationOfItem(command, item);
                    _procForItem.AddAbout(command, item);
                    _procForItem.AddCategoryId(command, item);
                    _procForItem.AddStatusId(command, item);
                    _procForItem.GetId(command);

                    command.ExecuteNonQuery();

                    id = (int)command.Parameters["@Id"].Value;
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
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForItem.AddItemId(command, id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Item> Find(string text, int pageSize, int skip, int status = 0, string category = default)
        {
            const string SQL_EXPRESSION = "FindItems";

            var items = new List<Item>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForItem.AddCategoryName(command, category);
                    _procForItem.AddStatusId(command, status);
                    _procForItem.AddName(command, text);
                    _procForItem.AddPageSize(command, pageSize);
                    _procForItem.AddSkip(command, skip);                   

                    SqlDataReader reader = command.ExecuteReader();
                    items = ReadFromDB(reader);
                }
            }

            return items;
        }

        public IEnumerable<Item> Find(string text, int status = 0, string category = default)
        {
            const string SQL_EXPRESSION = "FindAllItems";

            var items = new List<Item>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForItem.AddCategoryName(command, category);
                    _procForItem.AddStatusId(command, status);
                    _procForItem.AddName(command, text);

                    SqlDataReader reader = command.ExecuteReader();
                    items = ReadFromDB(reader);
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
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForItem.AddItemId(command, id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        item = new Item();

                        while (reader.Read())
                        {
                            item.Id = reader.GetInt32(ID);
                            item.Title = reader.GetString(TITLE);
                            item.InventoryNumber = reader.GetString(INVENTORY_NUMBER);
                            item.LocationOfItem = reader.GetString(LOCATION_OF_ITEM);
                            item.About = reader.GetString(ABOUT);
                            item.CategoryId = reader.GetInt32(CATEGORY_ID);
                            item.StatusId = reader.GetInt32(STATUS_ID);
                            item.CategoryName = reader.GetString(CATEGORY_NAME);
                            item.StatusName = reader.GetString(STATUS_NAME);
                            item.Images = GetCollectionImages(item.Id);
                        }
                    }
                }
            }

            return item;
        }

        public IEnumerable<Item> GetAll()
        {
            const string SQL_EXPRESSION = "GetAllItems";

            var items = new List<Item>();
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
                            var id = reader.GetInt32(ID);
                            items.Add(new Item
                            {
                                Id = id,
                                Title = reader.GetString(TITLE),
                                InventoryNumber = reader.GetString(INVENTORY_NUMBER),
                                LocationOfItem = reader.GetString(LOCATION_OF_ITEM),
                                About = reader.GetString(ABOUT),
                                CategoryId = reader.GetInt32(CATEGORY_ID),
                                StatusId = reader.GetInt32(STATUS_ID),
                                CategoryName = reader.GetString(CATEGORY_NAME),
                                StatusName = reader.GetString(STATUS_NAME),
                                Images = GetCollectionImages(id)
                            });
                        }
                    }
                }
            }

            return items;
        }    

        public void Update(Item item)
        {
            const string SQL_EXPRESSION = "UpdateItem";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForItem.AddItemId(command, item);
                    _procForItem.AddName(command, item);
                    _procForItem.AddInventoryNumber(command, item);
                    _procForItem.AddLocationOfItem(command, item);
                    _procForItem.AddAbout(command, item);
                    _procForItem.AddCategoryId(command, item);
                    _procForItem.AddStatusId(command, item);

                    command.ExecuteNonQuery();
                }
            }
        }

        public ICollection<Image> GetCollectionImages(int itemId)
        {
            const string SQL_EXPRESSION = "GetListImagesOfItem";

            var images = new List<Image>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForItem.AddItemId(command, itemId);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            images.Add(new Image
                            {
                                Id = reader.GetInt32(ID),
                                Screen = (byte[])reader["Screen"],
                                ScreenFormat = reader.GetString(SCREEN_FORMAT),
                                ItemId = itemId
                            });
                        }
                    }
                }
            }

            return images;
        }

        private List<Item> ReadFromDB(SqlDataReader reader)
        {
            var items = new List<Item>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(ID);
                    var item = new Item
                    {
                        Id = id,
                        Title = reader.GetString(TITLE),
                        InventoryNumber = reader.GetString(INVENTORY_NUMBER),
                        LocationOfItem = reader.GetString(LOCATION_OF_ITEM),
                        About = reader.GetString(ABOUT),
                        CategoryId = reader.GetInt32(CATEGORY_ID),
                        StatusId = reader.GetInt32(STATUS_ID),
                        Images = GetCollectionImages(id)
                    };

                    items.Add(item);
                }
            }
            return items;
        }
    }
}
