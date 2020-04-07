using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DALTheBookBusinessAccounting.Interfaces;
using DALTheBookBusinessAccounting.Entities;

namespace UnitTest.TestDAL
{
    [TestClass]
    public class UnitTestItemRepository
    {
        private readonly IItemRepository _itemRepository;

        public UnitTestItemRepository()
        {
            _itemRepository = new ItemRepositoryMock();
        }

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void AddItem()
        {
            //Arrage
            var item = new Item();
            //Act
            _itemRepository.Create(item, out int id);

            //Assert
            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void DeleteItem()
        {
            //Arrage
            const int ID = 3;
            //Act
            _itemRepository.Delete(ID);
            //Assert

        }

        [TestMethod]
        public void FindAllItems()
        {
            //Arrage
            IEnumerable<Item> items = null;

            //Act
            items = _itemRepository.Find("");

            //Assert
            Assert.IsNotNull(items);
        }

        [TestMethod]
        public void FindWithCategory()
        {
            //Arrage
            const int PAGE_SIZE = 3;
            const int SKIP = 2;
            const int CATEGORY_ID = 0;

            //Act
            var items = _itemRepository.Find("", PAGE_SIZE, SKIP, CATEGORY_ID, "");

            //Assert
            Assert.IsNotNull(items);
        }

        [TestMethod]
        public void GetAllItems()
        {
            //Act
            var items = _itemRepository.GetAll();

            //Assert
            Assert.IsNotNull(items);
        }

        [TestMethod]
        public void GetItem() 
        {
            //Arrage
            const int ID = 2;

            //Act
            var item = _itemRepository.Get(ID);

            //Assert
            Assert.IsTrue(item.Id == ID);
            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void UpdateItem()
        {
            //Arrage
            var item = new Item();

            //Act
            _itemRepository.Update(item);
        }

        [TestMethod]
        public void GetImageCollection()
        {
            //Arrage
            const int ID = 2;

            //Act
            var images = _itemRepository.GetCollectionImages(ID);

            //Assert
            Assert.IsNotNull(images);
        }

    }
}
