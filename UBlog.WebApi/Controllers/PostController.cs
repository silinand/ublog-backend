using Microsoft.AspNetCore.Mvc;
using UBlog.Core.ContentConstructors;
using UBlog.Core.Models;

namespace UBlog.Controllers;

[ApiController]
[Route("posts")]
public class PostController : ControllerBase
{
    [HttpGet]
    public Post[] GetPosts()
    {
        var posts = new List<Post>();
        for (int i = 0; i < 5; i++)
        {
            posts.Add(PostGenerator.GetPost());
        }
        
        return posts.ToArray();
    }

    [HttpGet("{id}")]
    public Post GetPost(string id)
    {
        return PostGenerator.GetPost();
    }

    [HttpDelete]
    public IResult DeletePost(Post post)
    {
        return Results.Ok();
    }
    
    // =<
    [HttpPost]
    public IResult PostPost(Post post)
    {
        return Results.Ok();
    }
    
    [HttpPut]
    public IResult PutPost(Post post)
    {
        return Results.Ok();
    }
}