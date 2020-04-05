using BLLTheBookOfBusinessAccounting.ModelsDto;
using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Interfaces
{
    public interface ICategoryService
    {
        CategoryDto Get(int id);
        IEnumerable<CategoryDto> GetAll();

        void Add(CategoryDto categoryDto);
        void Update(CategoryDto categoryDto);
        void Delete(int id);
    }
}
