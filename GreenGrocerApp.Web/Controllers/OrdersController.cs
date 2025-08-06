using AutoMapper;
using GreenGrocerApp.Services.Core.Interfaces;
using GreenGrocerApp.Web.ViewModels.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GreenGrocerApp.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;
        private readonly ICartService cartService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService,
                                ICartService cartService,
                                IMapper mapper)
        {
            this.orderService = orderService;
            this.cartService = cartService;
            this.mapper = mapper;
        }

        private Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<IActionResult> MyOrders()
        {
            var orders = await orderService.GetOrdersByUserIdAsync(GetUserId());
            var viewModel = mapper.Map<IEnumerable<OrderViewModel>>(orders);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var order = await orderService.GetOrderByIdAsync(id);
            if (order == null || order.ApplicationUserId != GetUserId())
            {
                return NotFound();
            }

            var viewModel = mapper.Map<OrderViewModel>(order);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            var userId = GetUserId();
            await orderService.CreateOrderAsync(userId);
            await cartService.ClearCartAsync(userId);

            TempData["Success"] = "Order placed successfully.";
            return RedirectToAction(nameof(MyOrders));
        }
    }
}
