using DALTheBookBusinessAccounting.Entities;
using System.Collections.Generic;

namespace DALTheBookBusinessAccounting.Interfaces
{
    public interface IRoleRepository
    { 
        IEnumerable<Role> GetAll();
        ICollection<Role> GetAllRolesOfUser(string login);
        Role Get(int id);
    }
}
