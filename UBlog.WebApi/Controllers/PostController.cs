using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UBlog.Core.Models;
using UBlog.Services.Abstract;

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
    [HttpGet("userlikes")]
    public async Task<IList<PostSimple>> GetUserLikesPosts()
    {
        var id = HttpContext.User.GetUsername();
        
        return await _postService.GetLikedPosts(id);
    }

    [Authorize]
    [HttpGet("following")]
    public async Task<IList<PostSimple>> GetFollowingPosts()
    {
        var id = HttpContext.User.GetUsername();

        return await _postService.GetFollowingPosts(id);
    }
    
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IResult> DeletePost(Guid id)
    {
        await _postService.Remove(id);
        
        return Results.Ok();
    }
    
    // =<
    [Authorize]
    [HttpPost]
    public async Task<IResult> PostPost([FromBody] PostSimple post)
    {
        await _postService.Post(post);
        
        return Results.Ok();
    }
    
    [Authorize]
    [HttpPut]
    public async Task<IResult> PutPost([FromBody] PostSimple post)
    {
        await _postService.Update(post);
        
        return Results.Ok();
    }
}