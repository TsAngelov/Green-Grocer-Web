using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Web.ViewModels.Products
{
    public class ProductInputModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; } = null!;

        [StringLength(300, ErrorMessage = "Description cannot exceed 300 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 1000, ErrorMessage = "Price must be between 0.01 and 1000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be 0 or greater.")]
        public int QuantityInStock { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public Guid CategoryId { get; set; }

        public string? ImageUrl { get; set; }
    }
}
