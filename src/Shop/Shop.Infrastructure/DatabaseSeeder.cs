using Common.Application.SecurityUtil;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Enums;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Services;
using Shop.Infrastructure.Persistent.Ef;

namespace Shop.Infrastructure;

public class DatabaseSeeder
{
    private readonly ShopContext _context;
    private readonly IUserDomainService _userDomainService;

    public DatabaseSeeder(IUserDomainService userDomainService, ShopContext context)
    {
        _userDomainService = userDomainService;
        _context = context;
    }

    public void Seed()
    {

        // Apply migrations
        var retries = 10;
        while (retries > 0)
        {
            try
            {
                _context.Database.Migrate();
                break;
            }
            catch (SqlException)
            {
                retries--;
                Thread.Sleep(5000); // wait 5s
                if (retries == 0) throw;
            }
        }
        
        var adminRole = _context.Roles.FirstOrDefault(r => r.Title == "admin");
        if (!_context.Roles.Any(r => r.Title == "admin"))
        {
            adminRole = new Role("admin", [
                RolePermission.CreateRolePermission(Permission.PanelAdmin),
                RolePermission.CreateRolePermission(Permission.EditProfile),
                RolePermission.CreateRolePermission(Permission.ChangePassword),
                RolePermission.CreateRolePermission(Permission.CrudBanner),
                RolePermission.CreateRolePermission(Permission.CrudSlider),
                RolePermission.CreateRolePermission(Permission.CurdUser),
                RolePermission.CreateRolePermission(Permission.CrudProduct),
                RolePermission.CreateRolePermission(Permission.SellerManagement),
                RolePermission.CreateRolePermission(Permission.OrderManagement),
                RolePermission.CreateRolePermission(Permission.RoleManagement),
                RolePermission.CreateRolePermission(Permission.CommentManagement),
                RolePermission.CreateRolePermission(Permission.CategoryManagement),
                RolePermission.CreateRolePermission(Permission.AddInventory),
                RolePermission.CreateRolePermission(Permission.EditInventory),
                RolePermission.CreateRolePermission(Permission.ChangeStatusInventory),
                RolePermission.CreateRolePermission(Permission.UserManagement),
                RolePermission.CreateRolePermission(Permission.SellerPanel)
            ]);
            _context.Roles.Add(adminRole);
            _context.SaveChanges();
        }

        // Seed admin user
        var adminUser = _context.Users
            .Include(x => x.UserRoles)
            .FirstOrDefault(u => u.Name == "admin");
        if (!_context.Users.Any(u => u.Name == "admin"))
        {
            adminUser = new User(
                name: "admin",
                family: "admin",
                phoneNumber: "09121234567",
                email: "admin@admin.com",
                password: Sha256Hasher.Hash("123456"),
                gender: Gender.None,
                _userDomainService
            );
            _context.Users.Add(adminUser);
            _context.SaveChanges();
            
            _context.Entry(adminUser).Collection(u => u.UserRoles).Load();
        }

        if (!adminUser.UserRoles.Any(x => x.RoleId == adminRole.Id))
        {
            var userRole = new UserRole(adminRole.Id, adminUser.Id); // RoleId only
            _context.Add(userRole); // EF will set UserId automatically
            _context.SaveChanges();
        }

    }
}