namespace RoboBadger.GridNav.Core.Tests;

/// <summary>
/// The test data input class
/// </summary>
public static class TestDataInput
{
    /// <summary>
    /// The robo badger ground control transmission
    /// </summary>
    public static readonly string RoboBadgerGroundControlTransmission = "SampleDataFixtures/RoboBadger.GroundControl.Transmission.txt";
}

/// <summary>
/// The test data output class
/// </summary>
public static class TestDataOutput
{
    /// <summary>
    /// The robo badger mars base transmission
    /// </summary>
    public static readonly string RoboBadgerMarsBaseTransmission = "SampleDataFixtures/RoboBadger.MarsBase.Transmission.txt";
}

/// <summary>
/// The test data grid slice class
/// </summary>
public static class TestDataGridSlice
{
    /// <summary>
    /// The grid parameters
    /// </summary>
    public static readonly string GridParameters = "SampleDataFixtures/InputSpliceResults/00-RoboBadgerGrid/Fixture.Grid.Parameters.txt";
}

/// <summary>
/// The test data badger instruction slices class
/// </summary>
public static class TestDataBadgerInstructionSlices
{
    /// <summary>
    /// The badger one instruction
    /// </summary>
    public static readonly string BadgerOneInstruction = "SampleDataFixtures/InputSpliceResults/01-RoboBadgerOne/Fixture.SampleOne.Instruction.txt";
    /// <summary>
    /// The badger two instruction
    /// </summary>
    public static readonly string BadgerTwoInstruction = "SampleDataFixtures/InputSpliceResults/02-RoboBadgerTwo/Fixture.SampleTwo.Instruction.txt";
    /// <summary>
    /// The badger three instruction
    /// </summary>
    public static readonly string BadgerThreeInstruction = "SampleDataFixtures/InputSpliceResults/03-RoboBadgerThree/Fixture.SampleThree.Instruction.txt";
}

/// <summary>
/// The test data badger swarm meta data class
/// </summary>
public static class TestDataBadgerSwarmMetaData
{
    /// <summary>
    /// The badger swarm nodes
    /// </summary>
    public static readonly int BadgerSwarmNodes = 3;
    /// <summary>
    /// The lines per badger instruction
    /// </summary>
    public static readonly int LinesPerBadgerInstruction = 2;
    /// <summary>
    /// The lines per badger instruction
    /// </summary>
    public static readonly int ExpectedBadgerInstructionLines = BadgerSwarmNodes * LinesPerBadgerInstruction;

}

/// <summary>
/// The test data badger position slices class
/// </summary>
public static class TestDataBadgerPositionSlices
{
    /// <summary>
    /// The badger one position
    /// </summary>
    public static readonly string BadgerOnePosition = "SampleDataFixtures/InputSpliceResults/01-RoboBadgerOne/Fixture.SampleOne.Position.txt";
    /// <summary>
    /// The badger two position
    /// </summary>
    public static readonly string BadgerTwoPosition = "SampleDataFixtures/InputSpliceResults/02-RoboBadgerTwo/Fixture.SampleTwo.Position.txt";
    /// <summary>
    /// The badger three position
    /// </summary>
    public static readonly string BadgerThreePosition = "SampleDataFixtures/InputSpliceResults/03-RoboBadgerThree/Fixture.SampleThree.Position.txt";
    
    /// <summary>
    /// The badger one position orientation
    /// </summary>
    public static readonly string BadgerOnePositionOrientation = "SampleDataFixtures/InputSpliceResults/01-RoboBadgerOne/Fixture.SampleOne.Position.Orientation.txt";
    /// <summary>
    /// The badger one position
    /// </summary>
    public static readonly string BadgerOnePositionX = "SampleDataFixtures/InputSpliceResults/01-RoboBadgerOne/Fixture.SampleOne.Position.X.txt";
    /// <summary>
    /// The badger one position
    /// </summary>
    public static readonly string BadgerOnePositionY = "SampleDataFixtures/InputSpliceResults/01-RoboBadgerOne/Fixture.SampleOne.Position.Y.txt";
    
    /// <summary>
    /// The badger two position orientation
    /// </summary>
    public static readonly string BadgerTwoPositionOrientation = "SampleDataFixtures/InputSpliceResults/02-RoboBadgerTwo/Fixture.SampleTwo.Position.Orientation.txt";
    /// <summary>
    /// The badger two position
    /// </summary>
    public static readonly string BadgerTwoPositionX = "SampleDataFixtures/InputSpliceResults/02-RoboBadgerTwo/Fixture.SampleTwo.Position.X.txt";
    /// <summary>
    /// The badger two position
    /// </summary>
    public static readonly string BadgerTwoPositionY = "SampleDataFixtures/InputSpliceResults/02-RoboBadgerTwo/Fixture.SampleTwo.Position.Y.txt";

    /// <summary>
    /// The badger three position orientation
    /// </summary>
    public static readonly string BadgerThreePositionOrientation = "SampleDataFixtures/InputSpliceResults/03-RoboBadgerThree/Fixture.SampleThree.Position.Orientation.txt";
    /// <summary>
    /// The badger three position
    /// </summary>
    public static readonly string BadgerThreePositionX = "SampleDataFixtures/InputSpliceResults/03-RoboBadgerThree/Fixture.SampleThree.Position.X.txt";
    /// <summary>
    /// The badger three position
    /// </summary>
    public static readonly string BadgerThreePositionY = "SampleDataFixtures/InputSpliceResults/03-RoboBadgerThree/Fixture.SampleThree.Position.Y.txt";
    
    
}