using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100, ErrorMessage = "Full name cannot be longer than 100 characters.")]
        public string FullName { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Delivery address cannot be longer than 200 characters.")]
        public string DeliveryAddress { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
