using System.Collections.Generic;

namespace Course.Models
{
    public class HomeViewModels
    {
        public IEnumerable<Creative> LastCreatives { get; set; }
        public IEnumerable<Creative> PopularCreatives { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

        public HomeViewModels(IEnumerable<Creative> lastCreatives, IEnumerable<Creative> popularCreatives,
                              IEnumerable<Tag> tags)
        {
            LastCreatives = lastCreatives;
            PopularCreatives = popularCreatives;
            Tags = tags; 
        }
    }
}