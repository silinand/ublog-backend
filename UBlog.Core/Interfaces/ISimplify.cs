namespace UBlog.Core.Interfaces;

public interface ISimplify<T>
{
    T Simplify();
    
    
    void Update(T newValue);
}