using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorRoleBasedAuthorizationWASM;

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
public class AuthStateProvider : AuthenticationStateProvider
{
    private Task<AuthenticationState> _authStateTask = 
        Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
    public void LoginAsGuest()
    {
        var user = new LoggedInUser(1, "Abhay Guest", Constants.Roles.Guest);

        var claimsIdentity = new ClaimsIdentity(user.ToClaims(), Constants.AuthType);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        _authStateTask = Task.FromResult(new AuthenticationState(claimsPrincipal));
        NotifyAuthenticationStateChanged(_authStateTask);
    }
    public void LoginAsAdmin()
    {
        var user = new LoggedInUser(1, "Abhay Prince", Constants.Roles.Admin);

        var claimsIdentity = new ClaimsIdentity(user.ToClaims(), Constants.AuthType);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        _authStateTask = Task.FromResult(new AuthenticationState(claimsPrincipal));
        NotifyAuthenticationStateChanged(_authStateTask);
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync() => _authStateTask;
}
