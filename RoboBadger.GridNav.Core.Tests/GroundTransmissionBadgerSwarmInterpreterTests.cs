using RoboBadger.GridNav.Constraints.TransmissionInterpretationExtensions;

namespace RoboBadger.GridNav.Core.Tests;

[TestClass]
[DeploymentItem("SampleDataFixtures", "SampleDataFixtures")]
public sealed class GroundTransmissionBadgerSwarmInterpreterTests
{
    [TestInitialize]
    public void Initialize()
    {
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

    [TestMethod]
    [Description("Ensures the Badger Swarm Grid Initialisation Parameters received from Ground Control are ready for interpretation")]
    public void GroundControlTransmission_GridParameterSplice_Passes()
    {
        // Arrange
        var groundControlPayload = File.ReadAllText(TestDataInput.RoboBadgerGroundControlTransmission);
        var expectedGridSlice = File.ReadAllText(TestDataGridSlice.GridParameters);

        // Act
        var gridSlice = groundControlPayload.BadgerGrid();

        // Assert
        Assert.AreEqual(expectedGridSlice, gridSlice, "GridNav Interpreter for BadgerGrid does not match test data");
    }

    [TestMethod]
    [Description("Ensures enough Mars Base badger nodes are available to process the commands from Ground Control Transmissions")]
    public void GroundControlTransmission_BadgerCountSlice_Passes()
    {
        // Arrange
        var groundControlPayload = File.ReadAllText(TestDataInput.RoboBadgerGroundControlTransmission);
        var expectedNodes = TestDataBadgerSwarmMetaData.BadgerSwarmNodes;

        // Act
        var actualNodes = groundControlPayload.BadgerSwarmNodeCount();

        // Assert
        Assert.AreEqual(expectedNodes, actualNodes,
            $"Mars base has {expectedNodes} badger nodes, but ground control transmission sent a transmission for {actualNodes} badger nodes");
    }

    [TestMethod]
    [Description("Ensures the dispatch queue has the same number of units of work as the number of badger nodes")]
    public void GroundControlTransmission_BadgerDispatchQueueUnitOfWorkSlice_Passes()
    {
        // Arrange
        var groundControlPayload = File.ReadAllText(TestDataInput.RoboBadgerGroundControlTransmission);
        var expectedNodes = TestDataBadgerSwarmMetaData.BadgerSwarmNodes;

        // Act
        var unitsOfWork = groundControlPayload.BadgerInstructionDispatchQueue().Count;

        // Assert
        Assert.AreEqual(expectedNodes, unitsOfWork,
            $"Mars base has {expectedNodes} badger nodes, but ground control transmission sent a transmission with {unitsOfWork} units of work");
    }

    [TestMethod]
    [Description("Ensures that the rows of badger commands meets the 2 rows per badger node constraint")]
    public void GroundControlTransmission_ComputationRowsSlice_Passes()
    {
        // Arrange
        var groundControlPayload = File.ReadAllText(TestDataInput.RoboBadgerGroundControlTransmission);
        var expectedNodes = TestDataBadgerSwarmMetaData.BadgerSwarmNodes;
        var expectedInstructionLines = TestDataBadgerSwarmMetaData.ExpectedBadgerInstructionLines;

        // Act
        var numberOfComputableInstructionLines = groundControlPayload.BadgerSwarmComputationInstructionRows().Length;

        // Assert
        Assert.AreEqual(expectedInstructionLines, numberOfComputableInstructionLines,
            $"Mars base has {expectedNodes} badger nodes, each processing expecting 2 lines of instructions totalling {expectedInstructionLines}, but ground set a transmission with {numberOfComputableInstructionLines}");
    }
}