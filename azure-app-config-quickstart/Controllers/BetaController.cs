using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace azure_app_config_quickstart.Controllers
{
    public class BetaController: Controller
    {
        private readonly IFeatureManager _featureManager;

        public BetaController(IFeatureManagerSnapshot featureManager) =>
            _featureManager = featureManager;

        [FeatureGate(MyFeatureFlags.Beta)]
        public IActionResult Index() => View();
    }
}