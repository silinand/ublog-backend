using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UBlog.Core.Services;

namespace UBlog.Controllers;

[Authorize]
[ApiController]
public class ActionController : ControllerBase
{
    private readonly IActionService _actionService;
    
    public ActionController(IActionService service)
    {
        _actionService = service;
    }
    
    [HttpPut("like/{id}")]
    public IResult Like(Guid id)
    {
        var userId = HttpContext.User.GetUsername();
        
        _actionService.Like(userId, id);
        
        return Results.Ok();
    }

    [HttpDelete("like/{id}")]
    public IResult Unlike(Guid id)
    {
        var userId = HttpContext.User.GetUsername();
        
        _actionService.Unlike(userId, id);
        
        return Results.Ok();
    }
    
    [HttpPut("subs/{id}")]
    public IResult Subscribe(string id)
    {
        var userId = HttpContext.User.GetUsername();
        
        _actionService.Subscribe(userId, id);
        
        return Results.Ok();
    }

    [HttpDelete("subs/{id}")]
    public IResult Unsubscribe(string id)
    {
        var userId = HttpContext.User.GetUsername();
        
        _actionService.Unsubscribe(userId, id);
        
        return Results.Ok();
    }
}