using AspireGeneralAvailabilityDemoHarness.Web;
using AspireGeneralAvailabilityDemoHarness.Web.Billing;
using AspireGeneralAvailabilityDemoHarness.Web.Components;
using Raygun4Aspire;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

builder.AddRaygun();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<WeatherApiClient>(client =>
    {
        // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
        // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
        client.BaseAddress = new("https+http://apiservice");
    });

builder.Services.AddAuthentication(options =>
  {
    options.DefaultAuthenticateScheme = "MyAuthScheme";
    options.DefaultChallengeScheme = "MyAuthScheme";
  })
  .AddCookie("MyAuthScheme");

//builder.Services.AddTransient<CartService>();

var app = builder.Build();

app.UseRaygun();

//if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.UseAuthentication();

app.Run();
