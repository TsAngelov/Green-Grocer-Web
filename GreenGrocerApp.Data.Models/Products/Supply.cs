using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Data.Models.Products
{
    public class Supply
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product is required.")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Required(ErrorMessage = "Supplier is required.")]
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Quantity supplied must be at least 1.")]
        public int QuantitySupplied { get; set; }

        public DateTime SupplyDate { get; set; } = DateTime.UtcNow;
    }
}
