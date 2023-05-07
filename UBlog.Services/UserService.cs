using Microsoft.EntityFrameworkCore;
using UBlog.Core.Models;
using UBlog.EntityFramework;
using UBlog.EntityFramework.Models;
using UBlog.EntityFramework.Repositories.Abstract;
using UBlog.Services.Abstract;

namespace UBlog.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IWorkStepper _stepper;

    public UserService(IUserRepository userRepository, IImageRepository imageRepository, IWorkStepper stepper)
    {
        _userRepository = userRepository;
        _imageRepository = imageRepository;
        _stepper = stepper;
    }
    
    public async Task<IList<UserSimple>> GetUsers()
    {
        return await _userRepository.GetUsers()
            .Select(o => o.Simplify())
            .ToListAsync();
    }

    public async Task<UserSimple> Get(string id)
    {
        var entity = await _userRepository.Get(id);
        var counts = await _userRepository.GetUserStat(id);
        //throw
        var user = entity.Simplify();
        user.PostsCount = counts.posts;
        user.FollowersCount = counts.follower;
        user.FollowingsCount = counts.following;

        return user;
    }

    public async Task<string[]> GetFollowingUser(string id)
    {
        return await _userRepository.GetFollowingUser(id);
    }

    public async Task<string[]> GetFollowedUser(string id)
    {
        return await _userRepository.GetFollowedUser(id);
    }

    public async Task<string> Add(UserCreationRequest user)
    {
        var imageUrl = await _imageRepository.Add(user.Email);
        var entity = new User
        {
            Email = user.Email,
            Name = user.Name,
            ImageUrl = imageUrl,
            Bio = user.Bio
        };
        
        _userRepository.Add(entity);

        await _stepper.Save();

        return entity.Id;
    }

    public async Task<string> Update(UserSimple user)
    {
        var entity = await _userRepository.Get(user.Id);
        entity.Update(user);

        _userRepository.Update(entity);

        await _stepper.Save();

        return entity.Id;
    }

    public async Task<bool> Remove(string id)
    {
        _userRepository.Remove(id);

        await _stepper.Save();

        return true;
    }
}