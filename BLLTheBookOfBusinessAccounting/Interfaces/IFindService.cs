using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Interfaces
{
    public interface IFindService<T> where T : class
    {
        IEnumerable<T> Find(string text);
        IEnumerable<T> Find(string text, int status = 0, int category = 0);
    }
}
