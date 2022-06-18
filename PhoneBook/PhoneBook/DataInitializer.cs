using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Type = DAL.Models.Type;

namespace PhoneBook
{
    public static class DataInitializer
    {
        public static void SeedData(RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            SeedRoles(roleManager);
            SeedTypes(context);
        }
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
        public static void SeedTypes(ApplicationDbContext context)
        {
            if (!context.Type.Any())
            {
                var TypesList = new List<DAL.Models.Type>()
                {
                    new Type
                    {
                        Name="Home"
                    },
                    new Type
                    {
                        Name="Work"
                    },
                    new Type
                    {
                        Name="PhoneCell"
                    },
                    new Type
                    {
                        Name="Other"
                    }
                };
                context.Type.AddRange(TypesList);
                context.SaveChanges();
            }
        }
    }
}
