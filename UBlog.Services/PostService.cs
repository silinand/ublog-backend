using Microsoft.EntityFrameworkCore;
using UBlog.Core.Models;
using UBlog.Core.Models.Requests;
using UBlog.EntityFramework;
using UBlog.EntityFramework.Models;
using UBlog.EntityFramework.Repositories.Abstract;
using UBlog.Services.Abstract;

namespace UBlog.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IActionRepository _actionRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IWorkStepper _stepper;

    public PostService(IPostRepository postRepository,
        IActionRepository actionRepository,
        IImageRepository imageRepository,
        IWorkStepper stepper)
    {
        _postRepository = postRepository;
        _actionRepository = actionRepository;
        _imageRepository = imageRepository;
        _stepper = stepper;
    }
    
    public async Task<PostSimple[]> GetPosts()
    {
        var posts = await _postRepository.GetPosts()
            .Select(o => o.Simplify())
            .ToArrayAsync();

        Parallel.ForEach(posts, UpdateLikes);

        return posts;
    }

    public async Task<PostSimple> GetPost(Guid id)
    {
        var entity  = await _postRepository.Get(id);
        var post = entity.Simplify();
        UpdateLikes(post);

        return post;
    }

    private async void UpdateLikes(PostSimple post)
    {
        post.IsLiked = await _actionRepository.GetIsLiked(post.UserId, post.Id);
        post.Likes = await _actionRepository.GetLikeCount(post.Id);
    }

    public async Task<PostSimple[]> GetUserPosts(string userId)
    {
        var posts = await _postRepository.GetUserPosts(userId);

        return posts.Select(o => o.Simplify())
            .ToArray();
    }

    public async Task<PostSimple[]> GetLikedPosts(string userId)
    {
        var posts = await _postRepository.GetLikedPosts(userId);

        return posts.Select(o => o.Simplify())
            .ToArray();
    }

    public async Task<PostSimple[]> GetFollowingPosts(string userId)
    {
        var posts = await _postRepository.GetFollowingPosts(userId);
        
        return posts.Select(o => o.Simplify())
            .ToArray();
    }

    public async Task<bool> Remove(Guid id)
    {
        _postRepository.Remove(id);
        
        await _stepper.Save();
        
        return true;
    }

    public async Task<Guid> Post(PostCreationRequest post)
    {
        var image = await _imageRepository.Add(post.Image);
        var entity = new Post
        {
            // #todo userService
            UserId = "admin",
            Title = post.Title,
            Text = post.Text,
            ImageUrl = image,
            CreationTime = DateTime.UtcNow
        };
        
        _postRepository.Add(entity);
        
        await _stepper.Save();
        
        return entity.Id;
    }

    public async Task<bool> Update(PostModifyRequest post)
    {
        var entity = await _postRepository.Get(post.Id);
        
        _postRepository.Update(entity);
        
        await _stepper.Save();
        
        return true;
    }
}