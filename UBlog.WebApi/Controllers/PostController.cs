using Microsoft.AspNetCore.Mvc;
using UBlog.Core.Interfaces;
using UBlog.Core.Models;
using UBlog.EntityFramework.Models;

namespace UBlog.Controllers;

[ApiController]
[Route("posts")]
public class PostController : ControllerBase
{
    private readonly IDatabaseService _dbService; 
        
    public PostController(IDatabaseService service)
    {
        _dbService = service;
    }
    
    [HttpGet]
    public PostSimple[] GetPosts()
    {
        return _dbService.Where<Post>()
            .Select(o => o.Simplify())
            .ToArray();
    }

    [HttpGet("{id}")]
    public PostSimple GetPost(Guid id)
    {
        return _dbService.Find<Post>(id)
            .Simplify();
    }
    
    [HttpGet("user/{id}")]
    public PostSimple[] GetUserPosts(Guid id)
    {
        return _dbService.Where<Post>(o => o.UserId.Equals(id))
            .Select(o => o.Simplify())
            .ToArray();
    }

    [HttpGet("userlikes/{id}")]
    public PostSimple[] GetUserLikesPosts(Guid id)
    {
        var likes = _dbService.Where<Like>(o => o.UserId.Equals(id))
            .Select(o => o.PostId)
            .ToArray();

        return _dbService.Where<Post>(o => o.Id.Equals(id))
            .Select(o => o.Simplify())
            .ToArray();
    }

    [HttpGet("followed")]
    public PostSimple[] GetFollowedPosts()
    {
        var id = Guid.NewGuid();//get from authorization
        
        var subscribes = _dbService.Where<Subscribe>(o => o.FollowerId.Equals(id))
            .Select(o => o.FollowedId)
            .ToArray();

        return _dbService.Where<User>(o => subscribes.Contains(o.Id))
            .SelectMany(o => GetUserPosts(o.Id))
            .ToArray();
    }
    
    
    [HttpDelete("{id}")]
    public IResult DeletePost(Guid id)
    {
        _dbService.Remove<Post>(id);
        return Results.Ok();
    }
    
    // =<
    [HttpPost]
    public IResult PostPost([FromBody] PostSimple post)
    {
        _dbService.Add(post);
        return Results.Ok();
    }
    
    [HttpPut]
    public IResult PutPost([FromBody] PostSimple post)
    {
        _dbService.Update<Post, PostSimple>(post);
        return Results.Ok();
    }
}