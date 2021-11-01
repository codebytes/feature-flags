using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LaunchDarklyQuickstart.Models;
using Microsoft.Extensions.Configuration;
using User = LaunchDarkly.Sdk.User;
using LaunchDarkly.Sdk.Server.Interfaces;

namespace LaunchDarklyQuickstart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private readonly ILdClient _ldClient;

        public HomeController(ILogger<HomeController> logger, ILdClient ldClient)
        {
            _logger = logger;
            _ldClient = ldClient;
        }

        public IActionResult Index(string email="default email address")
        {
            User user = LaunchDarkly.Sdk.User.WithKey(email);
            ViewBag.BetaFeatureEnabled = _ldClient.BoolVariation("beta", user, false);
            return View();
        }

        public IActionResult Privacy()
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
