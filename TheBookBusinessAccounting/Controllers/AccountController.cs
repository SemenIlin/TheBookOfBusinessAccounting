using BLLTheBookOfBusinessAccounting.Interfaces;
using System.Web.Mvc;
using System.Web.Security;
using TheBookBusinessAccounting.Extensions;
using TheBookBusinessAccounting.Models.LoginAndRegistration;

namespace TheBookBusinessAccounting.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Find(loginModel.Login, loginModel.Password);
               
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(loginModel.Login, true);
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(loginModel);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Find(registerModel.Login, registerModel.Password);
                
                if (user == null)
                {
                    _userService.Add(registerModel.MapToDtoModel());

                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(registerModel.Login, true);
                        return RedirectToAction("Index", "User");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(registerModel);
        }

        [HttpGet]
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}  