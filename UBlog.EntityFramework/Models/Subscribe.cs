using Microsoft.EntityFrameworkCore;

namespace UBlog.EntityFramework.Models;

[Keyless]
public class Subscribe
{
    public string FollowerId { get; init; }
    public string FollowingId { get; init; }
    public DateTime CreationTime { get; init; }

    public Subscribe(string userId, string followingId, DateTime time)
    {
        FollowerId = userId;
        FollowingId = followingId;
        CreationTime = time;
    }
}