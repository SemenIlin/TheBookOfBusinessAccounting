using System.Collections.Generic;
using System.Linq;
using BLLTheBookOfBusinessAccounting.Services;
using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest.TestDAL
{
    [TestClass]
    public class UnitTestImageRepository
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

        #region Дополнительные атрибуты тестирования
        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetImage()
        {
            //Arrage
            const int ID = 2;
            var image = new Mock<IImageRepository>();
            image.Setup(i => i.Get(ID)).Returns(
                new Image() { Id = ID, ItemId = 1, ScreenFormat = "png" }
                );

            // Act 
            var imageService = new ImageService(image.Object);
            var imageDto = imageService.Get(ID);

            //Assert
            Assert.IsNotNull(imageDto);
        }

        [TestMethod]
        public void GetAllImages()
        {
            //Arrage
            var images = new Mock<IImageRepository>();
            images.Setup(i => i.GetAll()).Returns(new List<Image> {
                new Image(){ Id = 1, ItemId = 1, ScreenFormat = "jpeg"},
                new Image(){ Id = 2, ItemId = 2, ScreenFormat = "jpeg"},
                new Image(){ Id = 3, ItemId = 3, ScreenFormat = "jpeg"},
            });

            //Act
            var imageService = new ImageService(images.Object);
            var imageDtos = imageService.GetAll().ToList();

            //Assert
            Assert.IsTrue(imageDtos.Count == 3);
        }
    }
}
