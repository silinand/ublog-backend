using UBlog.EntityFramework.Models;

namespace UBlog.EntityFramework.Repositories.Abstract;

public interface IPostRepository
{
    IQueryable<Post> GetPosts();

    Task<Post?> Get(Guid id);
    
    Task<Post[]> GetUserPosts(string userId);

    Task<Post[]> GetLikedPosts(string userId);

    Task<Post[]> GetFollowingPosts(string userId);

    Guid Add(Post post);

    Guid Update(Post post);

    bool Remove(Guid id);
}