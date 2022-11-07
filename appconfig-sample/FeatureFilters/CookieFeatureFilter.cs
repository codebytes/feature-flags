namespace appconfig_sample.FeatureFilters;
public class CookieFilterSettings
{
    public string CookieName { get; set; }
}

[FilterAlias(nameof(FeatureFlags.CookieFilter))]
public class CookieFeatureFilter : IFeatureFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CookieFeatureFilter(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
    {
        var settings = context.Parameters.Get<CookieFilterSettings>();
        var containsCookie = _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(settings.CookieName);
        return Task.FromResult(containsCookie);
    }
}
