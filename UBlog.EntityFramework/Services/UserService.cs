using Microsoft.EntityFrameworkCore;
using UBlog.Core.Models;
using UBlog.Core.Services;
using UBlog.EntityFramework.Models;
using UBlog.EntityFramework.Repositories.Abstract;

namespace UBlog.EntityFramework.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IActionRepository _actionRepository;
    private readonly IWorkStepper _stepper;

    public UserService(IUserRepository userRepository, IActionRepository actionRepository, IWorkStepper stepper)
    {
        _userRepository = userRepository;
        _actionRepository = actionRepository;
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
        var item = await _userRepository.GetUsers()
            .Include(o => o.Posts)
            .FirstOrDefaultAsync(o => o.Id.Equals(id));

        var followers = _actionRepository.GetSubs().Count(o => o.FollowingId.Equals(id));
        var followings = _actionRepository.GetSubs().Count(o => o.FollowerId.Equals(id));

        return (item.Posts.Count, followers, followings);
    }

    public async Task<IList<string>> GetFollowingUser(string id)
    {
        return await _actionRepository.GetSubs()
            .Where(o => o.FollowerId.Equals(id))
            .Select(o => o.FollowingId)
            .ToListAsync();
    }

    public async Task<IList<string>> GetFollowedUser(string id)
    {
        return await _actionRepository.GetSubs()
            .Where(o => o.FollowingId.Equals(id))
            .Select(o => o.FollowerId)
            .ToListAsync();
    }

    public async Task<bool> Add(UserSimple user)
    {
        var entity = new User(user);
        _userRepository.Add(entity);

        await _stepper.Save();

        return true;
    }

    public async Task<bool> Remove(string id)
    {
        _userRepository.Remove(id);

        await _stepper.Save();

        return true;
    }

    public Task<bool> Update(UserSimple user)
    {
        throw new NotImplementedException();
    }
}