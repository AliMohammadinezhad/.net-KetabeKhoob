using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CommentAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.RoleAgg.Repository;
using Shop.Domain.SellerAgg.Repository;
using Shop.Domain.SiteEntities.Repository;
using Shop.Domain.UserAgg.Repository;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Infrastructure.Persistent.Ef.CategoryAgg;
using Shop.Infrastructure.Persistent.Ef.CommentAgg;
using Shop.Infrastructure.Persistent.Ef.OrderAgg;
using Shop.Infrastructure.Persistent.Ef.ProductAgg;
using Shop.Infrastructure.Persistent.Ef.RoleAgg;
using Shop.Infrastructure.Persistent.Ef.SellerAgg;
using Shop.Infrastructure.Persistent.Ef.SiteEntities.Banner;
using Shop.Infrastructure.Persistent.Ef.SiteEntities.Slider;
using Shop.Infrastructure.Persistent.Ef.UserAgg;

namespace Shop.Infrastructure;

public static class InfrastructureBootstrapper
{
    public static void Init(this IServiceCollection service, string connectionString)
    {
        service.AddTransient<ICategoryRepository, CategoryRepository>();
        service.AddTransient<ICommentRepository, CommentRepository>();
        service.AddTransient<IOrderRepository, OrderRepository>();
        service.AddTransient<IProductRepository, ProductRepository>();
        service.AddTransient<IRoleRepository, RoleRepository>();
        service.AddTransient<ISellerRepository, SellerRepository>();
        service.AddTransient<IBannerRepository, BannerRepository>();
        service.AddTransient<ISliderRepository, SliderRepository>();
        service.AddTransient<IUserRepository, UserRepository>();
        service.AddTransient<DatabaseSeeder>();

        service.AddTransient(_ => new DapperContext(connectionString));
        service.AddDbContext<ShopContext>(option => option.UseSqlServer(connectionString));

    }
}