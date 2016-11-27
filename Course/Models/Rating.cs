using System.ComponentModel.DataAnnotations;

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