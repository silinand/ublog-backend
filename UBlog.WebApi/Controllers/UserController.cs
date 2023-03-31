using Microsoft.AspNetCore.Mvc;
using UBlog.Core.Models;

namespace UBlog.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    [HttpGet]
    public IEnumerable<User> GetUsers()
    {
        var users = new List<User>();
        for (int i = 0; i < 5; i++)
        {
            users.Add(Core.Models.User.GetOne());
        }
        
        return users.ToArray();
    }

    [HttpGet("{id}")]
    public User GetUser(string id)
    {
        return Core.Models.User.GetOne();
    }

    [HttpPost]
    public IResult AddUser(User user)
    {
        return Results.Ok();
    }

    [HttpDelete]
    public IResult DeleteUser(string id)
    {
        return Results.Ok();
    }

    [HttpPut]
    public IResult PutUser(User user)
    {
        return Results.Ok();
    }
}