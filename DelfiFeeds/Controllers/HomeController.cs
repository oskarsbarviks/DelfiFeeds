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

namespace DelfiFeeds.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoginLogic _loginLogic;

        public HomeController(ILogger<HomeController> logger, ILoginLogic loginLogic)
        {
            _logger = logger;
            _loginLogic = loginLogic;
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
        public IActionResult Feeds()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
