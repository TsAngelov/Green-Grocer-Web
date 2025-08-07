using AutoMapper;
using GreenGrocerApp.Services.Core.Interfaces;
using GreenGrocerApp.Web.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenGrocerApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public ProductsController(IProductService productService, ICategoryService categoryService, IMapper mapper)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllAsync();
            var model = mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateCategories();
            return View(new ProductInputModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategories();
                return View(inputModel);
            }

            await productService.CreateAsync(inputModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var model = mapper.Map<ProductInputModel>(product);
            ViewData["ProductId"] = id;
            await PopulateCategories();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = id;
                await PopulateCategories();
                return View(inputModel);
            }

            await productService.UpdateAsync(id, inputModel);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateCategories()
        {
            var categories = await categoryService.GetAllAsync();
            ViewBag.Categories = categories;
        }
    }
}