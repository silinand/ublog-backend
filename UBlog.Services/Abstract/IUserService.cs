using UBlog.Core.Models;
using UBlog.Core.Models.Requests;

namespace UBlog.Services.Abstract;

public interface IUserService
{
    Task<IList<UserSimple>> GetUsers();

    Task<UserSimple> Get(string id);

    Task<string[]> GetFollowingUser(string id);
    
    Task<string[]> GetFollowedUser(string id);

    Task<string> Add(UserCreationRequest user);

    Task<string> Update(string id, UserUpdateRequest user);

    Task<bool> Remove(string id);
}