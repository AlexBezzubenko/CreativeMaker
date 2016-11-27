using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Course.Models
{
    public class Header
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public int Order { get; set; } = 1;
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual Creative Creative { get; set; }

        public Header()
        {
            Name = "New header";
            Text = "";
            Tags = new List<Tag>();
        }
    }
}
