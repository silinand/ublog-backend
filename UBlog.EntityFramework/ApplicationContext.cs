using Microsoft.EntityFrameworkCore;
using UBlog.EntityFramework.Models;

namespace UBlog.EntityFramework;

public class ApplicationContext : DbContext
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Subscribe> Subscribes => Set<Subscribe>();
    public DbSet<Like> Likes => Set<Like>();

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var user = new User()
        {
            Id = "admin",
            Bio = "Bio",
            Email = "admin@gmail.com",
            Name = "User Admin"
        };
        
         modelBuilder.Entity<User>().HasData(user);
        
    }
}