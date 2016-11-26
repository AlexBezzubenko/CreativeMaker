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
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Course.Filters;
using System.Web.Script.Serialization;

namespace Course.Controllers
{
  
    [Culture]
    [Authorize]
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(String id)
        {
            var currentUserId = id ?? User.Identity.GetUserId();
            var user = db.Users.Find(currentUserId);
            var creatives = db.Creatives.Where(x => x.ApplicationUser.Id == currentUserId)
                                .Include(x => x.ApplicationUser).Include(x => x.Headers);
            return View(new UserViewModels(creatives, user));
        }

        [HttpPost]
        public long CreateCreative(string name, string userId)
        {
            var currentUserId = userId ?? User.Identity.GetUserId();
            var currentUser = db.Users.Find(currentUserId);
            var creative = new Creative()
            {
                Name = name,
                ApplicationUser = currentUser
            };
            db.Creatives.Add(creative);

            Award(currentUser);
            db.SaveChanges();

            return creative.Id;
        }

        private void Award(ApplicationUser user) {
            var badge = db.Badges.FirstOrDefault(x => x.Amount == user.Creatives.Count); 
            if (badge != null && !user.Badges.Contains(badge))
            {
                user.Badges.Add(badge);
            }           
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Creative creative = db.Creatives.Find(id);
            if (creative == null)
            {
                return HttpNotFound();
            }

            DeleteCreativeDocuments(creative);

            var userId = creative.ApplicationUser.Id; 
            db.Creatives.Remove(creative);
            
            db.SaveChanges();

            return RedirectToAction("Index", "User", new { id = userId });
        }

        private void DeleteCreativeDocuments(Creative c)
        {
            foreach (var h in c.Headers)
            {
                Lucene.LuceneSearch.DeleteDocument(h);
            }
        }

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


        public void ChangeCreativeName(long Id, string Name)
        {
            Creative creative = db.Creatives.Find(Id);
            creative.Name = Name;

            foreach(var h in creative.Headers)
            {
                Lucene.LuceneSearch.UpdateDocument(h);
            }

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

                Lucene.LuceneSearch.AddToIndex(header);

                return PartialView("~/Views/User/Header.cshtml", header);
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
        }

        
        public void DeleteHeader(long id)
        {
            Header header = db.Headers.Find(id);
            
            try
            {
                Lucene.LuceneSearch.DeleteDocument(header);
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

            Lucene.LuceneSearch.UpdateDocument(header);
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