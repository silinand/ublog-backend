namespace UBlog.Core.Models.Requests;

public class PostModifyRequest : PostCreationRequest
{
    public Guid Id { get; set; }
}