using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace APIPortal.AuthenticationService.Interfaces
{
  public interface IAccessTokenManager
  {
    string GenerateToken(IdentityUser user, IList<string> roles);
  }
}