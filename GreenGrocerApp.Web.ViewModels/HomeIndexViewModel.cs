using System.Collections.Generic;
using GreenGrocerApp.Web.ViewModels.Categories;
using GreenGrocerApp.Web.ViewModels.Products;

namespace GreenGrocerApp.Web.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        public IEnumerable<ProductViewModel> FeaturedProducts { get; set; } = new List<ProductViewModel>();
    }
}