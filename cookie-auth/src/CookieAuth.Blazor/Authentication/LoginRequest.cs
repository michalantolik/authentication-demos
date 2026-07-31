namespace CookieAuth.Blazor.Authentication;

/// <summary>
/// Represents a login request.
/// </summary>
public sealed class LoginRequest
{
    /// <summary>
    /// The user's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's password.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
