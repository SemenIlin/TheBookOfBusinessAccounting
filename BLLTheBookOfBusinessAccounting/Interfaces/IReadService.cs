using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Interfaces
{
    public interface IReadService<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
