using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.Services;
using DALTheBookBusinessAccounting.Entities;
using DALTheBookBusinessAccounting.Repositories;
using System.Linq;
using System.Security.Principal;

namespace BLLTheBookOfBusinessAccounting.Principals
{
    public class UserPrincipal : IPrincipal
    {
        private readonly IRoleService _roleService = new RoleService(new RoleRepository());

        public IIdentity Identity { get; }

        public User User { get; set; }

        public UserPrincipal(User user)
        {
            User = user;
            Identity = new GenericIdentity(user.UserLogin);
        }

        public bool IsInRole(string role)
        {
            return _roleService.GetAllRolesOfUser(User.UserLogin).Select(r=>r.RoleName).Contains(role);
        }
    }
}
