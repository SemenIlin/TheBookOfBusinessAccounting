using System.Collections.Generic;

namespace DALTheBookBusinessAccounting.Entities
{
    public class Role
    {
        public Role()
        {
            UsersRoles = new List<UsersRole>();
        }

        public int Id { get; set; }

        public string RoleName { get; set; }

        public ICollection<UsersRole> UsersRoles { get; set; }
    }
}
