using DALTheBookBusinessAccounting.Entities;
using System.Collections.Generic;

namespace DALTheBookBusinessAccounting.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category Get(int id);

        void Create(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}
