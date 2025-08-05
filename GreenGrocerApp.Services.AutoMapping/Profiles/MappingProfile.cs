using AutoMapper;
using GreenGrocerApp.Data.Models.Carts;
using GreenGrocerApp.Data.Models.Orders;
using GreenGrocerApp.Data.Models.Products;
using GreenGrocerApp.Web.ViewModels.Carts;
using GreenGrocerApp.Web.ViewModels.Categories;
using GreenGrocerApp.Web.ViewModels.Orders;
using GreenGrocerApp.Web.ViewModels.Products;

namespace GreenGrocerApp.Services.AutoMapping.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category
            CreateMap<Category, CategoryViewModel>()
                .ForMember(dest => dest.ProductsCount, opt => opt.MapFrom(src => src.Products.Count(p => !p.IsDeleted)));

            CreateMap<Category, CategoryWithProductsViewModel>();
            CreateMap<CategoryInputModel, Category>();
            CreateMap<Category, CategoryInputModel>();

            // Product
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<ProductInputModel, Product>();
            CreateMap<Product, ProductInputModel>();

            CreateMap<SupplierInputModel, Supplier>();
            CreateMap<Supplier, SupplierInputModel>();

            // Cart
            CreateMap<CartItem, CartItemViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // Needed for Remove form
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.Price));

            CreateMap<Cart, CartViewModel>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartItems));

            // Order
            CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));
        }
    }
}
