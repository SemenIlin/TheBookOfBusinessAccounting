using BLLTheBookOfBusinessAccounting.ModelsDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLLTheBookOfBusinessAccounting.Interfaces
{
    public interface IService<T> where T : class
    {
        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}
