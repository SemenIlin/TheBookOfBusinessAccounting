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

        public void Add(ItemDto itemDto, out int id)
        {
            _repositoryItem.Create(itemDto.MapToDbModel(), out id);
        }

        public void Delete(int id)
        {
            _repositoryItem.Delete(id);
        }

        public IEnumerable<ItemDto> Find(string text, int pageSize, int skip, int status = 0, string categoryName = null)
        {
            var items = _repositoryItem.Find(text, pageSize, skip, status, categoryName);
            if(items == null)
            {
                throw new NotFoundException("Предмет не найден", "");
            }

            return items.MapToListDtoModels();
        }

        public IEnumerable<ItemDto> Find(string text, int status = 0, string categoryName = null)
        {
            var items = _repositoryItem.Find(text, status, categoryName);
            if (items == null)
            {
                throw new NotFoundException("Предмет не найден", "");
            }

            return items.MapToListDtoModels();
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
