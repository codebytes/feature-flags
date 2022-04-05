using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace appconfig_sample.Controllers
{
    public class FeatureAController: Controller
    {
        private readonly IFeatureManager _featureManager;

        public FeatureAController(IFeatureManager featureManager) =>
            _featureManager = featureManager;

        [FeatureGate(MyFeatureFlags.Beta)]
        public IActionResult Index() => View();
    }
}