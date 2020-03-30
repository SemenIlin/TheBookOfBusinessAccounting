using DALTheBookBusinessAccounting.Entities;
using System.Collections.Generic;

namespace DALTheBookBusinessAccounting.Interfaces
{
    public interface IItemRepository
    {
        ICollection<Image> GetCollectionImages(int id);
        IEnumerable<Item> GetAll();
        Item Get(int id);

        IEnumerable<Item> Find(string text, int status = default, int category = default);

        void Create(Item item);
        void Update(Item item);
        void Delete(int id);
    }
}
