using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Web.ViewModels.Orders
{
    public class OrderHistoryViewModel
    {
        public List<OrderViewModel> Orders { get; set; } = new();
    }
}
