using HostedServices.HttpClients.DTO;
using Repositories.DataObject;
using System.Collections.Generic;
using System.Linq;

namespace HostedServices.Extensions
{
    /// <summary>
    /// Extensions class for Delfi feeds repository class
    /// </summary>
    public static class DelfiFeedToRepositoryObjectMapper
    {
        /// <summary>
        /// Maps Http Dto class to Database object
        /// </summary>
        public static List<DelfiFeedDataObject> ToDatabaseObject(this List<DelfiFeed> feeds, string category)
        {
            return feeds.Select(feed => new DelfiFeedDataObject
            {
                ID = feed.ID,
                Title = feed.Title,
                Description = feed.Description,
                CommentCount = feed.CommentCount,
                Link = feed.Link,
                Category = category,
                PictureUrl = feed.PictureUrl,
                PublishDate = feed.PublishDate
            }).ToList();
        }
    }
}
