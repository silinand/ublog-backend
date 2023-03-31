namespace UBlog.Core.Models;

public class Post
{
    public Guid Id { get; set; }
    public Guid IdUser { get; set; }
    public byte[] Image { get; }
    public string Text { get; }
    
    public string Title { get; }

    public DateTime Date { get; }

    public Post(string title, string text, byte[] image, DateTime date)
    {
        Image = image;
        Text = text;
        Title = title;
        Date = date;
    }
}