using GreenGrocerApp.Data.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Data.Models.Orders
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Order is required.")]
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        [Required(ErrorMessage = "Product is required.")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than zero.")]
        public decimal UnitPrice { get; set; }
    }
}
