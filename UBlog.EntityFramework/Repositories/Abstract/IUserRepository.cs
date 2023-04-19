using UBlog.EntityFramework.Models;

namespace UBlog.EntityFramework.Repositories.Abstract;

public interface IUserRepository
{
    IQueryable<User> GetUsers();

    Task<User> Get(string id);

    bool Add(User user);

    bool Update(User user);

    bool Remove(string id);
}