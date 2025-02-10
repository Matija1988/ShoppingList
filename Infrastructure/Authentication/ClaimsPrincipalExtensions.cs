using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Authentication;

internal static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        if (principal == null)
            throw new ApplicationException("ClaimsPrincipal is null");

        Console.WriteLine("=== Claims in JWT ===");
        foreach (var claim in principal.Claims)
        {
            Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
        }

        string? userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (string.IsNullOrEmpty(userId))
            throw new ApplicationException("User id is unavailable");


        return Guid.TryParse(userId, out Guid parsedUserId) ?
            parsedUserId :
            throw new ApplicationException("User id is unavailable");
    }
}
