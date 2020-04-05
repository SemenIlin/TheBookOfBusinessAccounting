using BLLTheBookOfBusinessAccounting.ModelsDto;
using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Interfaces
{
    public interface IUserService
    {
        UserDto Find(string login, string password);
        UserDto Find(string login);
        IEnumerable<UserDto> Find(string userLogin, int pageSize, int skip);
        IEnumerable<UserDto> FindAll(string userLogin);

        UserDto Get(int id);
        IEnumerable<UserDto> GetAll();

        void AddRoleForUser(int userId, int roleId);
        void DeleteRoleFromUser(int userId, int roleId);

        void Add(UserDto item, out int id);
        void Update(UserDto item);
        void Delete(int id);
    }
}
