using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using APIPortal.AuthenticationService.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace APIPortal.AuthenticationService.Implements
{
  public class AccessTokenManager : IAccessTokenManager
  {
    private readonly IConfiguration _config;
    public AccessTokenManager(IConfiguration config)
    {
      _config = config;
    }
    public string GenerateToken(IdentityUser user, IList<string> roles)
    {
      var claims = new List<Claim> {
        new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
        new Claim(JwtRegisteredClaimNames.Email, user.Email)
      };

      foreach (var role in roles)
      {
        claims.Add(new Claim(ClaimTypes.Role, role));
      }

      var key = Encoding.ASCII.GetBytes(_config["Token:Key"]);

      var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddDays(10),
        SigningCredentials = signingCredentials
      };

      var tokenHandler = new JwtSecurityTokenHandler();

      SecurityToken accessToken = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(accessToken);
    }
  }
}