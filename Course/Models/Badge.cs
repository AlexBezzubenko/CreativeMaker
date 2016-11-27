using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Course.Models
{
    public class Badge
    {
        [Required]
        public long Id { get; set; }

        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        public int Amount { get; set; } = 0;
        public int Order { get; set; } = 1;
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public Badge()
        {
            ApplicationUsers = new List<ApplicationUser>();
        }
    }
}