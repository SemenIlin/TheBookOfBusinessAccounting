using Ninject.Modules;
using Ninject;
using Ninject.Web.Mvc;
using TheBookBusinessAccounting.Infrastructure;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using TheBookBusinessAccounting.Models;
using TheBookOfBusinessAccounting.Principals;
using TheBookBusinessAccounting.Models.LoginAndRegistration;

namespace TheBookBusinessAccounting
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            NinjectModule registrations = new NinjectRegistrations();
            StandardKernel kernel = new StandardKernel(registrations);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var cookieUser = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);

            if (cookieUser != null)
            {
                var decryptCookieUser = FormsAuthentication.Decrypt(cookieUser.Value);//.Name.TrimStart().
                                                                                      //Replace("=","\":\"").Replace("&","\",\"").Replace("\"[","[");

                var deserializeUser = JsonConvert.DeserializeObject<SerializeModel>(decryptCookieUser.UserData);//("{\"" + decryptCookieUser + "}");
                var user = new UserViewModel
                { 
                    UserLogin = deserializeUser.Login,
                    Roles = deserializeUser.Roles
                };

                var principal = new UserPrincipal(user);

                HttpContext.Current.User = principal;
            }
        }
    }
}
