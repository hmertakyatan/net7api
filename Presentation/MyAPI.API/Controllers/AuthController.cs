using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.IdentityModel.Tokens;
using MyAPI.Application.Abstractions;
using MyAPI.Domain.Dto;
using MyAPI.Domain.Entities;
using MyAPI.Infrastructure.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
[OutputCache]
public class AuthController : ControllerBase
{
    public static User user = new User();
    private readonly IConfiguration _configuration;
    

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login(UserDto request)
    {
       if (user.Username != request.Username)
        {
            return BadRequest("Username not exist.");
        }
       if(!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return BadRequest("Wrong password.");
        }
        string token = CreateToken(user);
        return Ok(token);
    }
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(User request)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash);
        user.Id = Guid.NewGuid();
        user.CreatedTime = DateTime.Now;
        user.Name = request.Name;
        user.Username = request.Username;
        user.PasswordHash = passwordHash;
        user.Email = request.Email;
        user.Role = request.Role;
       

        return Ok(user);
    }
    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim (ClaimTypes.Name, user.Name),
            new Claim (ClaimTypes.Name, user.Username),
            new Claim (ClaimTypes.Email, user.Email),
            new Claim (ClaimTypes.Role, user.Role),
            
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials : creds
                );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}
