using System;

namespace GreenGrocerApp.Web.ViewModels.Products
{
    public class SupplierViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ContactInfo { get; set; }
    }
}