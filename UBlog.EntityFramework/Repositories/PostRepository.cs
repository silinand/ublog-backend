using UBlog.EntityFramework.Models;
using UBlog.EntityFramework.Repositories.Abstract;

namespace UBlog.EntityFramework.Repositories;

public class PostRepository : IPostRepository
{
    private readonly ApplicationContext _context;
    
    public PostRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public IQueryable<Post> GetPosts()
    {
        return _context.Posts;
    }

    public async Task<Post?> Get(Guid id)
    {
        return await _context.Posts.FindAsync(id);
    }

    public bool Add(Post post)
    {
        _context.Add(post);

        return true;
    }

    public bool Update(Post post)
    {
        throw new NotImplementedException();
    }

    public bool Remove(Guid id)
    {
        _context.Remove(id);

        return true;
    }
}