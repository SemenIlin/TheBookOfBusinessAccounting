using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Interfaces
{
    public interface IReadAndEditService<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();

        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}
