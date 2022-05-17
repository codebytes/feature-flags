using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement;

namespace feature_management.Pages;

public class IndexModel : PageModel
{
    public readonly IFeatureManager _featureManager;
    private readonly ILogger<IndexModel> _logger;
    public string Message { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IFeatureManager featureManager)
    {
        _logger = logger;
        _featureManager = featureManager;
    }

    public async Task OnGet()
    {
        Message = await _featureManager.IsEnabledAsync(nameof(FeatureFlags.FeatureA))
                        ? "Welcome to the feature flag"
                        : "Welcome";
    }
}
