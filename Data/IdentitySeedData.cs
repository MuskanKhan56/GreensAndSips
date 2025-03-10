using Microsoft.AspNetCore.Identity;

namespace GreensAndSips.Data
{
    public class IdentitySeedData
    {
        public static async Task Initialize(GreensAndSipsContext context,
            UserManager<IdentityUser>userManager,
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();
            string adminRole = "Admin";
            string memberRole = "Member";
            string password4all = "P@55word";
            if(await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }
            if (await roleManager.FindByNameAsync(memberRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }
            if(await userManager.FindByNameAsync("admin@usm.ac.im") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin@ucm.ac.im",
                    Email = "admin@ucm.ac.im",
                    PhoneNumber = "06124 648200"
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password4all);
                    await userManager.AddToRoleAsync(user, adminRole);
                }
            }
        }

    }
}
