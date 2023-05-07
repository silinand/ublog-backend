using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UBlog.EntityFramework.Repositories;
using UBlog.EntityFramework.Repositories.Abstract;

namespace UBlog.EntityFramework;

public static class DataExtensions
{
    public static void AddDbSupport(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationContext>(OnConfiguring);
        
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IActionRepository, ActionRepository>();
        services.AddSingleton<IImageRepository, ImageRepository>();
        
        services.AddScoped<IWorkStepper, WorkStepper>();
    }

    private static void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ublogapp.db");
        optionsBuilder.LogTo(Console.WriteLine);
    }
}