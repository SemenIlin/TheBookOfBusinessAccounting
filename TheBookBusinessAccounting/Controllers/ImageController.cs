using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using TheBookBusinessAccounting.Infrastructure;
using System.Web;
using System.Web.Mvc;
using TheBookBusinessAccounting.Models;
using TheBookBusinessAccounting.Extensions;

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
            GetListItems();
            return View();
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

            GetListItems();
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
        private SelectList GetListItems()
        {
            var items = new SelectList(_itemService.GetAll(), "Id", "Title");
            ViewBag.Items = items;

            return ViewBag.Items;
        }
    }
}