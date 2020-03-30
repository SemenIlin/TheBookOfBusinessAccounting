using BLLTheBookOfBusinessAccounting.ModelsDto;
using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Interfaces
{
    public interface IUserService
    {
        UserDto Find(string login, string password);
        UserDto Find(string login);

        UserDto Get(int id);
        IEnumerable<UserDto> GetAll();

        void Add(UserDto item);
        void Update(UserDto item);
        void Delete(int id);
    }
}
