using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Course.Models
{
    public class Badge
    {
        [Required]
        public long Id { get; set; }
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Name { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public Badge()
        {
            ApplicationUsers = new List<ApplicationUser>();
        }
    }
}