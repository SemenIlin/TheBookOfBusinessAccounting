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
    public class UnitTestStatusRepository
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
        public void GetStatus()
        {
            //Arrage
            const int ID = 2;
            var status = new Mock<IStatusRepository>();
            status.Setup(s => s.Get(ID)).Returns(
                new Status() { Id = ID, Title = "Status" });

            //Act
            var statusService = new StatusService(status.Object);
            var statusDto = statusService.Get(ID);

            //Assert
            Assert.AreEqual(statusDto.Title, "Status");
        }

        [TestMethod]
        public void GetAllStatuses()
        {
            //Arrage
            var statuses = new Mock<IStatusRepository>();
            statuses.Setup(s => s.GetAll()).Returns(new List<Status>
            {
                new Status() { Id = 1, Title = "Status1" },
                new Status() { Id = 2, Title = "Status2" },
                new Status() { Id = 3, Title = "Status3" },
            });

            //Act
            var statusServer = new StatusService(statuses.Object);
            var statusesDto = statusServer.GetAll().ToList();

            //Assert
            Assert.IsTrue(statusesDto.Count == 3);

        }
    }
}
