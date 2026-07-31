using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        app.MapPost("/authentication/logout", (Delegate)Logout);

        return app;
    }

    /// <summary>
    /// Handles a user login request.
    /// </summary>
    private static async Task<IResult> Login(
        HttpContext httpContext,
        [FromForm] LoginRequest request)
    {
        if (request.Email != "admin@admin.com" || request.Password != "123")
        {
            return Results.Unauthorized();
        }

        // Creates a collection of claims describing
        // the authenticated user.
        var claims = new List<Claim>();

        // Adds the user's unique identifier.
        claims.Add(
            new Claim(
                ClaimTypes.NameIdentifier,
                Guid.NewGuid().ToString()));

        // Adds the user's display name.
        claims.Add(
            new Claim(
                ClaimTypes.Name,
                request.Email));

        // Creates the user's identity and associates it
        // with the Cookies authentication scheme.
        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        // Creates the authenticated user from the identity.
        // A principal can contain one or more identities.
        var principal = new ClaimsPrincipal(identity);

        // Serializes and encrypts the ClaimsPrincipal,
        // creates the authentication cookie and sends it
        // to the browser in the response.
        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        return Results.Redirect("/private");
    }

    /// <summary>
    /// Handles a user logout request.
    /// </summary>
    private static async Task<IResult> Logout(
        HttpContext httpContext)
    {
        // Removes the authentication cookie and signs
        // the current user out of the application.
        await httpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return Results.Redirect("/");
    }
}
