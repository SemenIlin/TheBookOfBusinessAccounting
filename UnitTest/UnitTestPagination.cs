using System.Collections.Generic;
using System.Web.Mvc;
using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TheBookBusinessAccounting.Controllers;
using TheBookBusinessAccounting.Models.Pagination;

namespace UnitTest
{
    [TestClass]
    public class UnitTestPagination
    {
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            var mock = new Mock<IItemService>();
            mock.Setup(m => m.Find("", 0, "")).Returns(new List<ItemDto>
            {
                new ItemDto { Id = 1,  CategoryId = 0, StatusId = 0, Title = "Item1"},
                new ItemDto { Id = 2,  CategoryId = 0, StatusId = 0, Title = "Item2"},
                new ItemDto { Id = 3,  CategoryId = 0, StatusId = 0, Title = "Item3"},
                new ItemDto { Id = 4,  CategoryId = 0, StatusId = 0, Title = "Item4"},
                new ItemDto { Id = 5,  CategoryId = 0, StatusId = 0, Title = "Item5"},
                new ItemDto { Id = 6,  CategoryId = 0, StatusId = 0, Title = "Item6"},
                new ItemDto { Id = 7,  CategoryId = 0, StatusId = 0, Title = "Item7"},
                new ItemDto { Id = 8,  CategoryId = 0, StatusId = 0, Title = "Item8"},
                new ItemDto { Id = 9,  CategoryId = 0, StatusId = 0, Title = "Item9"},
                new ItemDto { Id = 10, CategoryId = 0, StatusId = 0, Title = "Item10"},
                new ItemDto { Id = 11, CategoryId = 0, StatusId = 0, Title = "Item11"}
            });

            using (var controller = new UserController(null, null, mock.Object))
            {
                // Act
                var result = controller.Index("", 2) as ViewResult;
                var viewModel = result.ViewData.Model as IndexViewModel;

                // Assert
                PageInfo pageInfo = viewModel.PageInfo;
                Assert.AreEqual(pageInfo.PageNumber, 2);
                Assert.AreEqual(pageInfo.PageSize, 10);
                Assert.AreEqual(pageInfo.TotalItems, 11);
                Assert.AreEqual(pageInfo.TotalPages, 2);
            }
        }
    }
}
