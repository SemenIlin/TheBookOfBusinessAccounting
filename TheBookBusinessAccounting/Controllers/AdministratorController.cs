using BLLTheBookOfBusinessAccounting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheBookBusinessAccounting.Extensions;
using TheBookBusinessAccounting.Models;
using TheBookBusinessAccounting.Models.Pagination;

namespace TheBookBusinessAccounting.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AdministratorController(
            IUserService userService,
            IRoleService roleService
            )
        {
            _roleService = roleService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Main(string userName, int page = 1)
        {
            const int pageSize = 10;
            var allUsers = _userService.GetAll();
            var userPerPages = allUsers.
                Where(p => userName == null ||
                      p.UserLogin == userName).
                OrderBy(item => item.UserLogin).
                Skip((page - 1) * pageSize).Take(pageSize);

            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = userName == null ?
                   allUsers.Count() :
                   allUsers.Where(p => p.UserName.ToLower().Contains(userName.ToLower())).Count()
            };

            var ivm = new MainViewModel
            {
                PageInfo = pageInfo,
                UserViewModels = userPerPages.MapToListViewModels(),
                ActionName = "Main"
            };

            return View(ivm);
        }

        [HttpPost]
        public ActionResult SearchUser(string search, int page = 1)
        {
            var ivm = GetSearchUsers(search, page);

            return View("Main", ivm);
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            var userViewModel = new UserViewModel();
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult CreateUser(UserViewModel userViewModel)
        {
            if(ModelState.IsValid)
            {
                _userService.Add(userViewModel.MapToDtoModel());
                var userDto = _userService.GetAll().LastOrDefault();
                _userService.AddRoleForUser(userDto.Id, userViewModel.RoleId);

                return RedirectToAction("Index", "User");
            }

            return View(userViewModel);
        }

        [HttpGet]
        public ActionResult EditUser(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            var userDto = _userService.Get(id.Value);
            if(userDto == null)
            {
                return HttpNotFound();
            }

            return View(userDto.MapToViewModel());
        }

        [HttpPost]
        public ActionResult EditUser(UserViewModel userViewModel)
        {
            userViewModel.Roles = _roleService.
                GetAllRolesOfUser(userViewModel.UserLogin).
                MapToCollectionViewModels();

            if(ModelState.IsValid)
            {
                _userService.DeleteRoleFromUser(userViewModel.Id, userViewModel.RoleId);
                _userService.Update(userViewModel.MapToDtoModel());

                return RedirectToAction ("Main");
            }

            return View(userViewModel);
        }

        [HttpGet]
        public ActionResult DeleteUser(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            var userDto = _userService.Get(id.Value);
            if(userDto == null)
            {
                return HttpNotFound();
            }

            return View(userDto.MapToViewModel());
        }

        [HttpPost]
        [ActionName("DeleteUser")]
        public ActionResult ConfirmDeleteUser(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            var userDto = _userService.Get(id.Value);
            if(userDto == null)
            {
                return HttpNotFound();
            }

            _userService.Delete(id.Value);
            return View("Main");
        }

        [NonAction]
        private MainViewModel GetSearchUsers(string search, int page = 1)
        {
            const int pageSize = 10;
            var allUsers = _userService.GetAll();
            var userPerPages = allUsers.
                Where(p => search == null ||
                      p.UserLogin.ToLower().Contains(search.ToLower())). 
                OrderBy(item => item.UserLogin).
                Skip((page - 1) * pageSize).Take(pageSize);

            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = search == null ?
                   allUsers.Count() :
                   allUsers.Where(p => p.UserLogin.ToLower().Contains(search.ToLower())).Count()
            };

            var mvm = new MainViewModel()
            {
                PageInfo = pageInfo,
                UserViewModels = userPerPages.MapToListViewModels(),
                ActionName = "SearchUser",
                SearchText = search
            };

            return mvm;
        }

    }
}