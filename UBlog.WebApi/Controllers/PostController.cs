using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UBlog.Core.Models;
using UBlog.Core.Services;

namespace UBlog.Controllers;

[ApiController]
[Route("posts")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService; 
        
    public PostController(IPostService service)
    {
        _postService = service;
    }
    
    [HttpGet]
    public async Task<IList<PostSimple>> GetPosts()
    {
        return await _postService.GetPosts();
    }

    [HttpGet("{id}")]
    public async Task<PostSimple> GetPost(Guid id)
    {
        return await _postService.GetPost(id);
    }
    
    [HttpGet("user/{id}")]
    public async Task<IList<PostSimple>> GetUserPosts(string id)
    {
        return await _postService.GetUserPosts(id);
    }

    [Authorize]
    [HttpGet("userlikes/{id}")]
    public Task<IList<PostSimple>> GetUserLikesPosts(string id)
    {
        return _postService.GetLikedPosts(id);
    }

    [Authorize]
    [HttpGet("following")]
    public Task<IList<PostSimple>> GetFollowingPosts()
    {
        var id = HttpContext.User.GetUsername();

        return _postService.GetFollowingPosts(id);
    }
    
    
    [Authorize]
    [HttpDelete("{id}")]
    public IResult DeletePost(Guid id)
    {
        _postService.Remove(id);
        
        return Results.Ok();
    }
    
    // =<
    [Authorize]
    [HttpPost]
    public IResult PostPost([FromBody] PostSimple post)
    {
        _postService.Post(post);
        
        return Results.Ok();
    }
    
    [Authorize]
    [HttpPut]
    public IResult PutPost([FromBody] PostSimple post)
    {
        _postService.Update(post);
        
        return Results.Ok();
    }
}