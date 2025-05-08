namespace TmoTask.Interfaces
{
    public interface IBranchService
    {
        Task<IEnumerable<string>> GetBranchesAsync();
    }
}
