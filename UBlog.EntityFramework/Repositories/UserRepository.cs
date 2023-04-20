using Microsoft.EntityFrameworkCore;
using UBlog.EntityFramework.Models;
using UBlog.EntityFramework.Repositories.Abstract;

namespace UBlog.EntityFramework.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }
    public IQueryable<User> GetUsers()
    {
        return _context.Users;
    }

    public async Task<User> Get(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<(int posts, int follower, int following)> GetUserStat(string id)
    {
        var posts = await _context.Posts
            .CountAsync(o => o.UserId == id);

        var followers = await _context.Subscribes.CountAsync(o => o.FollowingId == id);
        var following = await _context.Subscribes.CountAsync(o => o.FollowerId == id);

        return (posts, followers, following);
    }

    public async Task<string[]> GetFollowingUser(string id)
    {
        return await _context.Subscribes
            .Where(o => o.FollowerId == id)
            .Select(o => o.FollowingId)
            .ToArrayAsync();
    }

    public async Task<string[]> GetFollowedUser(string id)
    {
        return await _context.Subscribes
            .Where(o => o.FollowingId == id)
            .Select(o => o.FollowerId)
            .ToArrayAsync();
    }

    public string Add(User user)
    {
        _context.Add(user);

        return user.Id;
    }

    public string Update(User user)
    {
        _context.Update(user);

        return user.Id;
    }

    public bool Remove(string id)
    {
        _context.Remove(id);

        return true;
    }
}