using TmoTask.DTO;

namespace TmoTask.Interfaces
{
    public interface ISellerService
    {
        Task<IEnumerable<TopSellerByMonthDto>> GetTopSellersByMonthAsync(string? branch = null);
    }
}
