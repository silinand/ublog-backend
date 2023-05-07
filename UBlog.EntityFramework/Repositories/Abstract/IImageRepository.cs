namespace UBlog.EntityFramework.Repositories.Abstract;

public interface IImageRepository
{
    Task<string> Add(string content);
    
    void Remove(string path);
}