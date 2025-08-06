using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GreenGrocerApp.Services.Core.Interfaces;
using GreenGrocerApp.Web.ViewModels.Categories;
using GreenGrocerApp.Web.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GreenGrocerApp.Web.Areas.Admin.Controllers
{    
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public ProductsController(
            IProductService productService,
            ICategoryService categoryService,
            IMapper mapper)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllAsync();
            var viewModel = mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateCategories();
            return View(new ProductInputModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductInputModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategories();
                return View(model);
            }

            await productService.CreateAsync(model);
            TempData["Success"] = "Product created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var inputModel = mapper.Map<ProductInputModel>(product);
            ViewBag.ProductId = id;
            await PopulateCategories();
            return View(inputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductInputModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProductId = id;
                await PopulateCategories();
                return View(model);
            }

            await productService.UpdateAsync(id, model);
            TempData["Success"] = "Product updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await productService.DeleteAsync(id);
            TempData["Success"] = "Product deleted.";
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateCategories()
        {
            var categories = await categoryService.GetAllAsync();
            ViewBag.Categories = mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }
    }
}
