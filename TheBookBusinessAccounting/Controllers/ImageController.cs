using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheBookBusinessAccounting.Models;

namespace TheBookBusinessAccounting.Controllers
{
    public class ImageController : Controller
    {
        private readonly IReadAndEditService<ImageDto> _imageService;

        public ImageController(IReadAndEditService<ImageDto> imageService)
        {
            _imageService = imageService;
        }

        public ActionResult AddImage()
        {
            return View();
        }

        public ActionResult AddImage(ImageViewModel imageViewModel)
        {
            if(ModelState.IsValid)
            {
                return View();
            }
            return View();
        }

    }
}