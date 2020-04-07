using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DALTheBookBusinessAccounting.Interfaces;
using DALTheBookBusinessAccounting.Entities;
using BLLTheBookOfBusinessAccounting.Services;
using System.Linq;

namespace UnitTest.TestDAL
{
    [TestClass]
    public class UnitTestCategoryRepository
    {
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
        public void GetCategory()
        {
            //Arrage
            const int ID = 2;
            var category = new Mock<ICategoryRepository>();
            category.Setup(c => c.Get(ID)).Returns(
                new Category { Id = ID, Title = "Category1" }
            );

            //Act
            var categoryService = new CategoryService(category.Object);
            var categoryDto = categoryService.Get(ID);

            //Assert
            Assert.IsNotNull(categoryDto);
        }
        [TestMethod]
        public void GetAllCategories()
        {
            //Arrage
            var categories = new Mock<ICategoryRepository>();
            categories.Setup(c => c.GetAll()).Returns(new List<Category>
            {
                new Category(){ Id = 1, Title = "Category1"},
                new Category(){ Id = 2, Title = "Category2"},
                new Category(){ Id = 3, Title = "Category3"}
            });

            //Act
            var categoryService = new CategoryService(categories.Object);
            var categoryDto = categoryService.GetAll().ToList();

            //Assert
            Assert.IsTrue(categoryDto.Count() == 3);
        }
    }
}
