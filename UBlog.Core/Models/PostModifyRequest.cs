namespace UBlog.Core.Models;

public class PostModifyRequest : PostCreationRequest
{
    public Guid Id { get; set; }
}