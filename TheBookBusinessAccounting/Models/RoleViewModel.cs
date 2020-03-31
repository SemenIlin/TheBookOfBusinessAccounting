using System.Collections.Generic;

namespace TheBookBusinessAccounting.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        
        public ICollection<UserViewModel> UserViewModels { get; set; }
    }
}