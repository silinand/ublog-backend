using Microsoft.EntityFrameworkCore;

namespace UBlog.EntityFramework.Models;

[PrimaryKey(nameof(UserId), nameof(PostId))]
public class Like
{
    public string UserId { get; set; }
    
    public Guid PostId { get; set; }
}