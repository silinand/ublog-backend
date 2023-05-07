namespace UBlog.Core.Models.Requests;

public class UserUpdateRequest
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public string Image { get; set; }
}