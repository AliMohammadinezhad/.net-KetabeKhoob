using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application._Utilities;
using Shop.Application.Categories;
using Shop.Application.Orders;
using Shop.Application.Products;
using Shop.Application.Sellers;
using Shop.Application.Users;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.OrderAgg.Services;
using Shop.Domain.ProductAgg.Services;
using Shop.Domain.SellerAgg.Services;
using Shop.Domain.UserAgg.Services;
using Shop.Infrastructure;
using Shop.Query.Categories.GetById;

namespace Shop.Config;

public static class ShopBootstrapper
{
    public static void RegisterShopDependency(this IServiceCollection services, string connectionString)
    {
        // Infrastructure Layer
        InfrastructureBootstrapper.Init(services, connectionString);
        
        // MediatR Application Layer
        services.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssembly(typeof(Directories).Assembly));
        
        // MediatR Query Layer
        services.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssembly(typeof(GetCategoryByIdQuery).Assembly));

        // Application Layer
        services.AddTransient<ICategoryDomainService, CategoryDomainService>();
        services.AddTransient<IProductDomainService, ProductDomainService>();
        services.AddTransient<IOrderDomainService, OrderDomainService>();
        services.AddTransient<IProductDomainService, ProductDomainService>();
        services.AddTransient<ISellerDomainService, SellerDomainService>();
        services.AddTransient<IUserDomainService, UserDomainService>();

        // Fluent Validation
        services.AddValidatorsFromAssembly(typeof(Directories).Assembly);

    }
}