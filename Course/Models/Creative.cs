using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Course.Models
{
    public class Creative
    {
        [Required]
        public long Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        public long Views { get; set; }
        public double Rating { get; set; }
        public long RatingsAmount { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime CreationTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime LastEditTime { get; set; }

        public virtual Collection<Header> Headers { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public Creative()
        {
            Views = 0;
            Rating = 0;
            RatingsAmount = 0;
            CreationTime = DateTime.Now;
            LastEditTime = DateTime.Now;
            Headers = new Collection<Header>();
        }
    }
}