using Microsoft.EntityFrameworkCore;
using UBlog.Core.Services;
using UBlog.EntityFramework.Models;
using UBlog.EntityFramework.Repositories.Abstract;

namespace UBlog.EntityFramework.Services;

public class ActionService : IActionService
{
    private readonly IActionRepository _actionRepository;
    private readonly IWorkStepper _stepper;

    public ActionService(IActionRepository repository, IWorkStepper stepper)
    {
        _actionRepository = repository;
        _stepper = stepper;
    }
    
    public async Task<bool> Like(string userId, Guid contentId)
    {
        var like = new Like(userId, contentId);
        _actionRepository.Add(like);

        await _stepper.Save();

        return true;
    }

    public async Task<bool> Unlike(string userId, Guid contentId)
    {
        var item = await _actionRepository.GetLikes()
            .FirstOrDefaultAsync(o => o.PostId.Equals(contentId) && o.UserId.Equals(userId));
        
        //throw

        _actionRepository.Remove(item);
        await _stepper.Save();

        return true;
    }

    public async Task<bool> Subscribe(string userId, string followingId)
    {
        var sub = new Subscribe(userId, followingId, DateTime.Now);
        _actionRepository.Add(sub);

        await _stepper.Save();

        return true;
    }

    public async Task<bool> Unsubscribe(string userId, string followingId)
    {
        var item = await _actionRepository.GetSubs()
            .FirstOrDefaultAsync(o => o.FollowerId.Equals(userId) && o.FollowingId.Equals(followingId));
        
        //throw

        _actionRepository.Remove(item);
        await _stepper.Save();

        return true;
    }
}