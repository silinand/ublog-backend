using Microsoft.EntityFrameworkCore;
using UBlog.EntityFramework.Models;

namespace UBlog.EntityFramework;

internal class ApplicationContext : DbContext
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Subscribe> Subscribes => Set<Subscribe>();
    public DbSet<Like> Likes => Set<Like>();

    public ApplicationContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ublogapp.db");
    }

    public IEnumerable<T> GetEntities<T>()
    {
        if (typeof(T) == typeof(Post))
            return Posts.OfType<T>();
        
        if (typeof(T) == typeof(User))
            return Users.OfType<T>();
        
        if (typeof(T) == typeof(Like))
            return Likes.OfType<T>();
        
        if (typeof(T) == typeof(Subscribe))
            return Subscribes.OfType<T>();
        
        return null;
    }
}