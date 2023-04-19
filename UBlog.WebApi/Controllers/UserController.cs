using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UBlog.Core.Models;
using UBlog.Core.Services;

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
    
    [HttpGet("stat/{id}")]
    public async Task<int[]> GetUserStat(string id)
    {
        var stat = await _userService.GetUserStat(id);

        return new[] { stat.posts, stat.follower, stat.following };
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
    public async Task<IResult> AddUser([FromBody] UserSimple user)
    {
        await _userService.Add(user);
        
        return Results.Ok();
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IResult> DeleteUser(string id)
    {
        await _userService.Remove(id);
        
        return Results.Ok();
    }
    
    [Authorize]
    [HttpPut]
    public async Task<IResult> PutUser([FromBody] UserSimple user)
    {
        await _userService.Update(user);
        
        return Results.Ok();
    }
}