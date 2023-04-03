using Microsoft.AspNetCore.Mvc;
using UBlog.Core.Interfaces;
using UBlog.EntityFramework.Models;

namespace UBlog.Controllers;

[ApiController]
public class ActionController : ControllerBase
{
    private IDatabaseService _dbService;
    
    public ActionController(IDatabaseService service)
    {
        _dbService = service;
    }
    
    [HttpPut("like/{id}")]
    public IResult Like(Guid id)
    {
        var userId = Guid.NewGuid();// get from authorization
        _dbService.Like(userId, id);
        return Results.Ok();
    }

    [HttpDelete("like/{id}")]
    public IResult Unlike(Guid id)
    {
        var userId = Guid.NewGuid();// get from authorization
        
        _dbService.Remove<Like>(o => o.PostId.Equals(id) && o.UserId.Equals(userId));
        
        return Results.Ok();
    }
    
    [HttpPut("subs/{id}")]
    public IResult Subscribe(Guid id)
    {
        var userId = Guid.NewGuid();// get from authorization
        
        _dbService.Subscribe(userId, id);
        
        return Results.Ok();
    }

    [HttpDelete("subs/{id}")]
    public IResult Unsubscribe(Guid id)
    {
        var userId = Guid.NewGuid();// get from authorization
        
        _dbService.Remove<Subscribe>(o => o.FollowerId.Equals(userId) && o.FollowedId.Equals(id));
        
        return Results.Ok();
    }
}