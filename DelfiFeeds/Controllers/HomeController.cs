using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DelfiFeeds.Models;
using Microsoft.AspNetCore.Authorization;
using FrontendLogic.Interfaces;
using Repositories.Interfaces;
using FrontendLogic.DTO;
using FrontendLogic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DelfiFeeds.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginLogic _loginLogic;
        private readonly IFeedRepository _feedRepoistory;
        private readonly IProfileLogic _profileLogic;
        private readonly IClaimsLogic _claimsLogic;
        private readonly IUserRepository _userRepository;
        public HomeController
            (
            ILoginLogic loginLogic,
            IFeedRepository feedRepoistory,
            IProfileLogic profileLogic,
            IClaimsLogic claimsLogic,
            IUserRepository userRepository
            )
        {
            _loginLogic = loginLogic;
            _feedRepoistory = feedRepoistory;
            _profileLogic = profileLogic;
            _claimsLogic = claimsLogic;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("Home/Feeds");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Login(string token, string userID)
        {
            await _loginLogic.RegisterUser(token, userID, HttpContext);
            return Redirect("Feeds");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Feeds()
        {
            var userID = _claimsLogic.GetUserIdFromClaims(User.Claims);
            return View(await _feedRepoistory.GetFeedsByUserID(userID, ProfileSettingsConstants.DefaultCategory, ProfileSettingsConstants.DefaultFeedCount));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userID = _claimsLogic.GetUserIdFromClaims(User.Claims);
            return View(await _profileLogic.GetProfileDataByID(userID));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Profile(ProfileModel profile)
        {
            var userID = _claimsLogic.GetUserIdFromClaims(User.Claims);
            await _userRepository.UpdateUserProfileData(
                                                        userID,
                                                        profile.Email,
                                                        profile.FullName,
                                                        profile.Category,
                                                        profile.FeedsCount
                                                        );
            return View(await _profileLogic.GetProfileDataByID(userID));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
