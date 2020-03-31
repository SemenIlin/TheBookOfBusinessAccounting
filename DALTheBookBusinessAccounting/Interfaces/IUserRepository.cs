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

        void AddRole(int userId, int roleId);
        void DeleteRole(int userId, int roleId);

        void Create(User item);
        void Update(User item);
        void Delete(int id);
    }
}
