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
    [MyAuthorize(Roles = "User")]
    public class UserController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly IItemService _itemService;

        private readonly Cache<ItemDto> _itemCache;
        private readonly Cache<UserDto> _userCache;

        public UserController(
            IRoleService roleService,
            IUserService userService,
            IItemService itemService
            )
        {
            _roleService = roleService;
            _userService = userService;
            _itemService = itemService;

            _itemCache = new Cache<ItemDto>();
            _userCache = new Cache<UserDto>();
        }

        [HttpGet]
        public ActionResult Index(string category = "", int page = 1)
        {
            try
            {
                const int pageSize = 10;

                var itemsPerPage = _itemService.Find("", pageSize, page - 1, 0, category);
                var allItems = _itemService.Find("", 0, category);

                var pageInfo = new PageInfo
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalItems = allItems.Count()
                };

                var ivm = new IndexViewModel
                {
                    PageInfo = pageInfo,
                    ItemViewModels = itemsPerPage.MapToListViewModels(),
                    ActionName = "Index",
                    CurrentCategory = category
                };

                return View(ivm);
            }
            catch(NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("NotFound");
            }
        }

        [HttpGet]
        [ActionName("SearchItem")]
        public ActionResult GetSearchItem(string search, string category = "", int statusId = 0, int page = 1)
        {
            try
            {
                var ivm = GetSearchItems(search, statusId, page, category);

                return View("Index", ivm);
            }
            catch(NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("NotFound");
            }
        }

        [HttpPost]
        public ActionResult SearchItem(string search, string category = "", int statusId = 0, int page = 1)
        {
            try
            {
                var ivm = GetSearchItems(search, statusId, page, category);

                return View("Index", ivm);
            }
            catch(NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("NotFound");
            }
        }

        [HttpGet]
        public ActionResult GetItem(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            try
            {
                var item = _itemCache.GetValue(id.Value);
                if(item == null)
                {
                    item = _itemService.Get(id.Value);
                    _itemCache.Add(item, item.Id);
                }
                
                if (item == null)
                {
                    return View("NotFound");
                }

                return View(item.MapToViewModel());
            }
            catch(NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("NotFound");
            }
        }

        [HttpGet]
        public ActionResult EditUser()
        {
            try
            {
                var userDto = _userCache.GetValue(User.Identity.Name);
                if(userDto == null)
                {
                    userDto = _userService.Find(User.Identity.Name);
                    _userCache.Add(userDto, userDto.UserName);
                }
                

                if (userDto == null)
                {
                    return View("NotFound");
                }

                var userViewModel = userDto.MapToViewModel();
                return View(userViewModel);
            }
            catch(NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("NotFound");
            }
        }

        [HttpPost]
        public ActionResult EditUser(UserViewModel userViewModel)
        {
            try
            {
                var roles = _roleService.GetAllRolesOfUser(userViewModel.UserLogin);
                userViewModel.Roles = roles.MapToCollectionViewModels();

                if (ModelState.IsValid)
                {
                    var userDto = userViewModel.MapToDtoModel();
                    _userService.Update(userDto);
                    _userCache.Update(userDto, userDto.Id);

                    return RedirectToAction("Index");
                }

                return View(userViewModel);
            }
            catch(NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("NotFound");
            }
        }

        private IndexViewModel GetSearchItems(string search, int statusId = 0, int page = 1, string category = "")
        {
            const int pageSize = 10;
            var itemsPerPage = _itemService.Find(search, pageSize, page - 1, statusId, category);
            var allItems = _itemService.Find(search, statusId, category);

            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = allItems.Count()
            };

            var ivm = new IndexViewModel
            {
                PageInfo = pageInfo,
                ItemViewModels = itemsPerPage.MapToListViewModels(),
                ActionName = "SearchItem",
                SearchText = search,
                CurrentCategory = category
            };

            return ivm;
        }
    }
}