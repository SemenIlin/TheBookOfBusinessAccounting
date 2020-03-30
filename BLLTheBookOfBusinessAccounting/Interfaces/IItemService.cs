using BLLTheBookOfBusinessAccounting.ModelsDto;
using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Interfaces
{
    public interface IItemService
    {
        ICollection<ImageDto> GetCollectionImages(int id);
        IEnumerable<ItemDto> Find(string text, int status = 0, int category = 0);

        ItemDto Get(int id);
        IEnumerable<ItemDto> GetAll();

        void Add(ItemDto item);
        void Update(ItemDto item);
        void Delete(int id);
    }
}
