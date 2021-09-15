using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RafaelaColabora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RafaelaColabora.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Basic.ToString()));
        }

        public static void SeedCategories(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            // Look for any students.
            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }
            //var categories = new Category[]
            //{
            //    new Category{ Description= Enums.Categories.Comida.ToString() },
            //    new Category{ Description= Enums.Categories.Eventos.ToString() },
            //    new Category{ Description= Enums.Categories.Ropa.ToString() }
            //};
            //foreach (Category c in categories)
            //{
            //    context.Categories.Add(c);
            //}

            //var category1 = new Category { Description = Enums.Categories.Comida.ToString() };
            //var category2 = new Category { Description = Enums.Categories.Eventos.ToString() };
            //var category3 = new Category { Description = Enums.Categories.Ropa.ToString() };

            ////Seed Categories
            //context.Categories.Add(category1);
            //context.SaveChanges();
            //context.Categories.Add(category2);
            //context.SaveChanges();
            //context.Categories.Add(category3);
            //context.SaveChanges();



            var category1 = new Category { Description = Enums.Categories.Comida.ToString() };
            var category2 = new Category { Description = Enums.Categories.Eventos.ToString() };
            var category3 = new Category { Description = Enums.Categories.Ropa.ToString() };

            //Seed Categories
            context.Categories.Add(category1);
            context.Categories.Add(category2);
            context.Categories.Add(category3);
            context.SaveChanges();




            //Seed Categories
            //context.Categories.AddAsync(new Category{ Description = Enums.Categories.Comida.ToString() });
            //context.SaveChanges();
            //context.Categories.AddAsync(new Category{ Description = Enums.Categories.Eventos.ToString() });
            //context.SaveChanges();
            //context.Categories.AddAsync(new Category{ Description = Enums.Categories.Ropa.ToString() });
            //context.SaveChanges();

            //using (var transaction = context.Database.BeginTransaction())
            //{
            //    context.Categories.Add(new Category() { Description = Enums.Categories.Comida.ToString() });
            //    context.Categories.Add(new Category() { Description = Enums.Categories.Eventos.ToString() });
            //    context.Categories.Add(new Category() { Description = Enums.Categories.Ropa.ToString() });

            //    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Category ON;");
            //    context.SaveChanges();
            //    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Category OFF;");
            //    transaction.Commit();
            //}

            //context.Categories.AddRange(
            //    new Category {  Description = Enums.Categories.Comida.ToString() },
            //    new Category {  Description = Enums.Categories.Eventos.ToString() },
            //    new Category {  Description = Enums.Categories.Ropa.ToString() }
            //    );
            //context.SaveChanges();
        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "admin@hotmail.com",
                FirstName = "Super",
                LastName = "Admin",
                Cuil = "0000000000",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Super$Admin$123");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.SuperAdmin.ToString());
                }

            }
        }
    }
}
