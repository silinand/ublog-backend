using Microsoft.EntityFrameworkCore;
using UBlog.Core.Models;
using UBlog.EntityFramework;
using UBlog.EntityFramework.Models;
using UBlog.EntityFramework.Repositories.Abstract;
using UBlog.Services.Abstract;

namespace UBlog.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IWorkStepper _stepper;

    public PostService(IPostRepository postRepository, IWorkStepper stepper)
    {
        _postRepository = postRepository;
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

    public async Task<PostSimple[]> GetUserPosts(string userId)
    {
        var posts = await _postRepository.GetUserPosts(userId);

        return Simplify(posts);
    }

    public async Task<PostSimple[]> GetLikedPosts(string userId)
    {
        var posts = await _postRepository.GetLikedPosts(userId);

        return Simplify(posts);
    }

    public async Task<PostSimple[]> GetFollowingPosts(string userId)
    {
        var posts = await _postRepository.GetFollowingPosts(userId);

        return Simplify(posts);
    }

    public async Task<bool> Remove(Guid id)
    {
        _postRepository.Remove(id);
        
        await _stepper.Save();
        
        return true;
    }

    public async Task<bool> Post(PostSimple post)
    {
        var entity = new Post
        {
            Id = post.Id,
            UserId = post.UserId,
            Title = post.Title,
            Text = post.Text,
            Image = post.Image,
            CreationTime = post.CreationTime
        };
        
        _postRepository.Add(entity);
        
        await _stepper.Save();
        
        return true;
    }

    public async Task<bool> Update(PostSimple post)
    {
        var entity = await _postRepository.Get(post.Id);
        
        _postRepository.Update(entity);
        
        await _stepper.Save();
        
        return true;
    }

    private static PostSimple[] Simplify(IEnumerable<Post> posts)
    {
        return posts.Select(o => o.Simplify())
            .ToArray();
    }
}