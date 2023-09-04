using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentApp.Collection;
using RentApp.Models;
using RentApp.Service;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserService _userService;

    public AuthController(IConfiguration configuration, UserService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel loginModel)
    {
        var user = _userService.Authenticate(loginModel.Username, loginModel.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        var token = GenerateToken(user);

        return Ok(new { token });
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterModel registerModel)
    {
        var user = new People
        {
            UserName = registerModel.Username,
            Password = registerModel.Password
        };

        _userService.Create(user);

        return Ok();
    }

    private string GenerateToken(People user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSecret"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
