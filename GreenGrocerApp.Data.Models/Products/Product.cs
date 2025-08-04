using GreenGrocerApp.Data.Common.Models;
using GreenGrocerApp.Data.Models.Carts;
using GreenGrocerApp.Data.Models.Orders;
using System.ComponentModel.DataAnnotations;

namespace GreenGrocerApp.Data.Models.Products
{
    public class Product : BaseDeletableModel
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot be longer than {1} characters.")]
        public string Name { get; set; } = null!;

        [StringLength(300, ErrorMessage = "Description cannot be longer than {1} characters.")]
        public string? Description { get; set; }

        [Range(0.01, 10000, ErrorMessage = "Price must be between {1} and {2}.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity in stock must be zero or more.")]
        public int QuantityInStock { get; set; }

        [Url]
        [StringLength(300)]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;


        public ICollection<Supply> Supplies { get; set; } = new HashSet<Supply>();
        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}
