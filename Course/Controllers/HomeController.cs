using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Course.Models;
using Course.Filters;

using System.Data.Entity;
 

namespace Course.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.LastCreatives = db.Creatives.Include(x => x.ApplicationUser)
                                .Include(x => x.Headers).OrderByDescending(u => u.LastEditTime).Take(5);
            ViewBag.PopularCreatives = db.Creatives.Include(x => x.ApplicationUser)
                                .Include(x => x.Headers).OrderByDescending(u => u.Views).Take(5);
            //ViewBag.Tags = db.Tags.GroupBy(p => p.Name).Select(g => g.FirstOrDefault());

            return View();
        }

        public ActionResult HeadersByTag(int id)
        {
            var tag = db.Tags.Find(id);
            var headers = db.Headers.Where(x => x.Tags.Where(y => y.Name == tag.Name).Any()).Include(x => x.Tags).Include(x=>x.Creative);
            ViewBag.Headers = headers;
            return View();
        }

        public ActionResult ChangeCulture(String lang)
        {
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;   // если куки уже установлено, то обновляем значение
            else
            {

                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        public ActionResult ChangeTheme(string themename)
        {
            Session["CssTheme"] = themename;
            if (Request.UrlReferrer != null)
            {
                var returnUrl = Request.UrlReferrer.ToString();
                return new RedirectResult(returnUrl);
            }
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}