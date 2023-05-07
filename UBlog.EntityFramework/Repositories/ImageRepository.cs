using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using UBlog.EntityFramework.Repositories.Abstract;

namespace UBlog.EntityFramework.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly string _webRoot;

    public ImageRepository(IWebHostEnvironment environment)
    {
        _webRoot = environment.WebRootPath;
    }
    
    public async Task<string> Add(string content)
    {
        var meta = Regex.Match(content, @"data:image/(?<type>.+?);base64,(?<data>.+)");
        var path = Guid.NewGuid() + "." + meta.Groups["type"].Value;
        var data = Convert.FromBase64String(meta.Groups["data"].Value);
        
        await File.WriteAllBytesAsync(Path.Combine(_webRoot, path), data);
        
        return path;
    }

    public void Remove(string path)
    {
        File.Delete(Path.Combine(_webRoot, path));
    }
}