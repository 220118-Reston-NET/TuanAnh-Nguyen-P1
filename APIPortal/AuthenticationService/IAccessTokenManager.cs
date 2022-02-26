using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace APIPortal.AuthenticationService.Interfaces
{
  public interface IAccessTokenManager
  {
    string GenerateToken(IdentityUser user, IList<string> roles);
    Task<bool> IsCurrentActiveToken();
    Task<bool> IsActiveAsync(string token);
  }
}