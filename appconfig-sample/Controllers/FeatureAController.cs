namespace appconfig_sample.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

public class FeatureAController: Controller
    {
        private readonly IFeatureManager _featureManager;

        public FeatureAController(IFeatureManagerSnapshot featureManager) =>
            _featureManager = featureManager;

        [FeatureGate(MyFeatureFlags.FeatureA)]
        public IActionResult Index() => View();
    }
