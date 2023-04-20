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
    private readonly IWorkStepper _stepper;

    public UserService(IUserRepository userRepository, IWorkStepper stepper)
    {
        _userRepository = userRepository;
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
        var item = await _userRepository.Get(id);
        //throw
        return item.Simplify();
    }

    public async Task<(int posts, int follower, int following)> GetUserStat(string id)
    {
        return await _userRepository.GetUserStat(id);
    }

    public async Task<string[]> GetFollowingUser(string id)
    {
        return await _userRepository.GetFollowingUser(id);
    }

    public async Task<string[]> GetFollowedUser(string id)
    {
        return await _userRepository.GetFollowedUser(id);
    }

    public async Task<string> Add(UserSimple user)
    {
        var entity = new User
        {
            Email = user.Email,
            Name = user.Name,
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