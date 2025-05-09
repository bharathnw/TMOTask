using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TmoTask.Controllers;
using TmoTask.Interfaces;

namespace TmoTask.Tests.Controllers
{
    [TestFixture]
    public class BranchControllerTests
    {
        private Mock<IBranchService> _mockBranchService;
        private BranchController _controller;

        [SetUp]
        public void Setup()
        {
            _mockBranchService = new Mock<IBranchService>();
            _controller = new BranchController(_mockBranchService.Object);
        }

        [Test]
        public async Task GetAsync_Returns_Branches()
        {
            var branches = new List<string> { "Branch 1", "Branch 2", "Branch 3" };
            var expected = new List<string> { "Branch 1", "Branch 2", "Branch 3" };
            _mockBranchService.Setup(s => s.GetBranchesAsync()).ReturnsAsync(branches);

            var result = await _controller.GetAsync();

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            CollectionAssert.AreEqual(expected, okResult.Value as IEnumerable<string>);
        }

    }
}