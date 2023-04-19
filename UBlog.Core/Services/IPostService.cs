using UBlog.Core.Models;

namespace UBlog.Core.Services;

public interface IPostService
{
    Task<IList<PostSimple>> GetPosts();
    
    Task<PostSimple> GetPost(Guid id);

    Task<IList<PostSimple>> GetUserPosts(string userId);

    Task<IList<PostSimple>> GetLikedPosts(string userId);

    Task<IList<PostSimple>> GetFollowingPosts(string userId);

    Task<bool> Remove(Guid id);

    Task<bool> Post(PostSimple post);

    Task<bool> Update(PostSimple post);
}