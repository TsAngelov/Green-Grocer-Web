using AutoMapper;
using GreenGrocerApp.Services.Core.Interfaces;
using GreenGrocerApp.Web.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

namespace GreenGrocerApp.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllAsync();
            var viewModel = mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var category = await categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var viewModel = mapper.Map<CategoryWithProductsViewModel>(category);
            return View(viewModel);
        }
    }
}
