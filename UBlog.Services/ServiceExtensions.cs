using Microsoft.Extensions.DependencyInjection;
using UBlog.Core.Models;
using UBlog.Core.Models.Requests;
using UBlog.EntityFramework.Models;
using UBlog.EntityFramework.Repositories.Abstract;
using UBlog.Services.Abstract;

namespace UBlog.Services;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IActionService, ActionService>();
    }

    #region Model extensions

    public static void Update(this User entity, UserUpdateRequest user)
    {
        entity.Email = user.Email;
        entity.Name = user.Name;
        entity.Bio = user.Bio;
    }
    
    public static void Update(this Post entity, PostSimple post)
    {
        entity.Title = post.Title;
        entity.Text = post.Text;
        entity.CreationTime = post.CreationTime;
    }

    public static UserSimple Simplify(this User user) => new()
    {
        Id = user.Id,
        Email = user.Email,
        Name = user.Name,
        Bio = user.Bio,
        ImageUrl = user.ImageUrl
    };

    public static PostSimple Simplify(this Post post) => new()
    {
        Id = post.Id,
        UserId = post.UserId,
        Title = post.Title,
        Text = post.Text,
        ImageUrl = post.ImageUrl,
        CreationTime = post.CreationTime
    };

    #endregion
}