using Microsoft.AspNetCore.Mvc;
using UBlog.Core.Interfaces;
using UBlog.Core.Models;
using UBlog.EntityFramework.Models;

namespace UBlog.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private IDatabaseService _dbService;

    public UserController(IDatabaseService service)
    {
        _dbService = service;
    }
    
    [HttpGet]
    public UserSimple[] GetUsers()
    {
        return _dbService.Where<User>()
            .Select(o => o.Simplify())
            .ToArray();
    }
    
    [HttpGet("{id}")]
    public UserSimple GetUser(Guid id)
    {
        return _dbService.Find<User>(id)
            .Simplify();
    }

    [HttpGet("followed/{id}")]
    public UserSimple[] GetFollowedUsers(Guid id)
    {
        return GetFollowingUsers(id, o => o.FollowedId);
    }
    
    [HttpGet("follower/{id}")]
    public UserSimple[] GetFollowerUsers(Guid id)
    {
        return GetFollowingUsers(id, o => o.FollowerId);
    }

    private UserSimple[] GetFollowingUsers(Guid id, Func<Subscribe, Guid> func)
    {
        var user = _dbService.Find<User>(id);

        var ids = _dbService.Where<Subscribe>(o => func(o).Equals(id));
        return _dbService.Where<User>(o => o.Id.Equals(id))
            .Select(o => o.Simplify())
            .ToArray();
    }
    
    [HttpPost]
    public IResult AddUser([FromBody] UserSimple user)
    {
        _dbService.Add(user);
        
        return Results.Ok();
    }
    
    [HttpDelete("{id}")]
    public IResult DeleteUser(Guid id)
    {
        _dbService.Remove<User>(id);
        
        return Results.Ok();
    }
    
    [HttpPut]
    public IResult PutUser([FromBody] UserSimple user)
    {
        _dbService.Update<User, UserSimple>(user);
        
        return Results.Ok();
    }
}