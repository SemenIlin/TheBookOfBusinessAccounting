using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web;
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
                    CreateCookie(user);
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
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _userService.Find(registerModel.Login, registerModel.Password);

                    if (user == null)
                    {
                        _userService.Add(registerModel.MapToDtoModel(), out int id);
                        _userService.AddRoleForUser(id, 1);

                        if (user != null)
                        {
                            CreateCookie(user);
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
            catch
            {
                return View("NotFound");
            }
        }

        [HttpGet]
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private void CreateCookie(UserDto userDto)
        {
            var serialize = new SerializeModel
            {
                Login = userDto.UserLogin,
                Roles = userDto.RoleDtos.MapToCollectionViewModels()
            };

            var data = JsonConvert.SerializeObject(serialize);

            var ticket = new FormsAuthenticationTicket(1, userDto.UserLogin, DateTime.Now, DateTime.Now.AddMinutes(10),
                false, data);

            var encryptTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);

            HttpContext.Response.Cookies.Add(cookie);
        }

    }
}  