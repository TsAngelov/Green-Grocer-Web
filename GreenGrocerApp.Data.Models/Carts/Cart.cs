using GreenGrocerApp.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Data.Models.Carts
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User is required.")]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }
}
