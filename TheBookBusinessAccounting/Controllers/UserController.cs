using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.ModelsDto;
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
        private readonly IStatusService _statusService;
        private readonly IReadAndEditService<CategoryDto> _categoryService;
        private readonly IReadAndEditService<ImageDto> _imageService;

        public UserController(
            IRoleService roleService,
            IUserService userService,
            IItemService itemService,
            IStatusService statusService,
            IReadAndEditService<CategoryDto> categoryService,
            IReadAndEditService<ImageDto> imageService
            )
        {
            _roleService = roleService;
            _userService = userService;
            _itemService = itemService;
            _categoryService = categoryService;
            _statusService = statusService;
            _imageService = imageService;
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

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = category == null ?
                   allItems.Count() :
                   allItems.Where(p => p.Category.ToLower().Contains(category.ToLower())).Count()
            };

            IndexViewModel ivm = new IndexViewModel
            {
                PageInfo = pageInfo,
                ItemViewModels = itemPerPages.MapToListViewModels(),
                ActionName = "Index"
            };

            return View(ivm);            
        }

        [HttpGet]
        [ActionName("SearchItem")]
        public ActionResult GetSearchItem(string search, int page = 1)
        {
            IndexViewModel ivm = GetSearchItems(search, page);

            return View("Index", ivm);
        }

        [HttpPost]
        public ActionResult SearchItem(string search, int page = 1)
        {
            IndexViewModel ivm = GetSearchItems(search, page);

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
            var userViewModel = _userService.Find(User.Identity.Name).MapToViewModel();

            if(userViewModel == null)
            {
                return HttpNotFound();
            }

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
        private IndexViewModel GetSearchItems(string search, int page = 1)
        {
            const int pageSize = 10; 
            var allItems = _itemService.GetAll();
            var itemPerPages = allItems.
                Where(p => search == null ||
                      p.Category.ToLower().Contains(search.ToLower()) ||
                      p.Status.ToLower().Contains(search.ToLower()) ||
                      p.Title.ToLower().Contains(search.ToLower())).
                OrderBy(item => item.Title).
                Skip((page - 1) * pageSize).Take(pageSize);

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = search == null ?
                   allItems.Count() :
                   allItems.Where(
                       p => p.Category.ToLower().Contains(search.ToLower()) ||
                       p.Status.ToLower().Contains(search.ToLower()) ||
                       p.Title.ToLower().Contains(search.ToLower())).Count()
            };

            IndexViewModel ivm = new IndexViewModel
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