using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Course.Models;
using Course.Filters;

namespace Course.Controllers
{
    [Culture]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Users);
        }

        public async Task<ActionResult> Lock(string id)
        {
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            await userManager.LockUserAccount(id, null);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Unlock(string id)
        {
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            await userManager.UnlockUserAccount(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(string id)
        {
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = await userManager.FindByIdAsync(id);

            await userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }
    }
}