namespace UBlog.Core.Models.Requests;

public class PostCreationRequest
{
    public string Title { get; set; }
    public string Text { get; set; }
    public string Image { get; set; }
}