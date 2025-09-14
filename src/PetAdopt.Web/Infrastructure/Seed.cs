using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PetAdopt.Data.Identity;

namespace PetAdopt.Web.Infrastructure;

public static class Seed
{
    public static async Task EnsureAdminAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        const string adminRole = "Admin";
        const string email = "admin@petadopt.local";
        const string password = "Admin!234"; // dev-only

        if (!await roleMgr.RoleExistsAsync(adminRole))
            await roleMgr.CreateAsync(new IdentityRole(adminRole));

        var user = await userMgr.FindByEmailAsync(email);
        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                FullName = "Site Admin"
            };
            await userMgr.CreateAsync(user, password);
            await userMgr.AddToRoleAsync(user, adminRole);
        }
    }
}
