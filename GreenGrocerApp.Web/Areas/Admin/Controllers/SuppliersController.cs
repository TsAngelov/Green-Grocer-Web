using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GreenGrocerApp.Services.Core.Interfaces;
using GreenGrocerApp.Web.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenGrocerApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SuppliersController : Controller
    {
        private readonly ISupplierService supplierService;
        private readonly IMapper mapper;

        public SuppliersController(ISupplierService supplierService, IMapper mapper)
        {
            this.supplierService = supplierService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var suppliers = await supplierService.GetAllAsync();
            var viewModel = mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);
            return View(viewModel);
        }

        public IActionResult Create() => View(new SupplierInputModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await supplierService.CreateAsync(model);
            TempData["Success"] = "Supplier created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var supplier = await supplierService.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            var inputModel = mapper.Map<SupplierInputModel>(supplier);
            return View(inputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await supplierService.UpdateAsync(id, model);
            TempData["Success"] = "Supplier updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await supplierService.DeleteAsync(id);
            TempData["Success"] = "Supplier deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}