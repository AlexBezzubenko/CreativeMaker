using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Course.Models;

using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

using System.Data.Entity;
using System.Net;
using Microsoft.AspNet.Identity;
using Course.Filters;
using System.Web.Script.Serialization;

namespace Course.Controllers
{
  
    [Culture]
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();

            ViewBag.Creatives = db.Creatives.Where(x => x.ApplicationUser.Id == currentUserId)
                                .Include(x => x.ApplicationUser).Include(x => x.Headers);
            ViewBag.User = db.Users.Find(currentUserId);
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Add() => View();

        [Authorize]
        [HttpPost]
        public ActionResult Add(Creative creative)
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());

            creative.ApplicationUser = currentUser;
            db.Creatives.Add(creative);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult AddHeader(int id)
        {
            Creative creative = db.Creatives.Find(id);
            if (creative == null)
            {
                return HttpNotFound();
            }
            try
            {
                var header = new Header();

                db.Headers.Add(header);
                creative.Headers.Add(header);

                db.SaveChanges();

                return PartialView("~/Views/User/Header.cshtml", header);
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Creative creative = db.Creatives.Find(id);
            if (creative == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tags = db.Tags.Select(x => x.Name).Distinct();
            return View(creative);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Creative creative)
        {
            creative.LastEditTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(creative).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(creative);
        }

        public ActionResult View(int id)
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
                db.SaveChanges();
            }

            bool isOwnerOrNotAuthenticated = isOwner || !User.Identity.IsAuthenticated;

            ViewBag.IsOwnerOrNotAuthenticated = isOwnerOrNotAuthenticated;

            ViewBag.UserMark = 0;

            if (!isOwnerOrNotAuthenticated) {
                var userId = User.Identity.GetUserId();
                var rating = db.Ratings.Where(x => x.ApplicationUser.Id == userId
                                && x.Creative.Id == id).FirstOrDefault();

                if (rating != null)
                {
                    ViewBag.UserMark = rating.Value.ToString().Replace(',','.');
                }
            }
            return View(creative);
        }

        public string SetHeaderContext(int id)
        {
            var header = db.Headers.Find(id);
            var serializer = new JavaScriptSerializer();

            return serializer.Serialize(header);
        }

        public string EstimateCreative(int id, double rating)
        {
            Creative creative = db.Creatives.Find(id);
            var currentUser = db.Users.Find(User.Identity.GetUserId());

            if(!db.Ratings.Any(x => x.ApplicationUser.Id == currentUser.Id && x.Creative.Id == creative.Id))
            {
                var userRating = new Rating
                {
                    ApplicationUser = currentUser,
                    Creative = creative,
                    Value = rating
                };

                creative.Rating = (creative.Rating * creative.RatingsAmount + rating)
                                    / (creative.RatingsAmount + 1);
                creative.RatingsAmount++;

                db.Ratings.Add(userRating);
                try
                {
                    db.SaveChanges();
                }catch (Exception e)
                {

                }
            }
            else
            {
                var userRating = db.Ratings.Where(x => x.ApplicationUser.Id == currentUser.Id
                    && x.Creative.Id == creative.Id).FirstOrDefault();
                
                creative.Rating = (creative.Rating * creative.RatingsAmount - userRating.Value + rating) 
                                    / creative.RatingsAmount;
                userRating.Value = rating;
                //db.Entry(creative).State = EntityState.Modified;
                db.Entry(userRating).State = EntityState.Modified;
                db.SaveChanges();
            }
            return creative.Rating.ToString("0.0").Replace(',','.');
        }
    }
}