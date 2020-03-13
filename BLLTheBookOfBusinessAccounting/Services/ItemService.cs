using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.Mappers;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System.Collections.Generic;
using Common.Exceptions;

namespace BLLTheBookOfBusinessAccounting.Services
{
    public class ItemService : IFindService<ItemDto>, IReadService<ItemDto>, IService<ItemDto>
    {
        private readonly IFindRepository<Item> _findItem;
        private readonly IReadRepository<Item> _readItem;
        private readonly IRepository<Item> _repositoryItem;

        public ItemService(IFindRepository<Item> findItem,
                           IReadRepository<Item> readItem,
                           IRepository<Item> repositoryItem)
        {
            _findItem = findItem;
            _readItem = readItem;
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

        public IEnumerable<ItemDto> Find(string text)
        {
            return Find(text, 0, 0);
        }

        public IEnumerable<ItemDto> Find(string text, int status = 0, int category = 0)
        {
            return _findItem.Find(text, category, status).MapToListDtoModels();
        }

        public ItemDto Get(int id)
        {
            var item = _readItem.Get(id);
            if(item == null)
            {
                throw new ItemException("Предмет не найден","");
            }

            return item.MapToDtoModel();
        }

        public IEnumerable<ItemDto> GetAll()
        {
            return _readItem.GetAll().MapToListDtoModels();
        }

        public void Update(ItemDto itemDto)
        {
            _repositoryItem.Update(itemDto.MapToDbModel());
        }
    }
}
