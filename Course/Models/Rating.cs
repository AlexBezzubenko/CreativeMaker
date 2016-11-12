using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Course.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public double Value { get; set; }
        
        public virtual Creative Creative { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}