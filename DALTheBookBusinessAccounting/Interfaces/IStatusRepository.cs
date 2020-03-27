using DALTheBookBusinessAccounting.Entities;
using System.Collections.Generic;

namespace DALTheBookBusinessAccounting.Interfaces
{
    public interface IStatusRepository
    {
        IEnumerable<Status> GetAll();
        Status Get(int id);
    }
}
