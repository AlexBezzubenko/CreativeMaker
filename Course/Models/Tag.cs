using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Course.Models
{
    public class Tag
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Name { get; set; }
        public virtual ICollection<Header> Headers { get; set; }
        public Tag()
        {
            this.Headers = new List<Header>();
        }

    }
}