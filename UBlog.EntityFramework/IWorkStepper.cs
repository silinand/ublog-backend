namespace UBlog.EntityFramework;

public interface IWorkStepper
{
    Task<int> Save();
}