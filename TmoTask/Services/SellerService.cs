using TmoTask.DTO;
using TmoTask.Interfaces;

namespace TmoTask.Services
{
    public class SellerService : ISellerService
    {
        private readonly IDataHandler _dataHandler;
        public SellerService(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public async Task<IEnumerable<TopSellerByMonthDto>> GetTopSellersByMonthAsync(string branch)
        {
            var monthOrder = new Dictionary<string, int> {
                { "January", 1 },
                { "February", 2 },
                { "March", 3 },
                { "April", 4 },
                { "May", 5 },
                { "June", 6 },
                { "July", 7 },
                { "August", 8 },
                { "September", 9 },
                { "October", 10 },
                { "November", 11 },
                { "December", 12 }
            };
            var topSellersByMonth = await _dataHandler.GetTopSellersAsync(branch);
            return topSellersByMonth.Select(x => new TopSellerByMonthDto
            {
                Month = x.Key.Month,
                Name = x.Key.Seller,
                TotalOrders = x.Value.OrdersCount,
                TotalPrice = Math.Round(x.Value.TotalPrice, 2)
            }).OrderBy(x => monthOrder[x.Month]).ThenByDescending(x=>x.TotalOrders).ToList();
        }

        public async Task<IEnumerable<TopSellerByMonthDto>> GetTopSellersByMonthAsync()
        {
            var monthOrder = new Dictionary<string, int> {
                { "January", 1 },
                { "February", 2 },
                { "March", 3 },
                { "April", 4 },
                { "May", 5 },
                { "June", 6 },
                { "July", 7 },
                { "August", 8 },
                { "September", 9 },
                { "October", 10 },
                { "November", 11 },
                { "December", 12 }
            };
            var topSellersByMonth = await _dataHandler.GetTopSellersAsync();
            return topSellersByMonth.Select(x => new TopSellerByMonthDto
            {
                Month = x.Key.Month,
                Name = x.Key.Seller,
                TotalOrders = x.Value.OrdersCount,
                TotalPrice = Math.Round(x.Value.TotalPrice, 2)
            }).OrderBy(x => monthOrder[x.Month]).ThenByDescending(x => x.TotalOrders).ToList();
        }
    }
}
