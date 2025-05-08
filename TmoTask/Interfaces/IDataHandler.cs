using TmoTask.DTO;

namespace TmoTask.Interfaces
{
    public interface IDataHandler
    {
        Task<List<string>> GetBranchesAsync();
        Task<Dictionary<(string Seller, string Month), (int OrdersCount, double TotalPrice)>> GetTopSellersAsync(string? branch = null);
    }
}
