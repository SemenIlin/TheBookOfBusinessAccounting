using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using System.Linq;
using System.Web.Mvc;
using TheBookBusinessAccounting.Extensions;
using TheBookBusinessAccounting.Infrastructure;
using TheBookBusinessAccounting.Models;
using TheBookBusinessAccounting.Models.Pagination;

namespace TheBookBusinessAccounting.Controllers
{
    [MyAuthorizeAttribute(new string[]{"User", "Editor", "Administrator"})]
    public class UserController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IReadService<StatusDto> _statusService;
        private readonly IReadAndEditService<CategoryDto> _categoryService;
        private readonly IReadAndEditService<ImageDto> _imageService;

        public UserController(
            IItemService itemService,
            IReadService<StatusDto> statusService,
            IReadAndEditService<CategoryDto> categoryService,
            IReadAndEditService<ImageDto> imageService
            )
        {
            _itemService = itemService;
            _categoryService = categoryService;
            _statusService = statusService;
            _imageService = imageService;
        }

        [HttpGet]
        public ActionResult Index(string category, int page = 1)
        {
            const int pageSize = 10; // количество объектов на страницу
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
            var itemAndAction = GetActionResultAndItem(id);

            return itemAndAction.ActionResult ?? View(itemAndAction.ItemModel);

        }

        [NonAction]
        private SelectList SelectListStatuses()
        {
            var statuses = new SelectList(_statusService.GetAll(), "Id", "Title");
            ViewBag.Statuses = statuses;
            return ViewBag.Statuses;
        }

        [NonAction]
        private SelectList SelectListCategories()
        {
            var categories = new SelectList(_categoryService.GetAll(), "Id", "Title");
            ViewBag.Categories = categories;

            return ViewBag.Categories;
        }

        [NonAction]
        private (ActionResult ActionResult, ItemViewModel ItemModel) GetActionResultAndItem(int? id)
        {
            if (id == null)
            {
                return (HttpNotFound(), null);
            }

            var item = _itemService.Get(id.Value);
            if (item == null)
            {
                return (HttpNotFound(), null);
            }

            return (null, item.MapToViewModel());
        }

        [NonAction]
        private IndexViewModel GetSearchItems(string search, int page = 1)
        {
            const int pageSize = 10; // количество объектов на страницу
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