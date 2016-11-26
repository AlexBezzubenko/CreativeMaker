using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Course.Models;
using Course.Lucene;
using Course.Filters;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace Course.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var lastCreatives = db.Creatives.Include(x => x.ApplicationUser)
                                .Include(x => x.Headers).OrderByDescending(u => u.LastEditTime).Take(5);
            var popularCreatives = db.Creatives.Include(x => x.ApplicationUser)
                                .Include(x => x.Headers).OrderByDescending(u => u.Views).Take(5);
            var tags = db.Tags.ToList();
            return View(new HomeViewModels(lastCreatives, popularCreatives, tags));
        }

        public ActionResult HeadersByTag(int id)
        {
            var creativeResults = new List<CreativeResult>();

            foreach (var h in db.Tags.Find(id).Headers) {
                creativeResults.Add(CreateCreativeResult(h));
            }

            return View("Search", creativeResults);
        }

        private CreativeResult CreateCreativeResult(Header h)
        {
            var result = new CreativeResult()
            {
                CreativeId = h.Creative.Id,
                CreativeName = h.Creative.Name,
                HeaderId = h.Id,
                HeaderName = h.Name,
                Rating = h.Creative.Rating,
                UserName = h.Creative.ApplicationUser.UserName
            };
            return result;
        }

        public ActionResult ChangeCulture(String lang)
        {
            HttpCookie cookie = Request.Cookies["lang"];

            if (cookie != null)
                cookie.Value = lang;    
            else
            {
                cookie = SetCookieLanguage(lang);
            }
            Response.Cookies.Add(cookie);
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        private HttpCookie SetCookieLanguage(String lang)
        {
            var cookie = new HttpCookie("lang");
            cookie.HttpOnly = false;
            cookie.Value = lang;
            cookie.Expires = DateTime.Now.AddYears(1);
            return cookie;
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

        public ActionResult Search(string query)
        {
            var result = LuceneSearch.Search(query, 100);

            foreach(var creativeResult in result)
            {
                creativeResult.Rating = db.Creatives.Find(creativeResult.CreativeId).Rating;
            }

            return View(result);
        }

        public ActionResult View(long id, long selectedHeaderId = -1)
        {
            Creative creative = db.Creatives.Find(id);
            if (creative == null)
            {
                return HttpNotFound();
            }

            bool isOwner = User.Identity.GetUserId() == creative.ApplicationUser.Id;

            if (!isOwner)
            {
                creative.Views++;
                db.Entry(creative).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            bool isOwnerOrNotAuthenticated = isOwner || !User.Identity.IsAuthenticated;

            var userMark = "0";

            if (!isOwnerOrNotAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var rating = db.Ratings.Where(x => x.ApplicationUser.Id == userId
                                && x.Creative.Id == id).FirstOrDefault();

                if (rating != null)
                {
                    userMark = rating.Value.ToString().Replace(',', '.');
                }
            }
            return View(new ViewCreativeViewModels(creative, selectedHeaderId, userMark, isOwnerOrNotAuthenticated));
        }
    }
}