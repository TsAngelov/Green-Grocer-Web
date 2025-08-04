using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Web.ViewModels.Carts
{
    public class CartItemViewModel
    {
        public Guid ProductId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = null!;

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Total")]
        public decimal Total => UnitPrice * Quantity;
    }
}
