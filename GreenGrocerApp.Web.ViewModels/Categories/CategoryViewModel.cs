using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Web.ViewModels.Categories
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Category Name")]
        public string Name { get; set; } = null!;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Products Count")]
        public int ProductsCount { get; set; }
    }
}
