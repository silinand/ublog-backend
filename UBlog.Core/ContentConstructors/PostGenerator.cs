using System.Dynamic;
using System.Text.RegularExpressions;
using UBlog.Core.Models;

namespace UBlog.Core.ContentConstructors;

public static class PostGenerator
{
    private static int _counter = 0;

    public static PostSimple GetPost()
    {
        return new PostSimple()
        {
            Id = Guid.NewGuid(),
            Title = "Post title",
            Text = GetText(),
            CreationTime = DateTime.Now
        };
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
    
    public static byte[] SaveDataUrlToFile(string dataUrl, string savePath)
    {
        var matchGroups = Regex.Match(dataUrl, @"^data:((?<type>[\w\/]+))?;base64,(?<data>.+)$").Groups;
        var base64Data = matchGroups["data"].Value;
        return Convert.FromBase64String(base64Data);
    }
}