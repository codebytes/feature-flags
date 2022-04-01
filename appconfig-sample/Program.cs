using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using appconfig_sample.Data;
using appconfig_sample;

var builder = WebApplication.CreateBuilder(args);

var appConfigConnectionString = builder.Configuration.GetConnectionString("AppConfig");
builder.Host.ConfigureAppConfiguration(builder =>
                {
                    //Connect to your App Config Store using the connection string
                    builder
                        .AddAzureAppConfiguration(options =>
                            options.Connect(appConfigConnectionString)
                                .ConfigureRefresh(refreshOpt =>
                                    {
                                        refreshOpt.Register(key: "Beta", refreshAll: true)
                                        .SetCacheExpiration(new TimeSpan(0, 0, 5));
                                    })
                                .UseFeatureFlags());
                });

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddFeatureManagement()
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
