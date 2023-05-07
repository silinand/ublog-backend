namespace UBlog.Core.Models;

public class UserCreationRequest
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string Bio { get; set; }
    
    public string Image { get; set; }
}