using System.Collections.Generic;

namespace Course.Models
{
    public class UserViewModels
    {
        public IEnumerable<Creative> Creatives { get; set; }
        public ApplicationUser User { get; set; }

        public UserViewModels(IEnumerable<Creative> creatives, ApplicationUser user)
        {
            Creatives = creatives;
            User = user;
        }
    }
}