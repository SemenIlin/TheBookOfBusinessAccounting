using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using System.Web;
using System.Web.Mvc;
using TheBookBusinessAccounting.Infrastructure;
using TheBookBusinessAccounting.Extensions;
using TheBookBusinessAccounting.Models;
using System.Collections.Generic;
using System.Linq;
using Common.Exceptions;

namespace TheBookBusinessAccounting.Controllers
{
    [MyAuthorize(Roles = "Editor")]
    public class EditorController : Controller
    {
        private readonly IUserService _userService;
        private readonly IItemService _itemService;
        private readonly IStatusService _statusService;
        private readonly ICategoryService _categoryService;
        private readonly IImageService _imageService;

        private readonly Cache<ItemDto> _itemCache;
        private readonly Cache<UserDto> _userCache;
        private readonly Cache<ImageDto> _imageCache;

        public EditorController(
            IUserService userService,
            IItemService itemService,
            IStatusService statusService,
            ICategoryService categoryService,
            IImageService imageService)
        {
            _itemService = itemService;
            _statusService = statusService;
            _categoryService = categoryService;
            _imageService = imageService;
            _userService = userService;

            _itemCache = new Cache<ItemDto>();
            _userCache = new Cache<UserDto>();
            _imageCache = new Cache<ImageDto>();
        }

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
        public ActionResult AddItem(ItemViewModel itemViewModel, HttpPostedFileBase uploadImage)
        {
            try
            {
                itemViewModel.ImageViewModels = _itemService.GetCollectionImages(itemViewModel.Id).MapToCollectionViewModels();

                if (ModelState.IsValid)
                {
                    var itemDto = itemViewModel.MapToDtoModel();
                    _itemService.Add(itemDto, out int id);
                    _itemCache.Add(itemDto, id);

                    if (uploadImage != null)
                    {
                        itemViewModel.Screen = ImageConvert.ImageToByteArray(uploadImage);
                        itemViewModel.ScreenFormat = ImageConvert.GetImageExtension(uploadImage);

                        var imageVM = new ImageViewModel()
                        {
                            Screen = itemViewModel.Screen,
                            ScreenFormat = itemViewModel.ScreenFormat,
                            ItemId = id
                        };

                        var imageDto = imageVM.MapToDtoModel();
                        _imageService.Add(imageDto, out int imageId);
                    }

                    return RedirectToAction("Index", "User");
                }

                itemViewModel.Statuses = DictionaryOfStatuses();
                itemViewModel.Categories = DictionaryOfCategories();

                return View(itemViewModel);
            }
            catch(NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult EditItem(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            try
            {
                var itemDto = _itemCache.GetValue(id.Value);
                if(itemDto == null)
                {
                    itemDto = _itemService.Get(id.Value);
                    _itemCache.Add(itemDto, itemDto.Id);
                }

                if (itemDto == null)
                {
                    return View("NotFound");
                }

                var itemViewModel = itemDto.MapToViewModel();
                itemViewModel.Categories = DictionaryOfCategories();
                itemViewModel.Statuses = DictionaryOfStatuses();

                return View(itemViewModel);
            }
            catch(NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult EditItem(ItemViewModel itemViewModel, HttpPostedFileBase uploadImage)
        {
            try
            {
                itemViewModel.ImageViewModels = _itemService.GetCollectionImages(itemViewModel.Id).MapToCollectionViewModels();
                if (ModelState.IsValid)
                {
                    if (uploadImage != null)
                    {
                        itemViewModel.Screen = ImageConvert.ImageToByteArray(uploadImage);
                        itemViewModel.ScreenFormat = ImageConvert.GetImageExtension(uploadImage);

                        var imageVM = new ImageViewModel()
                        {
                            Screen = itemViewModel.Screen,
                            ScreenFormat = itemViewModel.ScreenFormat,
                            ItemId = itemViewModel.Id
                        };

                        _imageService.Add(imageVM.MapToDtoModel(), out int imageId);
                    }

                    var itemId = itemViewModel.Id;
                    _itemService.Update(itemViewModel.MapToDtoModel());
                    _itemCache.Update(_itemService.Get(itemId), itemId);

                    return RedirectToAction("Index", "User");
                }

                itemViewModel.Statuses = DictionaryOfStatuses();
                itemViewModel.Categories = DictionaryOfCategories();

                return View(itemViewModel);
            }
            catch(NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult DeleteItem(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            try
            {
                var itemDto = _itemCache.GetValue(id.Value);
                if(itemDto == null)
                {
                    itemDto = _itemService.Get(id.Value);
                    _itemCache.Add(itemDto, itemDto.Id);
                }
                
                if (itemDto == null)
                {
                    return View("NotFound");
                }

                return View(itemDto.MapToViewModel());
            }
            catch(NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ActionName("DeleteItem")]
        public ActionResult DeleteConfirmedItem(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            try
            {
                var itemDto = _itemCache.GetValue(id.Value);
                if(itemDto == null)
                {
                    itemDto = _itemService.Get(id.Value);
                }
                
                if (itemDto == null)
                {
                    return View("NotFound");
                }

                _itemService.Delete(id.Value);
                _itemCache.Delete(id.Value);

                return RedirectToAction("Index", "User");
            }
            catch(NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult DeleteImage(int? id, int? itemId)
        {
            if ((id == null) || (itemId == null))
            {
                return View("NotFound");
            }
            try
            {
                var imageDto = _imageCache.GetValue(id.Value);
                if(imageDto == null)
                {
                    imageDto = _imageService.Get(id.Value);
                }

                if (imageDto == null)
                {
                    return View("NotFound");
                }
                _imageService.Delete(id.Value);
                _imageCache.Delete(id.Value);

                var itemDto = _itemCache.GetValue(itemId.Value);
                if (itemDto == null)
                {
                    itemDto = _itemService.Get(itemId.Value);
                    _itemCache.Add(itemDto, itemDto.Id);
                }

                if (itemDto == null)
                {
                    return View("NotFound");
                }

                itemDto = _itemService.Get(itemId.Value);
                _itemCache.Update(itemDto, itemDto.Id);

                var itemViewModel = itemDto.MapToViewModel();
                itemViewModel.Categories = DictionaryOfCategories();
                itemViewModel.Statuses = DictionaryOfStatuses();

                return View("EditItem", itemViewModel);
            }
            catch(NotFoundException exception)
            {
                ViewBag.ErrorMessage = exception.Message;
                return View("Error");
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
            if (ModelState.IsValid)
            {
                var userDto = userViewModel.MapToDtoModel();
                _userService.Update(userDto);
                _userCache.Update(userDto, userViewModel.Id);

                return RedirectToAction("Index", "User");
            }

            return View(userViewModel);
        }

        private Dictionary<int,string> DictionaryOfStatuses()
        {
            return  _statusService.GetAll().ToDictionary(status => status.Id, status => status.Title);            
        }

        private Dictionary<int, string> DictionaryOfCategories()
        {
            return  _categoryService.GetAll().ToDictionary(category=>category.Id, category => category.Title);          
        }        
    }
}