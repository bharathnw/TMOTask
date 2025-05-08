namespace TmoTask.DTO
{
    public class TopSellerByMonthDto
    {
        public string Name { get; set; }
        public string Month { get; set; }
        public int TotalOrders { get; set; }
        public double TotalPrice { get; set; }
    }
}
