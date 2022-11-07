namespace appconfig_sample.FeatureFilters;
public class BrowserFilterSettings
{
    public string[] AllowedBrowsers { get; set; }
}

[FilterAlias(nameof(FeatureFlags.BrowserFilter))]
public class BrowserFeatureFilter : IFeatureFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public BrowserFeatureFilter(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
        var settings = context.Parameters.Get<BrowserFilterSettings>();
        return Task.FromResult(settings.AllowedBrowsers.Any(userAgent.Contains));
    }
}
