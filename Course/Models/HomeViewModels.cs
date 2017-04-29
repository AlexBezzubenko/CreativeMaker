using System.Collections.Generic;

namespace Course.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Creative> LastCreatives { get; set; }
        public IEnumerable<Creative> PopularCreatives { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

        public HomeViewModel(IEnumerable<Creative> lastCreatives, IEnumerable<Creative> popularCreatives,
                              IEnumerable<Tag> tags)
        {
            LastCreatives = lastCreatives;
            PopularCreatives = popularCreatives;
            Tags = tags; 
        }
    }

    public class ViewCreativeViewModel
    {
        public Creative Creative { get; set; }
        public long SelectedHeaderId { get; set; }
        public string UserMark { get; set; }
        public bool IsOwner { get; set; }

        public ViewCreativeViewModel(Creative creative, long selectedHeaderId,
                                        string userMark, bool isOwner)
        {
            Creative = creative;
            SelectedHeaderId = selectedHeaderId;
            UserMark = userMark;
            IsOwner = isOwner;
        }
    }
}