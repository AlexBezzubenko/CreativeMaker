using System.Collections.Generic;

namespace Course.Models
{
    public class UserViewModel
    {
        public IEnumerable<Creative> Creatives { get; set; }
        public ApplicationUser User { get; set; }

        public UserViewModel(IEnumerable<Creative> creatives, ApplicationUser user)
        {
            Creatives = creatives;
            User = user;
        }
    }

    public class EditViewModel
    {
        public Creative Creative { get; set; }
        public string JsonTags { get; set; }

        public EditViewModel(Creative creative, string jsonTags)
        {
            Creative = creative;
            JsonTags = jsonTags;
        }
    }
}