using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.Repositories
{
    public class CategoryRepository : IReadAndEditRepository<Category>
    {
        private const int ID = 0;
        private const int TITLE = 1;

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["TheBookOfBusinessAccountingContext"].ConnectionString;

        public void Create(Category category)
        {
            const string SQL_EXPRESSION = "AddCategory";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure// указываем, что команда представляет хранимую процедуру
                })
                {
                    SqlParameter nameParam = new SqlParameter// параметр для ввода категории
                    {
                        ParameterName = "@Title",
                        Value = category.Title
                    };

                    command.Parameters.Add(nameParam);  // добавляем параметр

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

        public Category Get(int id)
        {
            const string SQL_EXPRESSION = "GetCategory";

            Category category = null;

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
                    CommandType = System.Data.CommandType.StoredProcedure// указываем, что команда представляет хранимую процедуру
                })
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
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
                    CommandType = System.Data.CommandType.StoredProcedure// указываем, что команда представляет хранимую процедуру
                })
                {
                    SqlParameter IdParam = new SqlParameter 
                    {
                        ParameterName = "@Id",
                        Value = category.Id
                    };
                    command.Parameters.Add(IdParam);// добавляем параметр

                    SqlParameter titleParam = new SqlParameter // параметр для ввода Title
                    {
                        ParameterName = "@Title",
                        Value = category.Title
                    };

                    command.Parameters.Add(titleParam);// добавляем параметр

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
