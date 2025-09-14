using Microsoft.AspNetCore.Identity;
using PetAdopt.Identity;

namespace PetAdopt.Web.Infrastructure
{
    public static class Seed
    {
        public static async Task EnsureAdminAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            const string adminRole = "Admin";
            if (!await roleMgr.RoleExistsAsync(adminRole))
                _ = await roleMgr.CreateAsync(new IdentityRole(adminRole));

            var email = "admin@petadopt.local";
            var user = await userMgr.FindByEmailAsync(email);
            if (user is null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                // For real projects, read from user-secrets:
                // var pwd = scope.ServiceProvider.GetRequiredService<IConfiguration>()["Seed:AdminPassword"];
                var result = await userMgr.CreateAsync(user, "Admin!234"); // dev-only password
                if (result.Succeeded)
                {
                    await userMgr.AddToRoleAsync(user, adminRole);
                }
            }
        }
    }
}
