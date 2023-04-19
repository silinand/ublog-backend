using Microsoft.EntityFrameworkCore;
using UBlog.Core.Models;
using UBlog.Core.Services;
using UBlog.EntityFramework.Models;
using UBlog.EntityFramework.Repositories.Abstract;

namespace UBlog.EntityFramework.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IActionRepository _actionRepository;
    private readonly IWorkStepper _stepper;

    public PostService(IPostRepository postRepository, IActionRepository actionRepository, IWorkStepper stepper)
    {
        _postRepository = postRepository;
        _actionRepository = actionRepository;
        _stepper = stepper;
    }
    
    public async Task<IList<PostSimple>> GetPosts()
    {
        return await _postRepository.GetPosts()
            .Select(o => o.Simplify())
            .ToListAsync();
    }

    public async Task<PostSimple> GetPost(Guid id)
    {
        var item = await _postRepository.Get(id);
        //throw
        return item.Simplify();
    }

    public async Task<IList<PostSimple>> GetUserPosts(string userId)
    {
        return await _postRepository.GetPosts()
            .Where(o => userId.Equals(o.UserId))
            .Select(o => o.Simplify())
            .ToListAsync();
    }

    public async Task<IList<PostSimple>> GetLikedPosts(string userId)
    {
        return await _actionRepository.GetLikes()
            .Where(o => userId.Equals(o.UserId))
            .Join(_postRepository.GetPosts(),
                like => like.PostId,
                post => post.Id,
                (like, post) => post.Simplify())
            .ToListAsync();
    }

    public async Task<IList<PostSimple>> GetFollowingPosts(string userId)
    {
        return await _actionRepository.GetSubs()
            .Where(o => userId.Equals(o.FollowerId))
            .Join(_postRepository.GetPosts(),
                sub => sub.FollowerId,
                post => post.UserId,
                (sub, post) => post.Simplify())
            .ToListAsync();
    }

    public async Task<bool> Remove(Guid id)
    {
        _postRepository.Remove(id);
        
        await _stepper.Save();
        
        return true;
    }

    public async Task<bool> Post(PostSimple post)
    {
        var entity = new Post(post);
        _postRepository.Update(entity);
        
        await _stepper.Save();
        return true;
    }

    public async Task<bool> Update(PostSimple post)
    {
        // #todo check props
        var entity = new Post(post);
        _postRepository.Update(entity);
        
        await _stepper.Save();
        
        return true;
    }
}