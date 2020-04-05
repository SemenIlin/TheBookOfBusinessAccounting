using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.Mappers;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using Common.Exceptions;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
            
        public void Add(CategoryDto categoryDto)
        {
            _categoryRepository.Create(categoryDto.MapToDbModel());
        }

        public void Update(CategoryDto categoryDto)
        {
            _categoryRepository.Update(categoryDto.MapToDbModel());
        }

        public void Delete(int id)
        {
            _categoryRepository.Delete(id);
        }

        public CategoryDto Get(int id)
        {
            var category = _categoryRepository.Get(id);
            if(category == null)
            {
                throw new NotFoundException("Данная категория не найдена.", "");
            }

            return category.MapToDtoModel();
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return _categoryRepository.GetAll().MapToListDtoModels();
        }
    }
}
