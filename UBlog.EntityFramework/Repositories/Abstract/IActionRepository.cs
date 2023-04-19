using UBlog.EntityFramework.Models;

namespace UBlog.EntityFramework.Repositories.Abstract;

public interface IActionRepository
{
    IQueryable<Like> GetLikes();
    
    IQueryable<Subscribe> GetSubs();

    bool Add(Subscribe sub);
    
    bool Add(Like like);
    
    bool Remove(Subscribe sub);
    
    bool Remove(Like like);
}