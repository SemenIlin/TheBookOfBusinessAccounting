using DALTheBookBusinessAccounting.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DALTheBookBusinessAccounting.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id);

        User FindUserIsLogin(string login, string password);
        User FindUser(string login);
        IEnumerable<User> Find(string userLogin, int pageSize, int skip);
        IEnumerable<User> Find(string userLogin);

        void AddRole(int userId, int roleId);
        void DeleteRole(int userId, int roleId);

        void Create(User item, out int id);
        void Update(User item);
        void Delete(int id);
    }
}
