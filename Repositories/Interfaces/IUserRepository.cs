using Repositories.DataObject;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDataObject> GetUserByID(string userID);
        Task CreateOrUpdateUser(string userID, string email, string fullName, byte[] picture);
    }
}