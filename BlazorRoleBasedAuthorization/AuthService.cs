using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace BlazorRoleBasedAuthorization;

public record LoggedInUser(int Id, string Name, string Role)
{
    public Claim[] ToClaims() =>
        [
        new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
        new Claim(ClaimTypes.Name, Name),
        new Claim(ClaimTypes.Role, Role),
        ];
}
public class Constants
{
    public class Roles
    {
        public const string Admin = "Admin";
        public const string Guest = "Guest";
    }
    public const string AuthType = "ap-auth";
}
public class AuthService
{
    public async Task LoginAsGuest(HttpContext httpContext)
    {
        var user = new LoggedInUser(1, "Abhay Guest", Constants.Roles.Guest);

        var claimsIdentity = new ClaimsIdentity(user.ToClaims(), Constants.AuthType);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        await httpContext.SignInAsync(claimsPrincipal);
    }
    public async Task LoginAsAdmin(HttpContext httpContext)
    {
        var user = new LoggedInUser(1, "Abhay Prince", Constants.Roles.Admin);

        var claimsIdentity = new ClaimsIdentity(user.ToClaims(), Constants.AuthType);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        await httpContext.SignInAsync(claimsPrincipal);
    }
}
