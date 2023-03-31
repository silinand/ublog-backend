using System.Dynamic;
using UBlog.Core.Models;

namespace UBlog.Core.ContentConstructors;

public static class PostGenerator
{
    private static int _counter = 0;

    public static Post GetPost()
    {
        return new Post("Post title", GetText(), GetImage(), DateTime.Now);
}
    
    private static string GetText()
    {
        var lines = File.ReadLines("./Resources/PostContent.txt").ToList();

        if (_counter >= lines.Count)
            _counter = 0;

        return _counter + (lines[_counter++]);
    }

    private static byte[] GetImage()
    {
        var files = Directory.GetFiles("./Resources", "*.jpg");

        var index = _counter;
        while (index >= files.Length)
        {
            if (index >= files.Length)
                index -= files.Length;
        }

        return File.ReadAllBytes(files[index]);
    }
}