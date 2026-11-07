using BlazorKinetix.Demo.Components;
using BlazorKinetix.Components;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddLocalization();
builder.Services.AddBlazorKinetix(texts => texts.LoadJson(File.ReadAllText(
    Path.Combine(builder.Environment.ContentRootPath, "kinetix-texts.json"))));

var app = builder.Build();
var localization = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures("en", "es")
    .AddSupportedUICultures("en", "es");
localization.RequestCultureProviders =
[
    new CookieRequestCultureProvider(),
    new AcceptLanguageHeaderRequestCultureProvider()
];
app.UseRequestLocalization(localization);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapGet("/culture/{language}", (HttpContext context, string language, string? returnUrl) =>
{
    if (language is not ("en" or "es")) return Results.BadRequest();
    context.Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(language)),
        new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddYears(1),
            IsEssential = true,
            SameSite = SameSiteMode.Lax,
            Secure = context.Request.IsHttps
        });
    var destination = returnUrl is not null && returnUrl.StartsWith('/') && !returnUrl.StartsWith("//")
        ? returnUrl : "/";
    return Results.LocalRedirect(destination);
});
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
