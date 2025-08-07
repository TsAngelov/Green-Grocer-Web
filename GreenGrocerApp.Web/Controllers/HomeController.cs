using AutoMapper;
using GreenGrocerApp.Services.Core.Interfaces;
using GreenGrocerApp.Web.ViewModels.Categories;
using GreenGrocerApp.Web.ViewModels.Home;
using GreenGrocerApp.Web.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace GreenGrocerApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public HomeController(ICategoryService categoryService, IProductService productService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.productService = productService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllAsync();
            var products = await productService.GetAllAsync();

            var viewModel = new HomeIndexViewModel
            {
                Categories = mapper.Map<IEnumerable<CategoryViewModel>>(categories).Take(3),
                FeaturedProducts = mapper.Map<IEnumerable<ProductViewModel>>(products).Take(3)
            };

            return View(viewModel);
        }
    }
}