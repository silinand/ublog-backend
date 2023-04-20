using Microsoft.EntityFrameworkCore;

namespace UBlog.EntityFramework.Models;

[Keyless]
public class Like
{
    public string UserId { get; init; }
    public Guid PostId { get; init; }
}