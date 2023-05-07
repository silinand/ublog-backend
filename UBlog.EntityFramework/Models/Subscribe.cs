using Microsoft.EntityFrameworkCore;

namespace UBlog.EntityFramework.Models;

[PrimaryKey(nameof(FollowerId), nameof(FollowingId))]
public class Subscribe
{
    public string FollowerId { get; init; }
    public string FollowingId { get; init; }
    public DateTime CreationTime { get; init; }
}