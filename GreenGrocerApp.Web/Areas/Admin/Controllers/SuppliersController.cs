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
            var model = mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);
            return View(model);
        }

        public IActionResult Create()
        {
            return View(new SupplierInputModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            await supplierService.CreateAsync(inputModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var supplier = await supplierService.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            var model = mapper.Map<SupplierInputModel>(supplier);
            ViewData["SupplierId"] = id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["SupplierId"] = id;
                return View(inputModel);
            }

            await supplierService.UpdateAsync(id, inputModel);
            return RedirectToAction(nameof(Index));
        }
    }
}