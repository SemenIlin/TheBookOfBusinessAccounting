using BLLTheBookOfBusinessAccounting.ModelsDto;
using System.Collections.Generic;

namespace BLLTheBookOfBusinessAccounting.Interfaces
{
    public interface IRoleService
    {
        RoleDto Get(int id);
        IEnumerable<RoleDto> GetAll();
        ICollection<RoleDto> GetAllRolesOfUser(string login);
    }
}
