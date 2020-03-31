using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using TheBookBusinessAccounting.Infrastructure;
using System.Web;
using System.Web.Mvc;
using TheBookBusinessAccounting.Models;
using TheBookBusinessAccounting.Extensions;
using System.Collections.Generic;

namespace TheBookBusinessAccounting.Controllers
{
    public class ImageController : Controller
    {
        private readonly IReadAndEditService<ImageDto> _imageService;
        private readonly IItemService _itemService;

        public ImageController(
            IReadAndEditService<ImageDto> imageService,
            IItemService itemService
            )
        {
            _imageService = imageService;
            _itemService = itemService;
        }

        public ActionResult Index() => View();
    

        [HttpGet]
        public ActionResult AddImage()
        {
            var imageViewModel = new ImageViewModel()
            {
                Items = GetListItems()
            };
            
            return View(imageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddImage(ImageViewModel imageViewModel, HttpPostedFileBase uploadImage)
        {
            if(ModelState.IsValid && uploadImage != null)
            {
                imageViewModel.Screen = ImageConvert.ImageToByteArray(uploadImage);
                imageViewModel.ScreenFormat = ImageConvert.GetImageExtension(uploadImage);            

                _imageService.Add(imageViewModel.MapToDtoModel());

                return RedirectToAction("Index");
            }

            imageViewModel.Items = GetListItems();
            return View(imageViewModel);
        }

        [HttpGet]
        public ActionResult GetImage(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var image = _imageService.Get(id.Value);
            if (image == null)
            {
                return HttpNotFound();
            }

            return View(image.MapToViewModel());
        }

        [NonAction]
        private Dictionary<int, string> GetListItems()
        {
            var items = _itemService.GetAll();
            var dictionaryOfItems = new Dictionary<int, string>();
            foreach (var item in items)
            {
                dictionaryOfItems.Add(item.Id, item.Title);
            }

            return dictionaryOfItems;
        }
    }
}