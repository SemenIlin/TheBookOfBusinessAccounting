using System.Collections.Generic;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DALTheBookBusinessAccounting.Interfaces;
using DALTheBookBusinessAccounting.Entities;
using BLLTheBookOfBusinessAccounting.Services;
using System.Linq;

namespace UnitTest.TestDAL
{
    [TestClass]
    public class UnitTestUserRepository
    {
        private readonly Mock<IRoleRepository> _roles;

        public UnitTestUserRepository()
        {
            _roles = new Mock<IRoleRepository>();
            _roles.Setup(r => r.GetAll()).Returns(new List<Role>
            {
                new Role(){ Id = 1, RoleName = "User"},
                new Role(){ Id = 2, RoleName = "Editor"},
                new Role(){ Id = 3, RoleName = "Admonostrator"}
            });
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
        public void GetAllUsers()
        {
            //Arrage
            var users = new Mock<IUserRepository>();
            users.Setup(m => m.GetAll()).Returns(new List<User>
            {
                new User(){ Id = 1, UserLogin = "User1", UserName = "User1", Email = "User1", UserPassword = "User1", UsersRoles = new List<UsersRole>(){ new UsersRole() { RoleId = 1, UserId = 1 } }},
                new User(){ Id = 2, UserLogin = "User2", UserName = "User2", Email = "User2", UserPassword = "User2", UsersRoles = new List<UsersRole>(){ new UsersRole() { RoleId = 1, UserId = 2 } }},
                new User(){ Id = 3, UserLogin = "User3", UserName = "User3", Email = "User3", UserPassword = "User3", UsersRoles = new List<UsersRole>(){ new UsersRole() { RoleId = 1, UserId = 3 } }}
            });

            //Act 
            var userService = new UserService(users.Object, _roles.Object);
            var userDtos = userService.GetAll();

            //Assert
            Assert.IsNotNull(userDtos);

        }

        [TestMethod]
        public void GetUser()
        {
            //Arrage
            const int ID = 2;
            var user = new Mock<IUserRepository>();
            user.Setup(m => m.Get(ID)).Returns(
                new User(){ Id = ID, UserLogin = "User1", UserName = "User1", Email = "User1", UserPassword = "User1", UsersRoles = new List<UsersRole>(){ new UsersRole() { RoleId = 1, UserId = 1 } }}
                );
            //Act
            var userService = new UserService(user.Object, _roles.Object);
            var userDto = userService.Get(ID);

            //Assert
            Assert.AreEqual(userDto.UserName, "User1");
        }

        [TestMethod]
        public void FindUserByName()
        {
            //Arrage
            const string USER_NAME = "User1";
            var user = new Mock<IUserRepository>();
            user.Setup(m => m.FindUser(USER_NAME)).Returns(
                new User() { Id = 1, UserLogin = "User1", UserName = USER_NAME, Email = "User1", UserPassword = "User1", UsersRoles = new List<UsersRole>() { new UsersRole() { RoleId = 1, UserId = 1 } } }
                );
            //Act
            var userService = new UserService(user.Object, _roles.Object);
            var userDto = userService.Find(USER_NAME);

            //Assert
            Assert.AreEqual(userDto.UserLogin, "User1");
        }

        [TestMethod]
        public void FindUsersWithSkip()
        {
            //Arrage
            const string USER_NAME = "User";
            const int PAGE_SIZE = 3;
            const int SKIP = 1;

            var user = new Mock<IUserRepository>();
            user.Setup(m => m.Find(USER_NAME, PAGE_SIZE, SKIP)).Returns(new List<User> {
                new User(){ Id = 1, UserLogin = "User1", UserName = "User1", Email = "User1", UserPassword = "User1", UsersRoles = new List<UsersRole>(){ new UsersRole() { RoleId = 1, UserId = 1 } }},
                new User(){ Id = 2, UserLogin = "User2", UserName = "User2", Email = "User2", UserPassword = "User2", UsersRoles = new List<UsersRole>(){ new UsersRole() { RoleId = 1, UserId = 2 } }},
                new User(){ Id = 3, UserLogin = "User3", UserName = "User3", Email = "User3", UserPassword = "User3", UsersRoles = new List<UsersRole>(){ new UsersRole() { RoleId = 1, UserId = 3 } }}
             });

            //Act
            var userService = new UserService(user.Object, _roles.Object);
            var userDto = userService.Find(USER_NAME, PAGE_SIZE, SKIP).ToList();

            //Assert
            Assert.IsTrue(userDto.Count == 3);
        }

        [TestMethod]
        public void FindUserIsLogin()
        {
            //Arrage
            const string USER_LOGIN = "User1";
            const string PASSWORD = "User1";

            var user = new Mock<IUserRepository>();
            user.Setup(m => m.FindUserIsLogin(USER_LOGIN, PASSWORD)).Returns(
                new User(){ Id = 1, UserLogin = "User1", UserName = "User1", Email = "User1", UserPassword = "User1", UsersRoles = new List<UsersRole>(){ new UsersRole() { RoleId = 1, UserId = 1 } }}
              );

            //Act
            var userService = new UserService(user.Object, _roles.Object);
            var userDto = userService.Find(USER_LOGIN, PASSWORD);

            //Assert
            Assert.IsNotNull(userDto);
        }
    }
}
