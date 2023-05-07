using Microsoft.EntityFrameworkCore;
using UBlog.EntityFramework.Models;
using UBlog.EntityFramework.Repositories.Abstract;

namespace UBlog.EntityFramework.Repositories;

public class ActionRepository : IActionRepository
{
    private readonly ApplicationContext _context;

    public ActionRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public IQueryable<Like> GetLikes()
    {
        return _context.Likes;
    }

    public IQueryable<Subscribe> GetSubs()
    {
        return _context.Subscribes;
    }

    public bool AddLike(string userId, Guid contentId)
    {
        var like = new Like
        {
            UserId = userId,
            PostId = contentId
        };

        _context.Add(like);

        return true;
    }

    public bool AddSub(string userId, string followingId)
    {
        var sub = new Subscribe
        {
            FollowerId = userId,
            FollowingId = followingId,
            CreationTime = DateTime.Now
        };

        _context.Add(sub);

        return true;
    }

    public async Task<int> GetLikeCount(Guid contentId)
    {
        return await _context.Likes.CountAsync(o => o.PostId == contentId);
    }

    public async Task<bool> GetIsLiked(string userId, Guid contentId)
    {
        return await _context.Likes.AnyAsync(o => o.PostId == contentId && o.UserId == userId);
    }

    public async Task<bool> RemoveLike(string userId, Guid contentId)
    {
        var entity = await _context.Likes
            .FirstOrDefaultAsync(o => o.PostId.Equals(contentId) && o.UserId.Equals(userId));

        _context.Likes.Remove(entity);

        return true;
    }

    public async Task<bool> RemoveSub(string userId, string followingId)
    {
        var entity = await _context.Subscribes
            .FirstOrDefaultAsync(o => o.FollowerId.Equals(userId) && o.FollowingId.Equals(followingId));

        _context.Subscribes.Remove(entity);

        return true;
    }
}