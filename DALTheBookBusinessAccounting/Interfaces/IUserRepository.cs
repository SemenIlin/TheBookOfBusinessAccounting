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

        void Create(User item);
        void Update(User item);
        void Delete(int id);
    }
}
