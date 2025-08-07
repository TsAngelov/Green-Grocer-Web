using AutoMapper;
using GreenGrocerApp.Services.Core.Interfaces;
using GreenGrocerApp.Web.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

namespace GreenGrocerApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public HomeController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllAsync();
            var viewModel = mapper.Map<IEnumerable<CategoryViewModel>>(categories).Take(3);
            return View(viewModel);
        }
    }
}