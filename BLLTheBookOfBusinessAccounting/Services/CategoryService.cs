using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLLTheBookOfBusinessAccounting.Services
{
    public class CategoryService : IService<CategoryDto>, IReadService<CategoryDto>
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IReadRepository<Category> _categoryReadRepository;

        public CategoryService(
            IRepository<Category> categoryRepository,
            IReadRepository<Category> categoryReadRepository
            )
        {
            _categoryRepository = categoryRepository;
            _categoryReadRepository = categoryReadRepository;
        }
            
        public void Add(CategoryDto item)
        {
            throw new NotImplementedException();
        }

        public void Update(CategoryDto item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public CategoryDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            throw new NotImplementedException();
        }


    }
}
