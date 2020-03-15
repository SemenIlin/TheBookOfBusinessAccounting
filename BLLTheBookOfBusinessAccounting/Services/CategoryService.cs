using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.Mappers;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using Common.Exceptions;
using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Services
{
    public class CategoryService : IReadAndEditService<CategoryDto>
    {
        private readonly IReadAndEditRepository<Category> _categoryReadAndEditRepository;

        public CategoryService(IReadAndEditRepository<Category> categoryReadAndEditRepository)
        {
            _categoryReadAndEditRepository = categoryReadAndEditRepository;
        }
            
        public void Add(CategoryDto categoryDto)
        {
            _categoryReadAndEditRepository.Create(categoryDto.MapToDbModel());
        }

        public void Update(CategoryDto categoryDto)
        {
            _categoryReadAndEditRepository.Update(categoryDto.MapToDbModel());
        }

        public void Delete(int id)
        {
            _categoryReadAndEditRepository.Delete(id);
        }

        public CategoryDto Get(int id)
        {
            var category = _categoryReadAndEditRepository.Get(id);
            if(category == null)
            {
                throw new NotFoundException("Данная категория не найдена.", "");
            }

            return category.MapToDtoModel();
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return _categoryReadAndEditRepository.GetAll().MapToListDtoModels();
        }
    }
}
