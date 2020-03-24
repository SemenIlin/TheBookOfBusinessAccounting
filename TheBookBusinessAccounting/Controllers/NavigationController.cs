using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheBookBusinessAccounting.Controllers
{
    public class NavigationController: Controller
    {
        private readonly IReadAndEditService<CategoryDto> _categoryReadAndEditService;

        public NavigationController(IReadAndEditService<CategoryDto> categoryReadAndEditService)
        {
            _categoryReadAndEditService = categoryReadAndEditService;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            var categories = _categoryReadAndEditService.GetAll()
                .Select(c => c.Title)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(categories);
        }
    }
}