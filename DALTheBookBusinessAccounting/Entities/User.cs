using System.Collections.Generic;

namespace DALTheBookBusinessAccounting.Entities
{
    public class User
    {
        public User()
        {
            UsersRoles = new List<UsersRole>();
        }
        public int Id { get; set; }

        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public ICollection<UsersRole> UsersRoles { get; set; } 
    }
}
