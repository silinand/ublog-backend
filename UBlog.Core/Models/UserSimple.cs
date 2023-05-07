namespace UBlog.Core.Models;

public class UserSimple
{
    public string Id { get; init; }
    public string Email { get; init; }

    public string ImageUrl { get; init; }
    public string Name { get; init; }
    public string Bio { get; init; }
    
    public int PostsCount { get; set; }
    
    public int FollowersCount { get; set; }
    
    public int FollowingsCount { get; set; }
    // ?
    //public byte[] Password { get; set; }
}