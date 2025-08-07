using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GreenGrocerApp.Services.Core.Interfaces;
using GreenGrocerApp.Web.ViewModels.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenGrocerApp.Web.Controllers
{

    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public CartController(ICartService cartService, IProductService productService, IMapper mapper)
        {
            this.cartService = cartService;
            this.productService = productService;
            this.mapper = mapper;
        }

        private Guid GetUserId()
            => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<IActionResult> Index()
        {
            var cart = await cartService.GetCartByUserIdAsync(GetUserId());
            var viewModel = mapper.Map<CartViewModel>(cart);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Guid productId, int quantity)
        {
            if (quantity < 1)
            {
                TempData["Error"] = "Quantity must be at least 1.";
                return RedirectToAction("Details", "Products", new { id = productId });
            }

            var product = await productService.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            if (quantity > product.QuantityInStock)
            {
                TempData["Error"] = "Insufficient stock available.";
                return RedirectToAction("Details", "Products", new { id = productId });
            }

            try
            {
                await cartService.AddItemAsync(GetUserId(), productId, quantity);
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Details", "Products", new { id = productId });
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(Guid cartItemId)
        {
            var cart = await cartService.GetCartByUserIdAsync(GetUserId());
            var item = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (item != null)
            {
                await cartService.RemoveItemAsync(GetUserId(), item.ProductId);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Clear()
        {
            await cartService.ClearCartAsync(GetUserId());
            return RedirectToAction(nameof(Index));
        }
    }
}
