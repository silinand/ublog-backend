using UBlog.Core.Interfaces;
using UBlog.Core.Models;
using UBlog.EntityFramework.Models;

namespace UBlog.EntityFramework;

public class DatabaseService : IDatabaseService
{
    public void Like(Guid userId, Guid postId)
    {
        var like = new Like()
        {
            UserId = userId,
            PostId = postId
        };
        
        Add(like);
    }

    public void Subscribe(Guid followerId, Guid followedId)
    {
        var sub = new Subscribe(followerId, followedId, DateTime.Now);
        Add(sub);
    }
    
    public void Add(PostSimple simple)
    {
        var post = new Post(simple);
        Add(post);
    }

    public void Add(UserSimple simple)
    {
        var user = new User(simple);
        Add(user);
    }

    #region generic

    public void Add(object item)
    {
        using (var context = new ApplicationContext())
        {
            context.Add(item);
            context.SaveChanges();
        }
    }
    
    public void Remove<T>(Guid id)
        where T : class, IDatabaseEntity
    {
        using (var context = new ApplicationContext())
        {
            var item = context.Find<T>(id);

            context.Remove(item);
            context.SaveChanges();
        }
    }

    public void Remove<T>(Func<T, bool> func)
        where T : class
    {
        using (var context = new ApplicationContext())
        {
            var items = context.GetEntities<T>()
                .Where(func)
                .ToArray();

            context.RemoveRange(items);
            context.SaveChanges();
        }
    }

    public void Update<T, T2>(T2 newValue)
        where T : class, IDatabaseEntity, ISimplify<T2>
        where T2 : IDatabaseEntity
    {
        using (var context = new ApplicationContext())
        {
            var oldValue = context.Find<T>(newValue.Id);

            oldValue.Update(newValue);
            context.SaveChanges();
        } 
    }
    
    public T Find<T>(Guid id)
        where T : class
    {
        using (var context = new ApplicationContext())
        {
            return context.Find<T>(id);
        }
    }

    public IEnumerable<T> Where<T>() => Where<T>(o => true);
    
    public IEnumerable<T> Where<T>(Func<T, bool> action)
    {
        using (var context = new ApplicationContext())
        {
            return context.GetEntities<T>().Where(action).ToArray();
        }
    }
    
    #endregion
}