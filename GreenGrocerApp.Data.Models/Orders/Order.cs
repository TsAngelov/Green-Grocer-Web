using GreenGrocerApp.Data.Common.Models;
using GreenGrocerApp.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Data.Models.Orders
{
    public class Order : BaseDeletableModel
    {
        [Required(ErrorMessage = "User is required.")]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Range(0, double.MaxValue, ErrorMessage = "Total price cannot be negative.")]
        public decimal TotalPrice { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}
