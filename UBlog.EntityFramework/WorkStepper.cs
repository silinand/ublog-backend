namespace UBlog.EntityFramework;

public class WorkStepper : IWorkStepper
{
    private readonly ApplicationContext _context;

    public WorkStepper(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<int> Save()
    {
        return await _context.SaveChangesAsync();
    }
}