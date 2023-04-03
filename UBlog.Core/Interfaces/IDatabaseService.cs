using UBlog.Core.Models;

namespace UBlog.Core.Interfaces;

public interface IDatabaseService
{
    void Like(Guid userId, Guid postId);

    void Subscribe(Guid followerId, Guid followedId);
    
    void Add(PostSimple simple);

    void Add(UserSimple simple);

    void Remove<T>(Guid id) where T : class, IDatabaseEntity;

    public void Remove<T>(Func<T, bool> func) where T : class;

    void Update<T, T2>(T2 newValue)
        where T : class, IDatabaseEntity, ISimplify<T2>
        where T2 : IDatabaseEntity;
    
    T Find<T>(Guid id) where T : class;

    IEnumerable<T> Where<T>();

    IEnumerable<T> Where<T>(Func<T, bool> action);

}