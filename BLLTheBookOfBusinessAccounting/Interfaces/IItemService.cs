using BLLTheBookOfBusinessAccounting.ModelsDto;
using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Interfaces
{
    public interface IItemService
    {
        ICollection<ImageDto> GetCollectionImages(int id);
        IEnumerable<ItemDto> Find(string text, int pageSize, int skip, int status = 0, string category = null);
        IEnumerable<ItemDto> Find(string text, int status = 0, string category = null);

        ItemDto Get(int id);
        IEnumerable<ItemDto> GetAll();

        void Add(ItemDto item, out int id);
        void Update(ItemDto item);
        void Delete(int id);
    }
}
