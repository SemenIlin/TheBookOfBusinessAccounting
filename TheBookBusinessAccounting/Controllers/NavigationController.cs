using BLLTheBookOfBusinessAccounting.Interfaces;
using BLLTheBookOfBusinessAccounting.ModelsDto;
using System.Linq;
using System.Web.Mvc;
using TheBookBusinessAccounting.Infrastructure;

namespace TheBookBusinessAccounting.Controllers
{
    [MyAuthorize(Roles ="User")]
    public class NavigationController: Controller
    {
        private readonly ICategoryService _categoryService;

        public NavigationController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            var categories = _categoryService.GetAll()
                .Select(c => c.Title)
                .Distinct()
                .OrderBy(x => x);

            return PartialView("Menu", categories);
        }
    }
}