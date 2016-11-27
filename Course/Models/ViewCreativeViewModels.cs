namespace Course.Models
{
    public class ViewCreativeViewModels
    {
        public Creative Creative { get; set; }
        public long SelectedHeaderId { get; set; }
        public string UserMark { get; set; }
        public bool IsOwner { get; set; }

        public ViewCreativeViewModels(Creative creative, long selectedHeaderId,
                                        string userMark, bool isOwner)
        {
            Creative = creative;
            SelectedHeaderId = selectedHeaderId;
            UserMark = userMark;
            IsOwner = isOwner;
        }
    }
}