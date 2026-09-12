namespace RoboBadger.GridNav.Constraints.BadgerNodeUnitOfWorkInterpretationExtensions;

public static class BadgerNavigationExtensions
{
  
    public static string OrientationHeading(this decimal bearingFactor)
    {
        var headings = new Dictionary<decimal, string>
        {
            { 0m/360, "N" },
            { 90m/360, "E" },
            { 180m/360, "S" },
            { 270m/360, "W" }
            // Add more compas points as needed
        };
        return headings.GetValueOrDefault(bearingFactor, "No grid compas heading found for this bearing factor");
    }

    public static Dictionary<string, decimal> OrientationCommands()
    {
        return new Dictionary<string, decimal>
        {
            { "N", 0m/360 },
            { "E", 90m/360 },
            { "S", 180m/360 },
            { "W", 270m/360 }
            // Add more orientation commands as needed 
        };
        
    }


    public static Dictionary<string, decimal> DirectionCommands() 
    {
        return new Dictionary<string, decimal>
        {
            { "L", -90m/360 },
            { "R", 90m/360 },
            // Add more direction commands as needed
        };
    }

    public static Dictionary<string, decimal> MovementCommands()
    {
        return new Dictionary<string, decimal>
        {
            { "F", 1m },
            // Add more movement commands as needed
        };
        
    }
}