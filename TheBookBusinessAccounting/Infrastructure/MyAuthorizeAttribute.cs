using BLLTheBookOfBusinessAccounting.Principals;
using System.Web;
using System.Web.Mvc;

namespace TheBookBusinessAccounting.Infrastructure
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        private UserPrincipal userPrinciple;
        
        private string[] _allowedRoles;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {            
            if (!System.String.IsNullOrEmpty(base.Roles))
            {
                _allowedRoles = base.Roles.Split(new char[] { ',' });
                for (int i = 0; i < _allowedRoles.Length; i++)
                {
                    _allowedRoles[i] = _allowedRoles[i].Trim();
                }
            }
            return httpContext.Request.IsAuthenticated &&
                Role(httpContext);
        }

        private bool Role(HttpContextBase httpContext)
        {
            if (_allowedRoles.Length > 0)
            {
                for (int i = 0; i < _allowedRoles.Length; i++)
                {
                    if (httpContext.User.IsInRole(_allowedRoles[i]))
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }
    }
}