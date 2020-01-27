using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using bands_in_town_api.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace bands_in_town_api.Services
{
  public class AuthService
  {

    private readonly string KEY;

    public AuthService(IConfiguration configuration)
    {
      KEY = configuration["SECRET"];
    }

    public AuthenticatedData CreateToken(Models.User user)
    {
      var expirationTime = DateTime.UtcNow.AddHours(24);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Username)

      }),
        Expires = expirationTime,
        SigningCredentials = new SigningCredentials(
               new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.KEY)),
              SecurityAlgorithms.HmacSha256Signature
          )
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
      return new AuthenticatedData
      {
        State = user.State,
        Token = token,
        UserId = user.Id,
        Username = user.Username,
        ExpirationTime = expirationTime
      }; ;
    }
  }
}