using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UBlog.Core.Models;
using UBlog.Core.Models.Requests;
using UBlog.Services.Abstract;

namespace UBlog.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService service)
    {
        _userService = service;
    }
    
    [HttpGet]
    public async Task<IList<UserSimple>> GetUsers()
    {
        return await _userService.GetUsers();
    }
    
    [HttpGet("{id}")]
    public async Task<UserSimple> GetUser(string id)
    {
        return await _userService.Get(id);
    }

    [HttpGet("following/{id}")]
    public async Task<IList<string>> GetFollowingUsers(string id)
    {
        return await _userService.GetFollowingUser(id);
    }
    
    [HttpGet("follower/{id}")]
    public async Task<IList<string>> GetFollowerUsers(string id)
    {
        return await _userService.GetFollowedUser(id);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<string> AddUser(UserCreationRequest user)
    {
        return await _userService.Add(user);
    }
    
    [Authorize]
    [HttpDelete]
    public async Task<IResult> DeleteUser()
    {
        var id = HttpContext.User.GetUsername();
        
        await _userService.Remove(id);
        
        return Results.Ok();
    }
    
    [Authorize]
    [HttpPut]
    public async Task<IResult> PutUser(UserUpdateRequest user)
    {
        var id = HttpContext.User.GetUsername();
        
        await _userService.Update(id, user);
        
        return Results.Ok();
    }
}