using UBlog.Core.Models;

namespace UBlog.Core.Services;

public interface IUserService
{
    Task<IList<UserSimple>> GetUsers();

    Task<UserSimple> Get(string id);

    Task<(int posts, int follower, int following)> GetUserStat(string id);

    Task<IList<string>> GetFollowingUser(string id);
    
    Task<IList<string>> GetFollowedUser(string id);

    Task<bool> Add(UserSimple user);

    Task<bool> Remove(string id);

    Task<bool> Update(UserSimple user);
}