using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

            ViewBag.User = db.Users.Find(currentUserId);

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
        public long CreateCreative(string name)
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());

            Creative creative = new Creative();
            creative.Name = name;
            creative.ApplicationUser = currentUser;
            db.Creatives.Add(creative);

            Award(currentUser);
            db.SaveChanges();

            return creative.Id;
        }

        public void Award(ApplicationUser user) {
            var badge = db.Badges.FirstOrDefault(x => x.Amount == user.Creatives.Count); 
            if (badge != null && !user.Badges.Contains(badge))
            {
                user.Badges.Add(badge);
            }           
        }

        [Authorize]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Creative creative = db.Creatives.Find(id);
            if (creative == null)
            {
                return HttpNotFound();
            }
            db.Creatives.Remove(creative);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {

            }
            return RedirectToAction("Index");
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
            creative.LastEditTime = DateTime.Now;
            db.Entry(creative).State = EntityState.Modified;
            db.SaveChanges();
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


        [Authorize]
        public void ChangeCreativeName(long Id, string Name)
        {
            Creative creative = db.Creatives.Find(Id);
            creative.Name = Name;

            db.Entry(creative).State = EntityState.Modified;
            db.SaveChanges();
            return;
        }

        public string SetHeaderContext(int id)
        {
            var header = db.Headers.Find(id);
            var serializer = new JavaScriptSerializer();
            var simpleHeader = new
            {
                Id = header.Id,
                Text = header.Text,
                Name = header.Name,
                Tags = header.Tags.Select(x => x.Name)
            };

            return serializer.Serialize(simpleHeader);
        }

        [Authorize]
        public ActionResult AddHeader(long id)
        {
            Creative creative = db.Creatives.Find(id);
            if (creative == null)
            {
                return HttpNotFound();
            }
            try
            {
                var header = new Header();
                header.Order = creative.Headers.Count + 1;

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
        public void DeleteHeader(long id)
        {
            Header header = db.Headers.Find(id);
            
            try
            {
                db.Headers.Remove(header);
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
        }



        public void UpdateHeader(long Id, string Name, string Text, List<string> Tags)
        {
            Header header = db.Headers.Find(Id);
            header.Name = Name;
            header.Text = Text;
            header.Tags.Clear();

            if (Tags != null)
            {
                foreach (string tagName in Tags)
                {
                    Tag t = db.Tags.FirstOrDefault(x => x.Name == tagName);
                    if (t == null)
                    {
                        t = new Tag() { Name = tagName };
                        db.Tags.Add(t);
                        db.SaveChanges();
                    }
                    header.Tags.Add(t);
                }
            }

            db.Entry(header).State = EntityState.Modified;
            db.SaveChanges();
            return;
        }

        public void UpdateHeaderOrders(int[] headerOrders)
        {
            for (int i = 0; i < headerOrders.Length; i++)
            {
                Header header = db.Headers.Find(headerOrders[i]);
                header.Order = i + 1;
                db.Entry(header).State = EntityState.Modified;
            }
            db.SaveChanges();
            return;
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
                try
                {
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    throw e;
                }
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