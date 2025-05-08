using TmoTask.DTO;
using TmoTask.Interfaces;

namespace TmoTask.Services
{
    public class SellerService:ISellerService
    {
        private readonly IDataHandler _dataHandler;
        public SellerService(IDataHandler dataHandler) {
            _dataHandler = dataHandler;
        }

        public async Task<IEnumerable<TopSellerByMonthDto>> GetTopSellersByMonthAsync(string? branch = null)
        {
            var topSellersByMonth = await _dataHandler.GetTopSellersAsync(branch);
            return topSellersByMonth.Select(x => new TopSellerByMonthDto
            {
                Month = x.Key.Month,
                Name = x.Key.Seller,
                TotalOrders = x.Value.OrdersCount,
                TotalPrice = x.Value.TotalPrice
            }).ToList();
        }
    }
}
