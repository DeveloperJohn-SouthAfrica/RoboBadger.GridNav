namespace RoboBadger.GridNav.Core.Tests;

public static class TestDataInput
{
    public static readonly string RoboBadgerGroundControlTransmission = "SampleDataFixtures/RoboBadger.GroundControl.Transmission.txt";
}

public static class TestDataOutput
{
    public static readonly string RoboBadgerMarsBaseTransmission = "SampleDataFixtures/RoboBadger.MarsBase.Transmission.txt";
}

public static class TestDataGridSlice
{
    public static readonly string GridParameters = "SampleDataFixtures/InputSpliceResults/00-RoboBadgerGrid/Fixture.Grid.Parameters.txt";
}

public static class TestDataBadgerInstructionSlices
{
    public static readonly string BadgerOneInstruction = "SampleDataFixtures/InputSpliceResults/01-RoboBadgerOne/Fixture.SampleOne.Instruction.txt";
    public static readonly string BadgerTwoInstruction = "SampleDataFixtures/InputSpliceResults/02-RoboBadgerTwo/Fixture.SampleTwo.Instruction.txt";
    public static readonly string BadgerThreeInstruction = "SampleDataFixtures/InputSpliceResults/03-RoboBadgerThree/Fixture.SampleThree.Instruction.txt";
}

public static class TestDataBadgerSwarmMetaData
{
    public static readonly int BadgerSwarmNodes = 3;
    public static readonly int LinesPerBadgerInstruction = 2;
    public static readonly int ExpectedBadgerInstructionLines = BadgerSwarmNodes * LinesPerBadgerInstruction;

}

public static class TestDataBadgerPositionSlices
{
    public static readonly string BadgerOnePosition = "SampleDataFixtures/InputSpliceResults/01-RoboBadgerOne/Fixture.SampleOne.Position.txt";
    public static readonly string BadgerTwoPosition = "SampleDataFixtures/InputSpliceResults/02-RoboBadgerTwo/Fixture.SampleTwo.Position.txt";
    public static readonly string BadgerThreePosition = "SampleDataFixtures/InputSpliceResults/03-RoboBadgerThree/Fixture.SampleThree.Position.txt";
    
    public static readonly string BadgerOnePositionOrientation = "SampleDataFixtures/InputSpliceResults/01-RoboBadgerOne/Fixture.SampleOne.Position.Orientation.txt";
    public static readonly string BadgerOnePositionX = "SampleDataFixtures/InputSpliceResults/01-RoboBadgerOne/Fixture.SampleOne.Position.X.txt";
    public static readonly string BadgerOnePositionY = "SampleDataFixtures/InputSpliceResults/01-RoboBadgerOne/Fixture.SampleOne.Position.Y.txt";
    
    public static readonly string BadgerTwoPositionOrientation = "SampleDataFixtures/InputSpliceResults/02-RoboBadgerTwo/Fixture.SampleTwo.Position.Orientation.txt";
    public static readonly string BadgerTwoPositionX = "SampleDataFixtures/InputSpliceResults/02-RoboBadgerTwo/Fixture.SampleTwo.Position.X.txt";
    public static readonly string BadgerTwoPositionY = "SampleDataFixtures/InputSpliceResults/02-RoboBadgerTwo/Fixture.SampleTwo.Position.Y.txt";

    public static readonly string BadgerThreePositionOrientation = "SampleDataFixtures/InputSpliceResults/03-RoboBadgerThree/Fixture.SampleThree.Position.Orientation.txt";
    public static readonly string BadgerThreePositionX = "SampleDataFixtures/InputSpliceResults/03-RoboBadgerThree/Fixture.SampleThree.Position.X.txt";
    public static readonly string BadgerThreePositionY = "SampleDataFixtures/InputSpliceResults/03-RoboBadgerThree/Fixture.SampleThree.Position.Y.txt";
    
    
}