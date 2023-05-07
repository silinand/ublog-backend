using System.Security.Claims;

namespace UBlog;

public static class Extensions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        var id = user.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Sid);
        if (id is null)
        {
            throw new Exception();
        }

        return id.Value;
    }
}