namespace Affinity.Migrations
{
    using Affinity.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Affinity.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Affinity.Models.ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                // Add missing roles
                var role = roleManager.FindByName("admin");
                if (role == null)
                {
                    role = new IdentityRole("admin");
                    roleManager.Create(role);
                }

                var developerRole = roleManager.FindByName("developer");

                if (developerRole == null)
                {
                    developerRole = new IdentityRole("developer");
                    roleManager.Create(developerRole);
                }

                var projectManager = roleManager.FindByName("project manager");
                if (projectManager == null)
                {
                    projectManager = new IdentityRole("project manager");
                    roleManager.Create(projectManager);
                }

                // Create admin test user
                var user = userManager.FindByName("admin@admin.net");
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        UserName = "admin@admin.net",
                        Email = "admin@admin.net",
                        PhoneNumber = "5551234567",
                    };
                    userManager.Create(newUser, "Password@1");
                    userManager.SetLockoutEnabled(newUser.Id, false);
                    userManager.AddToRole(newUser.Id, "admin");
                }

                //Create Developer test user
                var developer = userManager.FindByName("developer@developer.net");
                if (developer == null)
                {
                    var newDeveloper = new ApplicationUser()
                    {
                        UserName = "developer@developer.net",
                        Email = "developer@developer.net",
                        PhoneNumber = "5551234567",
                    };

                    userManager.Create(newDeveloper, "Password@1");
                    userManager.SetLockoutEnabled(newDeveloper.Id, false);
                    userManager.AddToRole(newDeveloper.Id, "developer");
                }

                //Create Project Manager test user
                var projectManagerUser = userManager.FindByName("projectmanager@projectmanager.net");
                if(projectManagerUser == null)
                {
                    var newProjectManager = new ApplicationUser()
                    {
                        UserName = "projectmanager@projectmanager.net",
                        Email = "projectmanager@projectmanager.net",
                        PhoneNumber = "5551234567",
                    };

                    userManager.Create(newProjectManager, "Password@1");
                    userManager.SetLockoutEnabled(newProjectManager.Id, false);
                    userManager.AddToRole(newProjectManager.Id, "developer");
                }
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
