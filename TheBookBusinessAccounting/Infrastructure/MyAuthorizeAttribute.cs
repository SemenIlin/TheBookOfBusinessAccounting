using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.Services;
using DALTheBookBusinessAccounting.Repositories;
using System.Web;
using System.Web.Mvc;

namespace TheBookBusinessAccounting.Infrastructure
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] _allowedRoles;
        private readonly IRoleService _roleService;

        public MyAuthorizeAttribute(string[] roles)
        {
            _allowedRoles = roles;
            _roleService = new RoleService(new RoleRepository());
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Request.IsAuthenticated &&
                Role(_roleService,httpContext);
        }

        private bool Role(IRoleService roleService, HttpContextBase httpContext)
        {
            if (_allowedRoles.Length > 0)
            {
                var rolesOfUser = roleService.GetAllRolesOfUser(httpContext.User.Identity.Name);
                foreach(var role in rolesOfUser)
                {
                    for (int i = 0; i < _allowedRoles.Length; i++)
                    {
                        if (role.RoleName == _allowedRoles[i])
                        {
                            return true;
                        }
                    }
                }
                
                return false;
            }
            return true;
        }
    }
}