using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course.Models
{
    public class ViewCreativeViewModels
    {
        public Creative Creative { get; set; }
        public long SelectedHeaderId { get; set; }
        public string UserMark { get; set; }
        public bool IsOwnerOrNotAuthenticated { get; set; }

        public ViewCreativeViewModels(Creative creative, long selectedHeaderId,
                                        string userMark, bool isOwnerOrNotAuthenticated)
        {
            Creative = creative;
            SelectedHeaderId = selectedHeaderId;
            UserMark = userMark;
            IsOwnerOrNotAuthenticated = isOwnerOrNotAuthenticated;
        }
    }
}