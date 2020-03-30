using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using System.Web;
using System.Web.Mvc;
using TheBookBusinessAccounting.Infrastructure;
using TheBookBusinessAccounting.Extensions;
using TheBookBusinessAccounting.Models;
using System.Collections.Generic;
using System.Linq;
using TheBookBusinessAccounting.Models.Pagination;

namespace TheBookBusinessAccounting.Controllers
{
    //[Authorize(Roles = "Editor, Administrator")]
    public class EditorController : Controller
    {
        private readonly IUserService _userService;
        private readonly IItemService _itemService;
        private readonly IStatusService _statusService;
        private readonly IReadAndEditService<CategoryDto> _categoryService;
        private readonly IReadAndEditService<ImageDto> _imageService;

        public EditorController(
            IUserService userService,
            IItemService itemService,
            IStatusService statusService,
            IReadAndEditService<CategoryDto> categoryService,
            IReadAndEditService<ImageDto> imageService)
        {
            _itemService = itemService;
            _statusService = statusService;
            _categoryService = categoryService;
            _imageService = imageService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "User");
        }

        //[HttpGet]
        //[ActionName("SearchItem")]
        //public ActionResult GetSearchItem(string search, int page = 1)
        //{
        //    IndexViewModel ivm = GetSearchItems(search, page);

        //    return View("Index", ivm);
        //}

        //[HttpPost]
        //public ActionResult SearchItem(string search, int page = 1)
        //{
        //    IndexViewModel ivm = GetSearchItems(search, page);

        //    return View("Index", ivm);
        //}

        [HttpGet]
        public ActionResult AddItem()
        {
            var itemViewModel = new ItemViewModel()
            {
                Statuses = DictionaryOfStatuses(),
                Categories = DictionaryOfCategories()
            };            
           
            return View(itemViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddItem(ItemViewModel itemViewModel)
        {
            if (ModelState.IsValid)
            {
                _itemService.Add(itemViewModel.MapToDtoModel());

                return RedirectToAction("Index");
            }

            itemViewModel.Statuses = DictionaryOfStatuses();
            itemViewModel.Categories = DictionaryOfCategories();

            return View(itemViewModel);
        }

        [HttpGet]
        public ActionResult EditItem(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var itemDto = _itemService.Get(id.Value);
            if (itemDto == null)
            {
                return HttpNotFound();
            }

            var itemViewModel = itemDto.MapToViewModel();
            itemViewModel.Categories = DictionaryOfCategories();
            itemViewModel.Statuses = DictionaryOfStatuses();            

            return View(itemViewModel);
        }

        [HttpPost]
        public ActionResult EditItem(ItemViewModel itemViewModel, HttpPostedFileBase uploadImage)
        {
            itemViewModel.ImageViewModels = _itemService.GetCollectionImages(itemViewModel.Id).MapToCollectionViewModels() ;
            if (ModelState.IsValid)
            {                
                if(uploadImage != null)
                {
                    itemViewModel.Screen = ImageConvert.ImageToByteArray(uploadImage);
                    itemViewModel.ScreenFormat = ImageConvert.GetImageExtension(uploadImage);

                    var imageVM = new ImageViewModel()
                    {
                        Screen = itemViewModel.Screen,
                        ScreenFormat = itemViewModel.ScreenFormat,
                        ItemId = itemViewModel.Id
                    };
                    _imageService.Add(imageVM.MapToDtoModel());
                }
                
                _itemService.Update(itemViewModel.MapToDtoModel());

                return RedirectToAction("Index");
            }

            itemViewModel.Statuses = DictionaryOfStatuses();
            itemViewModel.Categories = DictionaryOfCategories();

            return View(itemViewModel);
        }

        [HttpGet]
        public ActionResult DeleteItem(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var itemDto = _itemService.Get(id.Value);
            if (itemDto == null)
            {
                return HttpNotFound();
            }

            return View(itemDto.MapToViewModel());
        }

        [HttpPost]
        [ActionName("DeleteItem")]
        public ActionResult DeleteConfirmedItem(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var itemDto = _itemService.Get(id.Value);
            if (itemDto == null)
            {
                return HttpNotFound();
            }

            _itemService.Delete(id.Value);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteImage(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var imageDto = _imageService.Get(id.Value);
            if (imageDto == null)
            {
                return HttpNotFound();
            }

            _imageService.Delete(id.Value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditUser()
        {
            var userViewModel = _userService.Find(User.Identity.Name).MapToViewModel();

            if (userViewModel == null)
            {
                return HttpNotFound();
            }

            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult EditUser(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                _userService.Update(userViewModel.MapToDtoModel());

                return RedirectToAction("Index");
            }

            return View(userViewModel);
        }

        [NonAction]
        private Dictionary<int,string> DictionaryOfStatuses()
        {
            var statuses = _statusService.GetAll();
            var dictionaryOfStatuses = new Dictionary<int, string>();
            foreach(var status in statuses)
            {
                dictionaryOfStatuses.Add(status.Id, status.Title);
            }

            return dictionaryOfStatuses;
        }

        [NonAction]
        private Dictionary<int, string> DictionaryOfCategories()
        {
            var categories = _categoryService.GetAll();
            var dictionaryOfCategories = new Dictionary<int, string>();
            foreach (var category in categories)
            {
                dictionaryOfCategories.Add(category.Id, category.Title);
            }

            return dictionaryOfCategories;
        }        
    }
}