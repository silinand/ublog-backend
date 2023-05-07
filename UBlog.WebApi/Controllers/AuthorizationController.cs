using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UBlog.Core.Models;
using UBlog.Core.Models.Requests;
using UBlog.Services.Abstract;

namespace UBlog.Controllers;

[ApiController]
public class AuthorizationController : ControllerBase
{
    private readonly IUserService _userService;
    
    public AuthorizationController(IUserService service)
    {
        _userService = service;
    }
    
    [HttpPost("login")]
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await _userService.Get(request.Username);

        var claims = new List<Claim>
        {
            new (ClaimTypes.Sid, user.Id)
        };
        
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new LoginResponse
        {
            Id = user.Id,
            Image = user.ImageUrl,
            Token = new JwtSecurityTokenHandler().WriteToken(jwt)
        };
    }
}