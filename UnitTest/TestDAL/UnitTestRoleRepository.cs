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
    public class UnitTestRoleRepository
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
        public void GetRole()
        {
            //Arrage
            const int ID = 2;
            var role = new Mock<IRoleRepository>();
            role.Setup(m => m.Get(ID)).Returns(
                new Role() { Id = ID, RoleName = "User" }
                );

            //Act
            var roleService = new RoleService(role.Object);
            var roleDto = roleService.Get(ID);

            //Assert
            Assert.AreNotEqual(roleDto.RoleName, "User1");
        }

        [TestMethod]
        public void GetAllRoles()
        {
            //Arrage
            var roles = new Mock<IRoleRepository>();
            roles.Setup(r => r.GetAll()).Returns(new List<Role>
            {
                new Role(){ Id = 1, RoleName = "User"},
                new Role(){ Id = 2, RoleName = "Editor"},
                new Role(){ Id = 3, RoleName = "Admonostrator"}
            });

            //Act
            var roleService = new RoleService(roles.Object);
            var roleDtos = roleService.GetAll().ToList();

            //Assert
            Assert.IsTrue(roleDtos.Count() == 3);
        }

        [TestMethod]
        public void GetAllRolesOfUser()
        {
            //Arrage
            const string USER_NAME = "User";
            var roles = new Mock<IRoleRepository>();
            roles.Setup(r => r.GetAllRolesOfUser(USER_NAME)).Returns(new List<Role>
            {
                new Role{ Id = 1, RoleName = "User", 
                    UsersRoles = new List<UsersRole>()
                    { 
                        new UsersRole{ Id = 1, RoleId = 1, UserId = 1 }
                    } },
                new Role{ Id = 2, RoleName = "Editor", 
                    UsersRoles = new List<UsersRole>()
                    {
                        new UsersRole{ Id = 2, RoleId = 2, UserId = 1 }
                    }
                }
            });

            //Act
            var roleService = new RoleService(roles.Object);
            var roleDtos = roleService.GetAllRolesOfUser(USER_NAME).ToList();

            //Assert
            Assert.IsTrue(roleDtos.Count == 2);
        }
    }
}
