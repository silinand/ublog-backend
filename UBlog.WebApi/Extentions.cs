using System.Security.Claims;

namespace UBlog;

public static class Extentions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        var id = user.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Name);
        if (id is null)
        {
            throw new Exception();
        }

        return id.Value;
    }
}