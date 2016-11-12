using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Course.Models
{
    public class Header
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 80, MinimumLength = 3)]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public Creative Creative { get; set; }

        public Header()
        {
            Name = "New header";
            Text = "";
            Tags = new List<Tag>();
        }
    }
}
