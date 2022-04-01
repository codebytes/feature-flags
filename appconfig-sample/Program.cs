using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using appconfig_sample.Data;
using appconfig_sample;

var builder = WebApplication.CreateBuilder(args);

var appConfigConnectionString = builder.Configuration.GetConnectionString("AppConfig");

// builder.Configuration.AddAzureAppConfiguration(connectionString);
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(appConfigConnectionString)
           // Configure to reload configuration if the registered key 'WebDemo:Sentinel' is modified.
           // Use the default cache expiration of 30 seconds. It can be overriden via AzureAppConfigurationRefreshOptions.SetCacheExpiration.
           .ConfigureRefresh(refreshOptions =>
           {
               refreshOptions.Register("Sentinel", refreshAll: true)
                             .SetCacheExpiration(TimeSpan.FromSeconds(5)); 
           })
           // Load all feature flags with no label. To load specific feature flags and labels, set via FeatureFlagOptions.Select.
           // Use the default cache expiration of 30 seconds. It can be overriden via FeatureFlagOptions.CacheExpirationInterval.
           .UseFeatureFlags(featureFlagOptions =>
           {
               featureFlagOptions.CacheExpirationInterval = TimeSpan.FromSeconds(5);
           });
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddAzureAppConfiguration()
                .AddFeatureManagement()
                .AddFeatureFilter<TargetingFilter>();

builder.Services.AddSingleton<ITargetingContextAccessor, TestTargetingContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
