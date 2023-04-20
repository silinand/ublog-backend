using UBlog.Core.Models;

namespace UBlog.Services.Abstract;

public interface IPostService
{
    Task<IList<PostSimple>> GetPosts();
    
    Task<PostSimple> GetPost(Guid id);

    Task<PostSimple[]> GetUserPosts(string userId);

    Task<PostSimple[]> GetLikedPosts(string userId);

    Task<PostSimple[]> GetFollowingPosts(string userId);

    Task<bool> Remove(Guid id);

    Task<bool> Post(PostSimple post);

    Task<bool> Update(PostSimple post);
}