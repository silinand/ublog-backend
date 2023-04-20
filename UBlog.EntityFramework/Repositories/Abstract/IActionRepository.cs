using UBlog.EntityFramework.Models;

namespace UBlog.EntityFramework.Repositories.Abstract;

public interface IActionRepository
{
    IQueryable<Like> GetLikes();
    
    IQueryable<Subscribe> GetSubs();

    bool AddLike(string userId, Guid contentId);
    
    bool AddSub(string userId, string followingId);
    
    Task<bool> RemoveLike(string userId, Guid contentId);
    
    Task<bool> RemoveSub(string userId, string followingId);
}