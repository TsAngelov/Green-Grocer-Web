using GreenGrocerApp.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Data.Models.Products
{
    public class Category : BaseDeletableModel
    {
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, ErrorMessage = "Category name cannot be longer than {1} characters.")]
        public string Name { get; set; } = null!;

        [StringLength(200, ErrorMessage = "Description cannot be longer than {1} characters.")]
        public string? Description { get; set; }

        [Url]
        [StringLength(300)]
        public string? ImageUrl { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
