namespace UBlog.Services.Abstract;

public interface IActionService
{
    Task<bool> Like(string userId, Guid contentId);

    Task<bool> Unlike(string userId, Guid contentId);

    Task<bool> Subscribe(string userId, string followingId);
    
    Task<bool> Unsubscribe(string userId, string followingId);
}