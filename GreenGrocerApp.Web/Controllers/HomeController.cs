using Microsoft.AspNetCore.Mvc;

namespace GreenGrocerApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public Task<IActionResult> Index() => Task.FromResult<IActionResult>(View());
    }
}
