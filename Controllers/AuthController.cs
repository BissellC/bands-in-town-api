using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using bands_in_town_api.Models;
using bands_in_town_api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bands_in_town_api.Services;
using Microsoft.Extensions.Configuration;

namespace bands_in_town_api.Controllers
{
  [Route("auth")]
  [ApiController]
  public class AuthController : ControllerBase
  {



    private readonly DatabaseContext _context;

    private readonly IConfiguration configuration;

    public AuthController(DatabaseContext context, IConfiguration config)
    {
      this._context = context;
      this.configuration = config;
    }

    [HttpPost("signup")]
    public async Task<ActionResult> SignUpUser(NewUserModel userData)
    {


      var existingUser = await this._context.Users.FirstOrDefaultAsync(f => f.Username == userData.Username);
      if (existingUser != null)
      {
        return BadRequest(new { Message = "user already exists" });
      }



      var user = new User
      {
        State = userData.State,
        Username = userData.Username,
        HashedPassword = ""
      };
      // hash the password

      var hashed = new PasswordHasher<User>().HashPassword(user, userData.Password);
      user.HashedPassword = hashed;

      this._context.Users.Add(user);
      await this._context.SaveChangesAsync();
      var rv = new AuthService(this.configuration).CreateToken(user);
      return Ok(rv);
    }


    [HttpPost("login")]
    public async Task<ActionResult> LoginUser(LoginViewModel loginData)
    {
      var user = await this._context.Users.FirstOrDefaultAsync(f => f.Username == loginData.Username);
      if (user == null)
      {
        return BadRequest(new { Message = "User does not exist" });
      }

      var verificationResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.HashedPassword, loginData.Password);

      if (verificationResult == PasswordVerificationResult.Success)
      {
        var rv = new AuthService(this.configuration).CreateToken(user);
        return Ok(rv);
      }
      else
      {
        return BadRequest(new { message = "Wrong password" });
      }
    }

  }
}
