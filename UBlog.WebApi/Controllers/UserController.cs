using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UBlog.Core.Models;
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
    public async Task<string> AddUser([FromBody] UserCreationRequest user)
    {
        return await _userService.Add(user);
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