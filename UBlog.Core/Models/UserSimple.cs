using UBlog.Core.Interfaces;

namespace UBlog.Core.Models;

public class UserSimple : IDatabaseEntity
{
    public Guid Id { get; init; }
    public string Username { get; init; }
    public string Email { get; init; }
    public string Name { get; init; }
    public string Bio { get; init; }
    // ?
    //public byte[] Password { get; set; }
}