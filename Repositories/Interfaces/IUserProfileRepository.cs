using Repositories.DataObject;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<UserProfileDataObject> GetUserProfileDataByUserID(string userID, string defaultCategory, int feedCount);
    }
}