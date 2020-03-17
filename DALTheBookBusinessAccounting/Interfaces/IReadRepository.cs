using System.Collections.Generic;

namespace DALTheBookBusinessAccounting.Interfaces
{
    public interface IReadRepository<T> where T : class
    { 
        IEnumerable<T> GetAll();
        T Get(int id);
    }
}
