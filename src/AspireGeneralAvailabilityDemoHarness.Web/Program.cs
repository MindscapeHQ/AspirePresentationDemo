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

builder.Services.AddAuthentication(options =>
  {
    options.DefaultAuthenticateScheme = "MyAuthScheme";
    options.DefaultChallengeScheme = "MyAuthScheme";
  })
  .AddCookie("MyAuthScheme");

//builder.Services.AddTransient<CartService>();

var app = builder.Build();

app.UseRaygun();

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  app.UseHsts();
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
