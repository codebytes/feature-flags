using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using appconfig_sample.Models;

namespace appconfig_sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeatureManager _featureManager;

        public HomeController(ILogger<HomeController> logger, IFeatureManager featureManager)
        {
            _logger = logger;
            _featureManager = featureManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Flags()
        {
            var flagTasks = Enum.GetNames(typeof(appconfig_sample.FeatureFlags))
            .Select(async f => new { FlagName = f, Enabled = await _featureManager.IsEnabledAsync(f) });

            var flags = await Task.WhenAll(flagTasks);
            ViewBag.Flags = flags;



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
