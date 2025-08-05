using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Web.ViewModels.Categories
{
    public class CategoryInputModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between {2} and {1} characters.")]
        public string Name { get; set; } = null!;

        [StringLength(200, ErrorMessage = "Description cannot exceed {1} characters.")]
        public string? Description { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        [StringLength(300, ErrorMessage = "Image URL cannot exceed {1} characters.")]
        public string? ImageUrl { get; set; }
    }   
}
