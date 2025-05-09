using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TmoTask.Controllers;
using TmoTask.DTO;
using TmoTask.Interfaces;

namespace TmoTask.Tests.Controllers
{

    [TestFixture]
    public class PerformanceReportControllerTests
    {
        private Mock<ISellerService> _mockSellerService;
        private PerformanceReportController _controller;

        [SetUp]
        public void Setup()
        {
            _mockSellerService = new Mock<ISellerService>();
            _controller = new PerformanceReportController(_mockSellerService.Object);
        }

        [Test]
        public async Task GetAsync_WithoutBranch_ReturnsOkWithData()
        {
            var dummyReport = new List<TopSellerByMonthDto>
            {
                new TopSellerByMonthDto { Name = "Seller 1", Month = "January", TotalOrders = 10, TotalPrice = 500 }
            };
            _mockSellerService.Setup(s => s.GetTopSellersByMonthAsync()).ReturnsAsync(dummyReport);

            var result = await _controller.GetAsync();

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo(dummyReport));
        }

        [Test]
        public async Task GetAsync_WithValidBranch_ReturnsOkWithData()
        {
            var dummyReport = new List<TopSellerByMonthDto>
            {
                new TopSellerByMonthDto { Name = "Seller 1", Month = "January", TotalOrders = 15, TotalPrice = 750 }
            };
            _mockSellerService.Setup(s => s.GetTopSellersByMonthAsync("Branch 1")).ReturnsAsync(dummyReport);

            var result = await _controller.GetAsync("Branch 1");

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo(dummyReport));
        }

        [TestCase("")]
        [TestCase("   ")]
        public async Task GetAsync_WithInvalidBranch_ReturnsBadRequest(string branch)
        {
            var result = await _controller.GetAsync(branch);

            var badRequest = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.That(badRequest.StatusCode, Is.EqualTo(400));
            Assert.That(badRequest.Value, Is.EqualTo("Branch must not be null or empty"));
        }
    }
}
