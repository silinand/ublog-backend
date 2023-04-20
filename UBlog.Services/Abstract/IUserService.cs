using UBlog.Core.Models;

namespace UBlog.Services.Abstract;

public interface IUserService
{
    Task<IList<UserSimple>> GetUsers();

    Task<UserSimple> Get(string id);

    Task<(int posts, int follower, int following)> GetUserStat(string id);

    Task<string[]> GetFollowingUser(string id);
    
    Task<string[]> GetFollowedUser(string id);

    Task<string> Add(UserSimple user);

    Task<string> Update(UserSimple user);

    Task<bool> Remove(string id);
}