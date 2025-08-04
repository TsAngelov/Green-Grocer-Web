using GreenGrocerApp.Data.Models.Carts;
using GreenGrocerApp.Data.Models.Orders;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GreenGrocerApp.Data.Models.Users
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name cannot be longer than {1} characters.")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Delivery address is required.")]
        [StringLength(200, ErrorMessage = "Delivery address cannot be longer than {1} characters.")]
        public string DeliveryAddress { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public ICollection<Cart> Carts { get; set; } = new HashSet<Cart>();
    }
}
