using System.Collections.Generic;

namespace DALTheBookBusinessAccounting.Interfaces
{
    public interface  IFindRepository<T> where T : class
    {
        IEnumerable<T> Find(string text);
        IEnumerable<T> Find(string text, int status = default, int category = default);
    }
}
