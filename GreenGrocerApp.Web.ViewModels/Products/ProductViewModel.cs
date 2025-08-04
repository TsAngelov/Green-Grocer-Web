using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Web.ViewModels.Products
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Product Name")]
        public string Name { get; set; } = null!;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "In Stock")]
        public int QuantityInStock { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}
