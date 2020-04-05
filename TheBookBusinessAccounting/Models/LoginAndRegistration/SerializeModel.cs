using System.Collections.Generic;

namespace TheBookBusinessAccounting.Models.LoginAndRegistration
{
    public class SerializeModel
    {
        public string Login { get; set; }
        public ICollection<RoleViewModel> Roles { get; set; }
    }
}