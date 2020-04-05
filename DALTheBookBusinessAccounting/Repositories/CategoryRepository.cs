using DALTheBookBusinessAccounting.BuilderForProc;
using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        private const int ID = 0;
        private const int TITLE = 1;

        private readonly ProcForCategory _procForCategory;

        public CategoryRepository()
        {
            _procForCategory = new ProcForCategory();
        }

        public void Create(Category category)
        {
            const string SQL_EXPRESSION = "AddCategory";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForCategory.AddTitle(command, category);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            const string SQL_EXPRESSION = "DelCategory";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForCategory.AddCategoryId(command, id);                    

                    command.ExecuteNonQuery();
                }
            }
        }

        public Category Get(int id)
        {
            const string SQL_EXPRESSION = "GetCategory";

            Category category = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForCategory.AddCategoryId(command, id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        category = new Category();

                        while (reader.Read())
                        {
                            category.Id = reader.GetInt32(ID);
                            category.Title = reader.GetString(TITLE);
                        }
                    }
                }
            }

            return category;
        }

        public IEnumerable<Category> GetAll()
        {
            const string SQL_EXPRESSION = "GetAllCategories";

            var categories = new List<Category>();
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
                            categories.Add(new Category
                            {
                                Id = reader.GetInt32(ID),
                                Title = reader.GetString(TITLE)
                            });
                        }
                    }
                }
            }

            return categories;
        }

        public void Update(Category category)
        {
            const string SQL_EXPRESSION = "UpdateCategory";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForCategory.AddCategoryId(command, category);
                    _procForCategory.AddTitle(command, category);
                    
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
