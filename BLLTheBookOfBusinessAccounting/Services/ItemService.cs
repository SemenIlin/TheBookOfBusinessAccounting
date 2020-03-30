using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.Mappers;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;
using Common.Exceptions;

namespace BLLTheBookOfBusinessAccounting.Services
{
    public class ItemService :  IItemService
    {
        private readonly IItemRepository _repositoryItem;

        public ItemService(IItemRepository repositoryItem)
        {
            _repositoryItem = repositoryItem;
        }

        public void Add(ItemDto itemDto)
        {
            _repositoryItem.Create(itemDto.MapToDbModel());
        }

        public void Delete(int id)
        {
            _repositoryItem.Delete(id);
        }

        public IEnumerable<ItemDto> Find(string text, int status = 0, int category = 0)
        {
            return _repositoryItem.Find(text, category, status).MapToListDtoModels();
        }

        public ItemDto Get(int id)
        {
            var item = _repositoryItem.Get(id);
            if(item == null)
            {
                throw new NotFoundException("Предмет не найден","");
            }

            return item.MapToDtoModel();
        }

        public IEnumerable<ItemDto> GetAll()
        {
            return _repositoryItem.GetAll().MapToListDtoModels();
        }

        public ICollection<ImageDto> GetCollectionImages(int id)
        {
            return _repositoryItem.GetCollectionImages(id).MapToCollectionDtoModels();
        }

        public void Update(ItemDto itemDto)
        {
            _repositoryItem.Update(itemDto.MapToDbModel());
        }
    }
}
