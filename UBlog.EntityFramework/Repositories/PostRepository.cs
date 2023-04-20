using Microsoft.EntityFrameworkCore;
using UBlog.EntityFramework.Models;
using UBlog.EntityFramework.Repositories.Abstract;

namespace UBlog.EntityFramework.Repositories;

public class PostRepository : IPostRepository
{
    private readonly ApplicationContext _context;
    
    public PostRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public IQueryable<Post> GetPosts()
    {
        return _context.Posts;
    }

    public async Task<Post?> Get(Guid id)
    {
        return await _context.Posts.FindAsync(id);
    }

    public async Task<Post[]> GetUserPosts(string userId)
    {
        return await _context.Posts
            .Where(o => o.UserId == userId)
            .ToArrayAsync();
    }

    public async Task<Post[]> GetLikedPosts(string userId)
    {
        return await _context.Likes
            .Where(o => o.UserId == userId)
            .Join(_context.Posts,
                like => like.PostId,
                post => post.Id,
                (like, post) => post)
            .ToArrayAsync();
    }

    public async Task<Post[]> GetFollowingPosts(string userId)
    {
        return await _context.Subscribes
            .Where(o => o.FollowerId == userId)
            .Join(_context.Posts,
                sub => sub.FollowingId,
                post => post.UserId,
                (like, post) => post)
            .ToArrayAsync();
    }

    public Guid Add(Post post)
    {
        _context.Add(post);

        return post.Id;
    }

    public Guid Update(Post post)
    {
        _context.Posts.Add(post);

        return post.Id;
    }

    public bool Remove(Guid id)
    {
        _context.Remove(id);

        return true;
    }
}