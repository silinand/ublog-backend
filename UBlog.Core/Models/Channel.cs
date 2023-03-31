namespace UBlog.Core.Models;

public class Channel
{
    public Guid Id { get; set; }
    public Guid IdUser { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
}