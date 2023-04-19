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

    public bool Add(Subscribe sub)
    {
        _context.Add(sub);

        return true;
    }

    public bool Add(Like like)
    {
        _context.Add(like);

        return true;
    }

    public bool Remove(Subscribe sub)
    {
        _context.Remove(sub);

        return true;
    }

    public bool Remove(Like like)
    {
        _context.Remove(like);

        return true;
    }
}