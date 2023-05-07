using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace UBlog;

public static class AuthOptions
{
    public const string Issuer = "http://localhost:5196/";
    public const string Audience = "http://localhost:3000/";
    private const string Key = "secretpassword123";

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
        => new(Encoding.UTF8.GetBytes(Key));
}