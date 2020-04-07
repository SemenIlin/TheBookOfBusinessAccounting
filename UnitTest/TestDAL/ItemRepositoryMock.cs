using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.TestDAL
{
    public class ItemRepositoryMock : IItemRepository
    {
        public void Create(Item item, out int id)
        {
            id = 1;
        }

        public void Delete(int id)
        {
            
        }

        public IEnumerable<Item> Find(string text, int pageSize, int skip, int status = 0, string category = null)
        {
            return GetItems();
        }

        public IEnumerable<Item> Find(string text, int status = 0, string category = null)
        {
            return GetItems();
        }

        public Item Get(int id)
        {
            return new Item()
            {
                Id = id,
                About = "",
                CategoryId = 2,
                Title = "Item2",
                CategoryName = "Category",
                InventoryNumber = "124",
                StatusName = "Status"
            };
        }

        public IEnumerable<Item> GetAll()
        {
            return GetItems();
        }

        public ICollection<Image> GetCollectionImages(int id)
        {
            return new List<Image>()
            {
                new Image()
                {
                    Id = 1,
                    ItemId = id
                }
            };
        }

        public void Update(Item item)
        {

        }

        private IEnumerable<Item> GetItems()
        {
            return new List<Item>()
            {
                new Item()
                {
                    Id = 1,
                    About = "",
                    CategoryId = 1,
                    Title = "Item1",
                    CategoryName = "Category",
                    InventoryNumber = "123",
                    StatusName = "Status"
                },

                new Item()
                {
                    Id = 2,
                    About = "",
                    CategoryId = 2,
                    Title = "Item2",
                    CategoryName = "Category",
                    InventoryNumber = "124",
                    StatusName = "Status"
                }
            };
        }
    }
}
