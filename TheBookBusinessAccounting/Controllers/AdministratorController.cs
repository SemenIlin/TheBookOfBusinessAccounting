using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using Common.Exceptions;
using System.Linq;
using System.Web.Mvc;
using TheBookBusinessAccounting.Extensions;
using TheBookBusinessAccounting.Infrastructure;
using TheBookBusinessAccounting.Models;
using TheBookBusinessAccounting.Models.Pagination;

namespace TheBookBusinessAccounting.Controllers
{
    [MyAuthorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        private readonly Cache<UserDto> _userCache;

        public AdministratorController(
            IUserService userService,
            IRoleService roleService
            )
        {
            _roleService = roleService;
            _userService = userService;

            _userCache = new Cache<UserDto>();
        }

        [HttpGet]
        public ActionResult Main(string userName = "", int page = 1)
        {
            try
            {
                const int pageSize = 10;
                var allUsers = _userService.FindAll(userName);
                var userPerPages = _userService.Find(userName, pageSize, page - 1);

                var pageInfo = new PageInfo
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalItems = allUsers.Count() 
                };

                var ivm = new MainViewModel
                {
                    PageInfo = pageInfo,
                    UserViewModels = userPerPages.MapToListViewModels(),
                    ActionName = "Main"
                };

                return View(ivm);
            }
            catch(NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("Error");
            }
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
            try
            {
                if (ModelState.IsValid)
                {
                    var userDto = userViewModel.MapToDtoModel();
                    _userService.Add(userDto, out int id);
                    _userService.AddRoleForUser(id, userViewModel.RoleId);
                    _userCache.Add(userDto, id);

                    return RedirectToAction("Index", "User");
                }

                return View(userViewModel);
            }
            catch(NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult EditUser(int? id)
        {
            if(id == null)
            {
                return View("NotFound");
            }
            try
            {
                var userDto = _userCache.GetValue(id.Value);
                if (userDto == null)
                {
                    userDto = _userService.Get(id.Value);
                    _userCache.Add(userDto, userDto.Id);
                }

                if (userDto == null)
                {
                    return View("NotFound");
                }

                return View(userDto.MapToViewModel());
            }
            catch(NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult EditUser(UserViewModel userViewModel)
        {
            userViewModel.Roles = _roleService.
                GetAllRolesOfUser(userViewModel.UserLogin).
                MapToCollectionViewModels();

            if(ModelState.IsValid)
            {
                var userDto = userViewModel.MapToDtoModel();
                _userService.DeleteRoleFromUser(userViewModel.Id, userViewModel.RoleId);
                _userService.Update(userDto);
                _userCache.Update(userDto, userViewModel.Id);

                return RedirectToAction ("Main");
            }

            return View(userViewModel);
        }

        [HttpGet]
        public ActionResult DeleteUser(int? id)
        {
            if(id == null)
            {
                return View("NotFound");
            }
            try
            {
                var userDto = _userCache.GetValue(id.Value);
                if(userDto == null)
                {
                    userDto = _userService.Get(id.Value);
                    _userCache.Add(userDto, userDto.Id);
                }

                if (userDto == null)
                {
                    return View("NotFound");
                }

                return View(userDto.MapToViewModel());
            }
            catch(NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ActionName("DeleteUser")]
        public ActionResult ConfirmDeleteUser(int? id)
        {
            if(id == null)
            {
                return View("NotFound");
            }
            try
            {
                var userDto = _userCache.GetValue(id.Value);
                if(userDto == null)
                {
                    userDto = _userService.Get(id.Value);
                    _userCache.Add(userDto, userDto.Id);
                }
                
                if (userDto == null)
                {
                    return View("NotFound");
                }

                _userService.Delete(id.Value);
                return RedirectToAction("Main");
            }
            catch (NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("Error");
            }
        }
                
        private MainViewModel GetSearchUsers(string search, int page = 1)
        {
            try
            {
                const int pageSize = 10;
                var allUsers = _userService.FindAll(search);
                var userPerPages = _userService.Find(search, pageSize, page - 1);

                var pageInfo = new PageInfo
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalItems = allUsers.Count()
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
            catch 
            {
                return null;
            }
        }
    }
}