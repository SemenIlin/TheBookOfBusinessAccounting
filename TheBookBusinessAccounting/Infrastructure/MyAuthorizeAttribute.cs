using System.Web;
using System.Web.Mvc;

namespace TheBookBusinessAccounting.Infrastructure
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {     
            return httpContext.Request.IsAuthenticated &&
                Role(httpContext);
        }

        private bool Role(HttpContextBase httpContext)
        {
            if (Roles == null)
            {
                return true;
            }

            return httpContext.User.IsInRole(Roles) == true ? true : false;            
        }
    }
}