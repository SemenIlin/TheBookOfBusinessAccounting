using System.Linq;
using System.Security.Principal;
using TheBookBusinessAccounting.Models;

namespace TheBookOfBusinessAccounting.Principals
{
    public class UserPrincipal : IPrincipal
    {
        public IIdentity Identity { get; }

        public UserViewModel User { get; set; }

        public UserPrincipal(UserViewModel user)
        {
            User = user;
            Identity = new GenericIdentity(user.UserLogin);
        }

        public bool IsInRole(string role)
        {
            return User.Roles.Select(r=>r.RoleName).Contains(role);
        }
    }
}
