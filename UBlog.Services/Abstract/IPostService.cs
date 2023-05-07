using UBlog.Core.Models;

namespace UBlog.Services.Abstract;

public interface IPostService
{
    Task<PostSimple[]> GetPosts();
    
    Task<PostSimple> GetPost(Guid id);

    Task<PostSimple[]> GetUserPosts(string userId);

    Task<PostSimple[]> GetLikedPosts(string userId);

    Task<PostSimple[]> GetFollowingPosts(string userId);

    Task<bool> Remove(Guid id);

    Task<Guid> Post(PostCreationRequest post);

    Task<bool> Update(PostModifyRequest post);
}