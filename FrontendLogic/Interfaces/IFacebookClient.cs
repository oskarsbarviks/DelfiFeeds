using FrontendLogic.FacebookClient;
using System.Threading.Tasks;

namespace FrontendLogic.Interfaces
{
    public interface IFacebookClient
    {
        Task<FacebookUserDto> GetUserDataFromAccessToken(string accessToken);
        Task<byte[]> GetPictureByteArrayByUserId(string userID);
    }
}