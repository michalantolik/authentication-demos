using CookieAuth.Blazor.Authentication;
using CookieAuth.Blazor.Components;

namespace CookieAuth.Blazor;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            // Handles unhandled exceptions.
            app.UseExceptionHandler("/Error");

            app.UseHsts();
        }

        // Handles HTTP status code responses.
        app.UseStatusCodePagesWithReExecute(
            "/StatusCode",
            "?code={0}");

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        // Registers authentication endpoints.
        app.MapAuthenticationEndpoints();

        app.Run();
    }
}
