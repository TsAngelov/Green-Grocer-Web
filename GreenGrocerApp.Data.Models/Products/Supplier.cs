using GreenGrocerApp.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Data.Models.Products
{
    public class Supplier : BaseDeletableModel
    {
        [Required(ErrorMessage = "Supplier name is required.")]
        [StringLength(100, ErrorMessage = "Supplier name cannot be longer than {1} characters.")]
        public string Name { get; set; } = null!;

        [StringLength(200, ErrorMessage = "Contact information cannot be longer than {1} characters.")]
        public string? ContactInfo { get; set; }

        public ICollection<Supply> Supplies { get; set; } = new HashSet<Supply>();
    }
}
