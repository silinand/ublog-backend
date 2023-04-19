using Microsoft.EntityFrameworkCore;

namespace UBlog.EntityFramework.Models;

[Keyless]
public class Like
{
    public string UserId { get; init; }
    public Guid PostId { get; init; }

    public Like(string userId, Guid postId)
    {
        UserId = userId;
        PostId = postId;
    }
}