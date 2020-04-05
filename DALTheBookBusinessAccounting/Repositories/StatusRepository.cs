using DALTheBookBusinessAccounting.BuilderForProc;
using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DALTheBookBusinessAccounting.Repositories
{
    public class StatusRepository : BaseRepository, IStatusRepository
    {
        private const int ID = 0;
        private const int TITLE = 1;

        private readonly ProcForStatus _procForStatus;

        public StatusRepository()
        {
            _procForStatus = new ProcForStatus();
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
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    _procForStatus.AddStatusId(command, id);                  

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        status = new Status();

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

            var statuses = new List<Status>();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand(SQL_EXPRESSION, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                   SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) 
                    {
                        while (reader.Read())
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
