using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
 
using System.Collections.Generic;

namespace Course.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Badge> Badges { get; set; }

        public virtual ICollection<Creative> Creatives { get; set; }
        public virtual DateTime RegistrationDate { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public ApplicationUser()
        {
            RegistrationDate = DateTime.Now;
            Ratings = new List<Rating>();
            Badges = new List<Badge>();
            Creatives = new List<Creative>();
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Creative> Creatives { get; set; }
        public DbSet<Header> Headers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*modelBuilder.Entity<Header>()
            .HasOptional(h => h.Creative)
            .WithMany()
            .WillCascadeOnDelete(true);*/

            modelBuilder.Entity<ApplicationUser>()
            .HasMany(x => x.Ratings)
            .WithRequired(x => x.ApplicationUser)
            .WillCascadeOnDelete();

            modelBuilder.Entity<ApplicationUser>()
            .HasMany(x => x.Creatives)
            .WithRequired(x => x.ApplicationUser)
            .WillCascadeOnDelete();

            modelBuilder.Entity<Creative>()
            .HasMany(x => x.Headers)
            .WithRequired(x => x.Creative)
            .WillCascadeOnDelete();

            modelBuilder.Entity<Header>().HasMany(h => h.Tags)
            .WithMany(t => t.Headers)
            .Map(q => q.MapLeftKey("HeaderId")
            .MapRightKey("TagId")
            .ToTable("HeaderTag"));

            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Badges)
            .WithMany(b => b.ApplicationUsers)
            .Map(q => q.MapLeftKey("ApplicationUserId")
            .MapRightKey("BadgeId")
            .ToTable("ApplicationUserBadge"));
        }
    }
}