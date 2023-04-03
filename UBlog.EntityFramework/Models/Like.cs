using Microsoft.EntityFrameworkCore;

namespace UBlog.EntityFramework.Models;

[Keyless]
public class Like
{
    public Guid UserId { get; init; }
    public Guid PostId { get; init; }
}