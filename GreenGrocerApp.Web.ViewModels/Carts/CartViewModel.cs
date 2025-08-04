using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Web.ViewModels.Carts
{
    public class CartViewModel
    {
        public Guid UserId { get; set; }

        public List<CartItemViewModel> Items { get; set; } = new();

        public decimal TotalPrice => Items.Sum(i => i.Total);
    }
}
