using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LaunchDarklyQuickstart.Models;
using Microsoft.Extensions.Configuration;
using LaunchDarkly.Client;

namespace LaunchDarklyQuickstart.Controllers
{
    public class BetaController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public BetaController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index(string email="default email address")
        {
            LdClient client = new LdClient(_configuration["ConnectionStrings:AppConfig"]);
            LaunchDarkly.Client.User user = LaunchDarkly.Client.User.WithKey(email);
            ViewBag.BetaFeatureEnabled = client.BoolVariation("beta", user, false);
            return View();
        }
    }
}
