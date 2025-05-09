using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Configuration;
using TmoTask.DataAccess;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace TmoTask.Tests
{
    public class DataHandlerTests
    {

        [Test]
        public void GetCsvReader_WhenPathIsInvalid_ThrowsException()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c["DataSourcePath"]).Returns("dummy.csv");

            var dataHandler = new DataHandler(configMock.Object);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await dataHandler.GetBranchesAsync());
            Assert.That(ex!.Message, Does.Contain("Could not find the file"));
        }
    }
}
