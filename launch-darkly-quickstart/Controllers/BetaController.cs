using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LaunchDarklyQuickstart.Models;
using Microsoft.Extensions.Configuration;
using LaunchDarkly.Sdk.Server.Interfaces;
using LaunchDarkly.Sdk.Server;
using User = LaunchDarkly.Sdk.User;

namespace LaunchDarklyQuickstart.Controllers
{
    public class BetaController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILdClient _ldClient;

        public BetaController(ILogger<HomeController> logger, ILdClient ldClient)
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
    }
}
