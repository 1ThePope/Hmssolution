using Microsoft.AspNetCore.Identity;

namespace Hmssolution
{
    public static class SeedData
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider services, IConfiguration config)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            var roles = new[] { "Admin", "Student", "LandLord" };
            foreach (var r in roles)
                if (!await roleManager.RoleExistsAsync(r))
                    await roleManager.CreateAsync(new IdentityRole(r));

            // seed admin user if not exists
            var adminEmail = config["Admin:Email"] ?? "admin@example.com";
            var adminPassword = config["Admin:Password"] ?? "Admin123!";
            var existing = await userManager.FindByEmailAsync(adminEmail);
            if (existing == null)
            {
                var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                var res = await userManager.CreateAsync(adminUser, adminPassword);
                if (res.Succeeded)
                    await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
