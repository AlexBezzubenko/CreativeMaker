using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Course.Models
{
    public class Tag
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 20, MinimumLength = 1)]
        public string Name { get; set; }
        public virtual ICollection<Header> Headers { get; set; }
        public Tag()
        {
            Headers = new List<Header>();
        }

    }
}