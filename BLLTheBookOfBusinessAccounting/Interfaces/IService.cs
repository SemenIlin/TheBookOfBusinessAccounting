using BLLTheBookOfBusinessAccounting.ModelsDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLLTheBookOfBusinessAccounting.Interfaces
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> Find(string text, int status = 0, int category = 0);

        T Get(int id);
        IEnumerable<T> GetAll();

        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}
