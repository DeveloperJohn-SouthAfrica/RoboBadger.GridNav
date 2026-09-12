using Microsoft.Extensions.DependencyInjection;
using RoboBadger.GridNav.Constraints;
using RoboBadger.GridNav.Constraints.TransmissionInterpretationExtensions;

namespace RoboBadger.GridNav.Core.Tests;

[TestClass]
public class GroundTransmissionConstraintTests
{
    private IServiceCollection? _serviceCollection;
    private IServiceProvider? _serviceProvider;
    private IGroundTransmissionValidator? _groundTransmissionValidator;
    
    [TestInitialize]
    public void Initialize()
    {
        _serviceCollection = new ServiceCollection();
        _serviceCollection.AddSingleton<IGroundTransmissionValidator, GroundTransmissionValidator>();
        _serviceProvider = _serviceCollection.BuildServiceProvider();
        _groundTransmissionValidator = _serviceProvider.GetRequiredService<IGroundTransmissionValidator>();
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
    [Description("Ensures the Ground Transmission Validator can validate a transmission from Ground Control to Mars Base")]
    public void GroundTransmissionValidator_ValidTransmission_Passes()
    {
        // Arrange
        var groundControlPayload = File.ReadAllText(TestDataInput.RoboBadgerGroundControlTransmission);

        // Act
        var isValidGridLine = _groundTransmissionValidator!.IsValidGridLine(groundControlPayload.BadgerGrid());
        var gridIsRectangular = _groundTransmissionValidator!.IsRectangularBoundedGrid(Convert.ToInt32(groundControlPayload.BadgerGrid().Split(" ")[0]), Convert.ToInt32(groundControlPayload.BadgerGrid().Split(" ")[1]));
        var doesNotExceedMaxGridLongitudeSize = _groundTransmissionValidator!.NotExceedingMaxLongitude(groundControlPayload.BadgerGrid());
        var doesNotExceedMaxGridLatitudeSize = _groundTransmissionValidator!.NotExceedingMaxLatitude(groundControlPayload.BadgerGrid());

        var workQueue = groundControlPayload.BadgerInstructionDispatchQueue();
        var badgerNodeCount = groundControlPayload.BadgerSwarmNodeCount();

        // Assert
        for (int i = 0; i < badgerNodeCount; i++)
        {
            var workItem = workQueue.Dequeue();
            var notExceedingMaxInstructionLineLength = _groundTransmissionValidator!.NotExceedingMaxInstructionLineLength(workItem.Instructions);
            Assert.IsTrue(notExceedingMaxInstructionLineLength, "Ground Transmission Validator failed to validate the max instruction line length from Ground Control to Mars Base");
        }
        Assert.IsTrue(isValidGridLine, "Ground Transmission Validator failed to validate a valid grid line from Ground Control to Mars Base");
        Assert.IsTrue(gridIsRectangular, "Ground Transmission Validator failed to validate a rectangular bounded grid from Ground Control to Mars Base");
        Assert.IsTrue(doesNotExceedMaxGridLongitudeSize, "Ground Transmission Validator failed to validate the max grid longitude size from Ground Control to Mars Base");
        Assert.IsTrue(doesNotExceedMaxGridLatitudeSize, "Ground Transmission Validator failed to validate the max grid latitude size from Ground Control to Mars Base");
        
    }


}