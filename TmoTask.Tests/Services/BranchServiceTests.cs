using Moq;
using TmoTask.Interfaces;
using TmoTask.Services;

[TestFixture]
public class BranchServiceTests
{
    private Mock<IDataHandler> _mockDataHandler = null!;
    private BranchService _branchService = null!;

    [SetUp]
    public void Setup()
    {
        _mockDataHandler = new Mock<IDataHandler>();
        _branchService = new BranchService(_mockDataHandler.Object);
    }

    [Test]
    public async Task GetBranchesAsync_ReturnsList()
    {
        var branches = new List<string> { "Branch 1", "Branch 2" };
        _mockDataHandler.Setup(d => d.GetBranchesAsync()).ReturnsAsync(branches);

        var result = await _branchService.GetBranchesAsync();

        Assert.That(result.First(), Is.EqualTo("Branch 1"));
        Assert.That(result.Last(), Is.EqualTo("Branch 2"));
    }
}
