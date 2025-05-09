using Moq;
using NUnit.Framework;
using TmoTask.Interfaces;
using TmoTask.Services;
using TmoTask.DTO;
using TmoTask.DataAccess;

namespace TmoTask.Tests.Services
{
    public class SellerServiceTests
    {
        private Mock<IDataHandler> _mockDataHandler = null!;
        private SellerService _sellerService = null!;

        [SetUp]
        public void Setup()
        {
            _mockDataHandler = new Mock<IDataHandler>();
            _sellerService = new SellerService(_mockDataHandler.Object);
        }

        [Test]
        public async Task GetTopSellersByMonthAsync_NoBranch_Results()
        {
            var testData = new Dictionary<(string, string), (int, double)>
            {
                { ("Seller A", "January"), (10, 1000.52) },
                { ("Seller B", "February"), (5, 500.52) }
            };

            _mockDataHandler.Setup(d => d.GetTopSellersAsync(null))
                .ReturnsAsync(testData);

            var result = await _sellerService.GetTopSellersByMonthAsync();

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Month, Is.EqualTo("January"));
            Assert.That(result.First().Name, Is.EqualTo("Seller A"));
            Assert.That(result.First().TotalOrders, Is.EqualTo(10));
            Assert.That(result.First().TotalPrice, Is.EqualTo(1000.52));
        }

        [Test]
        public async Task GetTopSellersByMonthAsync_WithBranch_Results()
        {
            string branch = "Branch 1";
            _mockDataHandler.Setup(d => d.GetTopSellersAsync(branch))
                .ReturnsAsync(new Dictionary<(string, string), (int, double)>());

            var result = await _sellerService.GetTopSellersByMonthAsync(branch);

            _mockDataHandler.Verify(d => d.GetTopSellersAsync(branch), Times.Once);
        }
    }
}
