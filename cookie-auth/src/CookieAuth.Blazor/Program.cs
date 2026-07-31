using CookieAuth.Blazor.Authentication;
using CookieAuth.Blazor.Components;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CookieAuth.Blazor;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        // Registers the ASP.NET Core authentication services.
        builder.Services
            // Sets Cookies as the default authentication scheme.
            // Operations such as AuthenticateAsync, SignInAsync
            // and SignOutAsync will use this scheme unless another
            // scheme is explicitly provided.
            .AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)

            // Registers the built-in cookie authentication handler.
            // The handler can create an encrypted authentication cookie,
            // validate it on later requests and remove it during sign-out.
            .AddCookie(options =>
            {
                // Defines where the cookie handler redirects the browser
                // when an unauthenticated user requests a protected resource.
                // The original address is included as a return URL.
                options.LoginPath = "/login";

                // Defines the path recognized as the logout page.
                // The actual authentication cookie will be removed later
                // by calling SignOutAsync from the logout endpoint.
                options.LogoutPath = "/logout";
            });

        // Registers authorization services.
        // Authorization decides whether the current user is allowed
        // to access a resource after authentication has identified them.
        builder.Services.AddAuthorization();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            // Handles unhandled exceptions and redirects requests
            // to the application's error page.
            app.UseExceptionHandler("/Error");

            app.UseHsts();
        }

        // Re-executes the request pipeline to display
        // a custom page for HTTP error status codes.
        app.UseStatusCodePagesWithReExecute(
            "/StatusCode",
            "?code={0}");

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        // Authentication and authorization middleware must run
        // before protected endpoints are executed.
        //
        // Authentication reads the cookie from the incoming request,
        // validates it and sets HttpContext.User.
        app.UseAuthentication();

        // Authorization uses HttpContext.User and endpoint metadata,
        // such as [Authorize], to decide whether access is allowed.
        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        // Maps the custom login and logout POST endpoints.
        app.MapAuthenticationEndpoints();

        app.Run();
    }
}
