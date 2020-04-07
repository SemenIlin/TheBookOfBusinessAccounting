using System.Web.Mvc;

namespace TheBookBusinessAccounting.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}