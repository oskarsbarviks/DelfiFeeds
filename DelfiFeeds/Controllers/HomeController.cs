using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DelfiFeeds.Models;
using Microsoft.AspNetCore.Authorization;
using FrontendLogic.Interfaces;
using Repositories.Interfaces;
using System.Security.Claims;

namespace DelfiFeeds.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginLogic _loginLogic;
        private readonly IFeedRepository _feedRepoistory;
        public HomeController(ILoginLogic loginLogic, IFeedRepository feedRepoistory)
        {
            _loginLogic = loginLogic;
            _feedRepoistory = feedRepoistory;
        }

        [HttpGet]
        public IActionResult Index()
        { 
            return View();
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
            var userID = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var model = await _feedRepoistory.GetFeedsByUserID(userID, "Aculiecinieks", 12);          
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> FeedsPartial()
        {
            var userID = User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            return PartialView(await _feedRepoistory.GetFeedsByUserID(userID, "Aculiecinieks", 12));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
