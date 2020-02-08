using FrontendLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Repositories.Interfaces;
using System.Threading.Tasks;

namespace FrontendLogic
{
    /// <summary>
    /// Class responsible for user login
    /// </summary>
    public class LoginLogic : ILoginLogic
    {
        private IUserRepository _userRepository;
        private IAuthorizationLogic _authorizationLogic;
        private IFacebookClient _facebookClient;
        public LoginLogic
            (
            IUserRepository userRepository,
            IAuthorizationLogic authorizationLogic,
            IFacebookClient facebookClient
            )
        {
            _userRepository = userRepository;
            _authorizationLogic = authorizationLogic;
            _facebookClient = facebookClient;
        }

        /// <summary>
        /// Registers and signs in users
        /// </summary>
        public async Task RegisterUser(string accessToken, string userID, HttpContext httpContext)
        {
            var user = await _userRepository.GetUserByID(userID);
            if (user == null)
            {
                var facebookPictureTask = _facebookClient.GetPictureByteArrayByUserId(userID);
                var facebookUser = await _facebookClient.GetUserDataFromAccessToken(accessToken);
                await _userRepository.CreateOrUpdateUser(userID, facebookUser.Email, facebookUser.Name, await facebookPictureTask);
                await _authorizationLogic.AuthorizeUser(userID, facebookUser.Name, facebookUser.Email, httpContext);
            }
            else
            {
                // create auth cookie, add claims, authorize user
                await _authorizationLogic.AuthorizeUser(userID, user.FullName, user.Email, httpContext);
            }
        }
    }
}
