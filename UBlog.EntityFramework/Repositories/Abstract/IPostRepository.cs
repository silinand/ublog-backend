using UBlog.EntityFramework.Models;

namespace UBlog.EntityFramework.Repositories.Abstract;

public interface IPostRepository
{
    IQueryable<Post> GetPosts();

    Task<Post?> Get(Guid id);

    bool Add(Post post);

    bool Update(Post post);

    bool Remove(Guid id);
}