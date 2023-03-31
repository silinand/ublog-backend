using Microsoft.AspNetCore.Mvc;

namespace UBlog.Controllers;

[ApiController]
public class ActionController : ControllerBase
{
    [HttpPut("like")]
    public IResult Like(Guid idContent)
    {
        return Results.Ok();
    }

    [HttpDelete("like")]
    public IResult Unlike(Guid idContent)
    {
        return Results.Ok();
    }
    
    [HttpPut("subs")]
    public IResult Subscribe(Guid idContent)
    {
        return Results.Ok();
    }

    [HttpDelete("subs")]
    public IResult Unsubscribe(Guid idContent)
    {
        return Results.Ok();
    }
}