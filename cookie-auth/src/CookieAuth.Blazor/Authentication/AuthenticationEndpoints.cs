using Microsoft.AspNetCore.Mvc;

namespace CookieAuth.Blazor.Authentication;

/// <summary>
/// Registers authentication endpoints.
/// </summary>
public static class AuthenticationEndpoints
{
    /// <summary>
    /// Adds authentication endpoints to the application.
    /// </summary>
    public static IEndpointRouteBuilder MapAuthenticationEndpoints(
        this IEndpointRouteBuilder app)
    {
        // Handles login requests sent with the POST method.
        app.MapPost("/authentication/login", Login);

        // Handles logout requests sent with the POST method.
        app.MapPost("/authentication/logout", Logout);

        return app;
    }

    /// <summary>
    /// Handles a user login request.
    /// </summary>
    private static IResult Login([FromForm] LoginRequest request)
    {
        if (request.Email != "admin@admin.com" || request.Password != "123")
        {
            return Results.Unauthorized();
        }

        return Results.Redirect("/private");
    }

    /// <summary>
    /// Handles a user logout request.
    /// </summary>
    private static IResult Logout()
    {
        return Results.Redirect("/");
    }
}
