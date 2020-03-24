using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheBookBusinessAccounting.Infrastructure;
using TheBookBusinessAccounting.Mappers;
using TheBookBusinessAccounting.Models;

namespace TheBookBusinessAccounting.Controllers
{
    public class EditorController : Controller
    {
        private readonly IService<ItemDto> _itemService;
        private readonly IReadService<StatusDto> _statusService;
        private readonly IReadAndEditService<CategoryDto> _categoryService;
        private readonly IReadAndEditService<ImageDto> _imageService;

        public EditorController(
            IService<ItemDto> itemService,
            IReadService<StatusDto> statusService,
            IReadAndEditService<CategoryDto> categoryService,
            IReadAndEditService<ImageDto> imageService)
        {
            _itemService = itemService;
            _statusService = statusService;
            _categoryService = categoryService;
            _imageService = imageService;
        }

        [HttpGet]
        public ActionResult AddItem()
        {
            SelectListStatuses();
            SelectListCategories();
            return View();
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

            SelectListStatuses();
            SelectListCategories();

            return View(itemViewModel);
        }


        [HttpGet]
        public ActionResult EditItem(int? id)
        {
            var itemAndActionResult = GetActionResultAndItem(id);

            if (itemAndActionResult.ItemModel == null)
            {
                return itemAndActionResult.ActionResult;
            }

            SelectListStatuses();
            SelectListCategories();

            return View(itemAndActionResult.ItemModel);
        }

        [HttpPost]
        public ActionResult EditItem(ItemViewModel itemViewModel, HttpPostedFileBase uploadImage)
        {
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

            SelectListStatuses();
            SelectListCategories();

            return View(itemViewModel);

        }

        [HttpGet]
        public ActionResult DeleteItem(int? id)
        {
            var itemAndActionResult = GetActionResultAndItem(id);

            return itemAndActionResult.ActionResult ?? View(itemAndActionResult.ItemModel);
        }

        [HttpPost]
        [ActionName("DeleteItem")]
        public ActionResult DeleteConfirmedItem(int? id)
        {
            var itemAndActionResult = GetActionResultAndItem(id);

            if (itemAndActionResult.ItemModel == null)
            {
                return itemAndActionResult.ActionResult;
            }

            _itemService.Delete(id.Value);

            return RedirectToAction("Index");
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

    }
}