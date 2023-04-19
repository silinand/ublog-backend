using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UBlog.Core.Services;
using UBlog.EntityFramework.Repositories;
using UBlog.EntityFramework.Repositories.Abstract;
using UBlog.EntityFramework.Services;

namespace UBlog.EntityFramework;

public static class ServiceExtensions
{
    public static void AddDbSupport(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationContext>(OnConfiguring);
        
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IActionRepository, ActionRepository>();

        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IActionService, ActionService>();
        
        services.AddScoped<IWorkStepper, WorkStepper>();
    }

    private static void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ublogapp.db");
        optionsBuilder.LogTo(Console.WriteLine);
    }
}