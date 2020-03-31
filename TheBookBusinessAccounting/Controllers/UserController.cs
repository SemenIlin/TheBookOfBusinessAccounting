using BLLTheBookOfBusinessAccounting.Interfaces;
using System.Linq;
using System.Web.Mvc;
using TheBookBusinessAccounting.Extensions;
using TheBookBusinessAccounting.Models;
using TheBookBusinessAccounting.Models.Pagination;

namespace TheBookBusinessAccounting.Controllers
{
    //[Authorize(Roles = "User, Editor, Administrator")]
    public class UserController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly IItemService _itemService;

        public UserController(
            IRoleService roleService,
            IUserService userService,
            IItemService itemService
            )
        {
            _roleService = roleService;
            _userService = userService;
            _itemService = itemService;
        }

        [HttpGet]
        public ActionResult Index(string category, int page = 1)
        {
            const int pageSize = 10; 
            var allItems = _itemService.GetAll();
            var itemPerPages = allItems.
                Where(p => category == null ||
                      p.Category == category).
                OrderBy(item => item.Title).
                Skip((page - 1) * pageSize).Take(pageSize);

            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = category == null ?
                   allItems.Count() :
                   allItems.Where(p => p.Category.ToLower().Contains(category.ToLower())).Count()
            };

            var ivm = new IndexViewModel
            {
                PageInfo = pageInfo,
                ItemViewModels = itemPerPages.MapToListViewModels(),
                ActionName = "Index"
            };

            return View(ivm);            
        }

        [HttpGet]
        [ActionName("SearchItem")]
        public ActionResult GetSearchItem(string search, int statusId = 4, int page = 1)
        {
            var ivm = GetSearchItems(search, statusId, page);

            return View("Index", ivm);
        }

        [HttpPost]
        public ActionResult SearchItem(string search, int statusId = 4, int page = 1)
        {
            var ivm = GetSearchItems(search, statusId, page);

            return View("Index", ivm);
        }

        [HttpGet]
        public ActionResult GetItem(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var item = _itemService.Get(id.Value);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item.MapToViewModel());
        }

        [HttpGet]
        public ActionResult EditUser()
        {
            var userDto = _userService.Find(User.Identity.Name);

            if(userDto == null)
            {
                return HttpNotFound();
            }

            var userViewModel = userDto.MapToViewModel();
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult EditUser(UserViewModel userViewModel)
        {
            var roles = _roleService.GetAllRolesOfUser(userViewModel.UserLogin);
            userViewModel.Roles = roles.MapToCollectionViewModels();

            if(ModelState.IsValid)
            {
                _userService.Update(userViewModel.MapToDtoModel());

                return RedirectToAction("Index");
            }            

            return View(userViewModel);
        }

        [NonAction]
        private IndexViewModel GetSearchItems(string search, int statusId = 4, int page = 1)
        {
            const int pageSize = 10; 
            var allItems = _itemService.GetAll();
            var itemPerPages = allItems.
                Where(p => search == null ||
                      p.Category.ToLower().Contains(search.ToLower()) ||
                      p.Status.ToLower().Contains(search.ToLower()) ||
                      p.Title.ToLower().Contains(search.ToLower()));

            var pageInfo = new PageInfo()
            {
                PageNumber = page,
                PageSize = pageSize
            };

            if (statusId == 4)
            {
                itemPerPages = itemPerPages.OrderBy(item => item.Title).
                    Skip((page - 1) * pageSize).Take(pageSize);

                pageInfo.TotalItems = search == null ?
                   allItems.Count() :
                   allItems.Where(
                       p => p.Category.ToLower().Contains(search.ToLower()) ||
                       p.Status.ToLower().Contains(search.ToLower()) ||
                       p.Title.ToLower().Contains(search.ToLower())).Count();
            }
            else
            {
                itemPerPages = itemPerPages.Where(p => p.StatusId == statusId).
                    OrderBy(item => item.Title).
                    Skip((page - 1) * pageSize).Take(pageSize);

                pageInfo.TotalItems = search == null ?
                  allItems.Count() :
                  allItems.Where(
                      p => p.Category.ToLower().Contains(search.ToLower()) ||
                      p.Status.ToLower().Contains(search.ToLower()) ||
                      p.Title.ToLower().Contains(search.ToLower())).
                      Where(p => p.StatusId == statusId).Count();
            }            

            var ivm = new IndexViewModel
            {
                PageInfo = pageInfo,
                ItemViewModels = itemPerPages.MapToListViewModels(),
                ActionName = "SearchItem",
                SearchText = search
            };

            return ivm;
        }
    }
}