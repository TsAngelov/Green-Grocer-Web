using AutoMapper;
using GreenGrocerApp.Services.Core.Interfaces;
using GreenGrocerApp.Web.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace GreenGrocerApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllAsync();
            var viewModel = mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var product = await productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var viewModel = mapper.Map<ProductViewModel>(product);
            return View(viewModel);
        }

        public async Task<IActionResult> ByCategory(Guid categoryId)
        {
            var products = await productService.GetByCategoryAsync(categoryId);
            var viewModel = mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View(viewModel);
        }
    }
}
