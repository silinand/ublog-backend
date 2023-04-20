using UBlog.EntityFramework.Models;

namespace UBlog.EntityFramework.Repositories.Abstract;

public interface IUserRepository
{
    IQueryable<User> GetUsers();

    Task<User> Get(string id);
    
    Task<(int posts, int follower, int following)> GetUserStat(string id);

    Task<string[]> GetFollowingUser(string id);
    
    Task<string[]> GetFollowedUser(string id);

    string Add(User user);

    string Update(User user);

    bool Remove(string id);
}