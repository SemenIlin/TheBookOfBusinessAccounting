using System.Collections.Generic;

namespace DALTheBookBusinessAccounting.Interfaces
{
    public interface IRepository<T> where T : class 
    {
        IEnumerable<T> GetAll();
        T Get(int id);

        IEnumerable<T> Find(string text, int status = default, int category = default);

        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
