using System.Configuration;

namespace DALTheBookBusinessAccounting.Repositories
{
    public class BaseRepository
    {
        protected readonly string connectionString = ConfigurationManager.ConnectionStrings["TheBookOfBusinessAccountingContext"].ConnectionString;
    }
}
