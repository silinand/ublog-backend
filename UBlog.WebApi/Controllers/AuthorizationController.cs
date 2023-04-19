using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using UBlog.Core.Models;
using UBlog.Core.Services;

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
    public async Task<IResult> Login(LoginRequest request)
    {
        var user = await _userService.Get(request.Username);

        // #todo change nullable
        if (user is null || request.Password is null)
            return Results.Unauthorized();

        var claims = new List<Claim> { new (ClaimTypes.Name, user.Id) };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        return Results.Ok(user.Id);
    }

    [HttpGet("logout")]
    public async Task<IResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Results.Ok();
    }
}