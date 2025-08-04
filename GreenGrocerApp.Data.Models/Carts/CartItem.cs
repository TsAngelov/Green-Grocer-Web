using GreenGrocerApp.Data.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Data.Models.Carts
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Cart is required.")]
        public Guid CartId { get; set; }
        public Cart Cart { get; set; } = null!;

        [Required(ErrorMessage = "Product is required.")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}
