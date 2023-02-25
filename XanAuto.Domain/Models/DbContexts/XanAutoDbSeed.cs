using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XanAuto.Domain.Models.Entities.Membership;
using System;

namespace XanAuto.Domain.Models.DbContexts
{
    public static class XanAutoDbSeed
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<XanAutoDbContext>();

                db.Database.Migrate();

            }

            return app;
        }

        public static IApplicationBuilder SeedMembership(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<XanAutoUser>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<XanAutoUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<XanAutoRole>>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                string superAdminRoleName = configuration["defaultAccount:superAdmin"];
                string superAdminEmail = configuration["defaultAccount:email"];
                string superAdminUserName = configuration["defaultAccount:username"];
                string superAdminPassword = configuration["defaultAccount:password"];
                string superAdminName = configuration["defaultAccount:name"];
                string superAdminSurname = configuration["defaultAccount:surname"];

                var superAdminRole = roleManager.FindByNameAsync(superAdminRoleName).Result;


                if (superAdminRole == null)
                {
                    superAdminRole = new XanAutoRole
                    {
                        Name = superAdminRoleName,
                    };

                    var roleResult = roleManager.CreateAsync(superAdminRole).Result;

                    if (!roleResult.Succeeded)
                    {
                        throw new Exception("Problem at RoleCreating.....");
                    }
                }

                var superAdminUser = userManager.FindByEmailAsync(superAdminEmail).Result;

                if (superAdminUser == null)
                {
                    superAdminUser = new XanAutoUser
                    {
                        Email = superAdminEmail,
                        UserName = superAdminUserName,
                        EmailConfirmed = true,
                        Name = superAdminName,
                        Surname = superAdminSurname
                    };

                    var userResult = userManager.CreateAsync(superAdminUser, superAdminPassword).Result;

                    if (!userResult.Succeeded)
                    {
                        throw new Exception("Problem occurred when user is created");
                    }

                }

                var isInRole = userManager.IsInRoleAsync(superAdminUser, superAdminRole.Name).Result;

                if (isInRole != true)
                {
                    userManager.AddToRoleAsync(superAdminUser, superAdminRole.Name).Wait();
                }

            }


            return app;
        }

        public static void SeedUserRole(RoleManager<XanAutoRole> roleManager)
        {

            if (!roleManager.RoleExistsAsync("user").Result)
            {
                XanAutoRole role = new XanAutoRole();
                role.Name = "user";

                IdentityResult roleResult = roleManager.

                CreateAsync(role).Result;
            }

        }
    }
}
