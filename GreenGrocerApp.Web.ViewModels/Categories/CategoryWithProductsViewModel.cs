using GreenGrocerApp.Web.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Web.ViewModels.Categories
{
    public class CategoryWithProductsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
            = new List<ProductViewModel>();
    }
}
