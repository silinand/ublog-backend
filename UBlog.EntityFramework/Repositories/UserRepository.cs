using UBlog.EntityFramework.Models;
using UBlog.EntityFramework.Repositories.Abstract;

namespace UBlog.EntityFramework.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }
    public IQueryable<User> GetUsers()
    {
        return _context.Users;
    }

    public async Task<User> Get(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public bool Add(User user)
    {
        _context.Add(user);

        return true;
    }

    public bool Update(User user)
    {
        _context.Update(user);

        return true;
    }

    public bool Remove(string id)
    {
        _context.Remove(id);

        return true;
    }
}