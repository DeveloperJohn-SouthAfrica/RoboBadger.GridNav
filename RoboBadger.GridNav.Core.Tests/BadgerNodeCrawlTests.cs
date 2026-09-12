using Microsoft.Extensions.DependencyInjection;
using RoboBadger.GridNav.Application.ServiceDefinitions;
using RoboBadger.GridNav.Core.Tests.ConfigurationContainer;

namespace RoboBadger.GridNav.Core.Tests;

/// <summary>
/// The badger node crawl tests class
/// </summary>
[TestClass]
[DeploymentItem("SampleDataFixtures", "SampleDataFixtures")]
public class BadgerNodeCrawlTests
{
    /// <summary>
    /// The services
    /// </summary>
    private IServiceCollection _services;
    /// <summary>
    /// The badger swarm processor
    /// </summary>
    private IBadgerSwarmProcessor _badgerSwarmProcessor;
    
    /// <summary>
    /// Initializes this instance
    /// </summary>
    /// <exception cref="FileNotFoundException">Test cannot initialise without test data</exception>
    [TestInitialize]
    public void Initialize()
    {
        _services = new ServiceCollection();
        _services.ConfigureServices();
        var serviceProvider = _services.BuildServiceProvider();
        _badgerSwarmProcessor = serviceProvider.GetRequiredService<IBadgerSwarmProcessor>();
        bool[] fixtureCollection =
        [
            File.Exists(TestDataInput.RoboBadgerGroundControlTransmission),
            File.Exists(TestDataOutput.RoboBadgerMarsBaseTransmission),
            File.Exists(TestDataGridSlice.GridParameters),
            File.Exists(TestDataBadgerInstructionSlices.BadgerOneInstruction),
            File.Exists(TestDataBadgerInstructionSlices.BadgerTwoInstruction),
            File.Exists(TestDataBadgerInstructionSlices.BadgerThreeInstruction),
            File.Exists(TestDataBadgerPositionSlices.BadgerOnePosition),
            File.Exists(TestDataBadgerPositionSlices.BadgerTwoPosition),
            File.Exists(TestDataBadgerPositionSlices.BadgerThreePosition)
        ];
        if (fixtureCollection.Any(exists => !exists))
            throw new FileNotFoundException("Test cannot initialise without test data");
    }

    /// <summary>
    /// Tests that full badger swarm work queue clear success
    /// </summary>
    [TestMethod]
    public async Task FullBadgerSwarmWorkQueueClear_Success()
    {
        // Arrange
        var groundControlPayload = await File.ReadAllTextAsync(TestDataInput.RoboBadgerGroundControlTransmission);
        var marsBasePayload = await File.ReadAllTextAsync(TestDataOutput.RoboBadgerMarsBaseTransmission);
        
        var finalResult = await _badgerSwarmProcessor.ProcessTransmission(groundControlPayload);
        Assert.AreEqual(marsBasePayload, finalResult, "The final result of the badger swarm work queue does not match the expected output");
    }
}
      
