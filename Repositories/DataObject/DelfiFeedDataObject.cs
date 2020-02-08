using System;

namespace Repositories.DataObject
{
    /// <summary>
    /// Database class object. Table - Feeds
    /// </summary>
    public class DelfiFeedDataObject
    {
        public string ID { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CommentCount { get; set; }
        public Uri PictureUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public string Category { get; set; }
    }
}
