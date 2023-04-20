using UBlog.EntityFramework;
using UBlog.EntityFramework.Repositories.Abstract;
using UBlog.Services.Abstract;

namespace UBlog.Services;

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
        _actionRepository.AddLike(userId, contentId);

        await _stepper.Save();

        return true;
    }

    public async Task<bool> Unlike(string userId, Guid contentId)
    {
        _actionRepository.RemoveLike(userId, contentId);
        
        await _stepper.Save();

        return true;
    }

    public async Task<bool> Subscribe(string userId, string followingId)
    {
        _actionRepository.AddSub(userId, followingId);

        await _stepper.Save();

        return true;
    }

    public async Task<bool> Unsubscribe(string userId, string followingId)
    {
        _actionRepository.RemoveSub(userId, followingId);
        
        await _stepper.Save();

        return true;
    }
}