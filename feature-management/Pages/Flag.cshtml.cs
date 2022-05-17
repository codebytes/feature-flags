using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement.Mvc;

namespace feature_management.Pages
{
    [FeatureGate(FeatureFlags.FlagController)]
    public class FlagModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
