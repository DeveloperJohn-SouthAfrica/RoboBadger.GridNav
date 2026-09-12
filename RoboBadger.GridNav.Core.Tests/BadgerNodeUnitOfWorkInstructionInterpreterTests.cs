using RoboBadger.GridNav.Constraints.BadgerNodeUnitOfWorkInterpretationExtensions;
using RoboBadger.GridNav.Constraints.TransmissionInterpretationExtensions;

namespace RoboBadger.GridNav.Core.Tests;

/// <summary>
/// The badger node unit of work location interpreter tests class
/// </summary>
[TestClass]
[DeploymentItem("SampleDataFixtures", "SampleDataFixtures")]
public class BadgerNodeUnitOfWorkLocationInterpreterTests
{
    /// <summary>
    /// Initializes this instance
    /// </summary>
    /// <exception cref="FileNotFoundException">Test cannot initialise without test data</exception>
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

    // test dispatch queue returns horizontal in sequence
    /// <summary>
    /// Tests that ground control transmission instruction queue runs serial badger crawl passes
    /// </summary>
    [TestMethod]
    [Description("Ensures that interpretation of X, Y and Orientation are correct for each badger node in the dispatch queue")]
    public void GroundControlTransmission_InstructionQueueRunsSerialBadgerCrawl_Passes()
    {
        // Arrange
        var groundControlPayload = File.ReadAllText(TestDataInput.RoboBadgerGroundControlTransmission);
        var badgerSwarmNodes = groundControlPayload.BadgerSwarmNodeCount();

        var badgerOneExpectedPositionX = File.ReadAllText(TestDataBadgerPositionSlices.BadgerOnePositionX).Trim();
        var badgerOneExpectedPositionY = File.ReadAllText(TestDataBadgerPositionSlices.BadgerOnePositionY).Trim();
        var badgerOneExpectedPositionOrientation = File.ReadAllText(TestDataBadgerPositionSlices.BadgerOnePositionOrientation).Trim();

        var badgerTwoExpectedPositionX = File.ReadAllText(TestDataBadgerPositionSlices.BadgerTwoPositionX).Trim();
        var badgerTwoExpectedPositionY = File.ReadAllText(TestDataBadgerPositionSlices.BadgerTwoPositionY).Trim();
        var badgerTwoExpectedPositionOrientation = File.ReadAllText(TestDataBadgerPositionSlices.BadgerTwoPositionOrientation).Trim();

        var badgerThreeExpectedPositionX = File.ReadAllText(TestDataBadgerPositionSlices.BadgerThreePositionX).Trim();
        var badgerThreeExpectedPositionY = File.ReadAllText(TestDataBadgerPositionSlices.BadgerThreePositionY).Trim();
        var badgerThreeExpectedPositionOrientation = File.ReadAllText(TestDataBadgerPositionSlices.BadgerThreePositionOrientation).Trim();


        // Act
        var unitsOfWork = groundControlPayload.BadgerInstructionDispatchQueue();

        // Assert
        for (int processingBadgerNode = 1; processingBadgerNode <= badgerSwarmNodes; processingBadgerNode++)
        {
            var unitOfwork = unitsOfWork.Dequeue();
            switch (processingBadgerNode)
            {
                case 1:
                {
                    Assert.IsTrue(unitOfwork.BadgerNumber.Equals(processingBadgerNode));
                    Assert.AreEqual(badgerOneExpectedPositionX, unitOfwork.UnitOfWorkPositionHorizontalPosition());
                    Assert.AreEqual(badgerOneExpectedPositionY, unitOfwork.UnitOfWorkPositionVerticalPosition());
                    Assert.AreEqual(badgerOneExpectedPositionOrientation, unitOfwork.UnitOfWorkOrientation());
                    break;
                }
                case 2:
                {
                    Assert.IsTrue(unitOfwork.BadgerNumber.Equals(processingBadgerNode));
                    Assert.AreEqual(badgerTwoExpectedPositionX, unitOfwork.UnitOfWorkPositionHorizontalPosition());
                    Assert.AreEqual(badgerTwoExpectedPositionY, unitOfwork.UnitOfWorkPositionVerticalPosition());
                    Assert.AreEqual(badgerTwoExpectedPositionOrientation, unitOfwork.UnitOfWorkOrientation());
                    break;
                }
                case 3:
                {
                    Assert.IsTrue(unitOfwork.BadgerNumber.Equals(processingBadgerNode));
                    Assert.AreEqual(badgerThreeExpectedPositionX, unitOfwork.UnitOfWorkPositionHorizontalPosition());
                    Assert.AreEqual(badgerThreeExpectedPositionY, unitOfwork.UnitOfWorkPositionVerticalPosition());
                    Assert.AreEqual(badgerThreeExpectedPositionOrientation, unitOfwork.UnitOfWorkOrientation());
                    break;
                }
                default:
                    Assert.Fail(
                        $"Mars base badger swarm does not have a badger node available to process badger {unitOfwork.BadgerNumber}");
                    break;
            }
        }
    }
}