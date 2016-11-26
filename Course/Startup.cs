using Microsoft.Owin;
using Owin;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Course.Models;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(Course.Startup))]
namespace Course
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            var context = new ApplicationDbContext();
            Lucene.LuceneSearch.BuildIndex(context.Headers.Include(x => x.Creative)
                            .Include(x => x.Creative.ApplicationUser).Include(x => x.Tags));

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var roleAdmin = new IdentityRole { Name = "admin" };

            if (!roleManager.RoleExists("admin"))
            {
                roleManager.Create(roleAdmin);
            }

            var admin = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com" };

            string password = "adminme";
            try
            {
                var result = userManager.Create(admin, password);
                if (result.Succeeded)
                {
                    userManager.AddToRole(admin.Id, roleAdmin.Name);
                }

                context.SaveChanges();

            }
            catch(System.Exception e)
            {
                throw e;
            }
           
        }
    }
}
