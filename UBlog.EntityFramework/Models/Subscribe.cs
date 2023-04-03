using Microsoft.EntityFrameworkCore;

namespace UBlog.EntityFramework.Models;

[Keyless]
public class Subscribe
{
    public Guid FollowerId { get; }
    public Guid FollowedId { get; }
    public DateTime CreationTime { get; }

    public Subscribe()
    { }
    
    internal Subscribe(Guid followerId, Guid followedId, DateTime time)
    {
        FollowerId = followerId;
        FollowedId = followedId;
        CreationTime = time;
    }
}