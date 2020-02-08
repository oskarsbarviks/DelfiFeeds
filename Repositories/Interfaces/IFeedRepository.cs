using Repositories.DataObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IFeedRepository
    {
        Task<bool> UpdateOrInsertFeeds(List<DelfiFeedDataObject> feeds);
        Task<List<DelfiFeedDataObject>> GetFeedsByUserID(string userID, string defaultCattegory, int defaultCount);
    }
}