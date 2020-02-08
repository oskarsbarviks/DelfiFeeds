using System;

namespace HostedServices.HttpClients.DTO
{
    /// <summary>
    /// Delfi feed Http call DTO class
    /// </summary>
    public class DelfiFeed
    {
        public string ID { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CommentCount { get; set; }
        public Uri PictureUrl { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
