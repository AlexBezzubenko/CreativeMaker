using Microsoft.Owin;
using Owin;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Course.Models;

[assembly: OwinStartupAttribute(typeof(Course.Startup))]
namespace Course
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            var context = new ApplicationDbContext();
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var roleAdmin = new IdentityRole { Name = "admin" };
            //var roleUser = new IdentityRole { Name = "user" };

            if (!roleManager.RoleExists("admin"))
            {
                roleManager.Create(roleAdmin);
            }
            /*if (!roleManager.RoleExists("user"))
            {
                roleManager.Create(roleUser);
            }*/

            var admin = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com" };

            string password = "adminme";
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, roleAdmin.Name);
                //userManager.AddToRole(admin.Id, roleUser.Name);
            }

            context.SaveChanges();
        }
    }
}
