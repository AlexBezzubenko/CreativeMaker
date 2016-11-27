using System.Collections.Generic;

namespace Course.Models
{
    public class EditViewModels
    {
        public Creative Creative { get; set; }
        public string JsonTags { get; set; }

        public EditViewModels(Creative creative, string jsonTags)
        {
            Creative = creative;
            JsonTags = jsonTags;
        }
    }
}