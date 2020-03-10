using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.Repositories
{
    public class StatusRepository : IReadRepository<Status>
    {
        private const int ID = 0;
        private const int TITLE = 1;

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["TheBookOfBusinessAccountingContext"].ConnectionString;
          
        public IEnumerable<Status> Find(Func<Status, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Status Get(int id)
        {
            const string SQL_EXPRESSION = "GetStatus";
            
            Status status = null;

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
                            status.Id = reader.GetInt32(ID);
                            status.Title = reader.GetString(TITLE);
                        }
                    }
                }
            }

            return status;
        }

        public IEnumerable<Status> GetAll()
        {
            const string SQL_EXPRESSION = "GetAllStatuses";

            List<Status> statuses = new List<Status>();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure// указываем, что команда представляет хранимую процедуру
                })
                {
                   SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            statuses.Add(new Status
                            {
                                Id = reader.GetInt32(ID),
                                Title = reader.GetString(TITLE)
                            });
                        }
                    }
                }
            }

            return statuses;        
        }
    }
}
