namespace RoboBadger.GridNav.Constraints.BadgerNodeUnitOfWorkInterpretationExtensions;

/// <summary>
/// The badger navigation extensions class
/// </summary>
public static class BadgerNavigationExtensions
{
  
    /// <summary>
    /// Orientations the heading using the specified bearing factor
    /// </summary>
    /// <param name="bearingFactor">The bearing factor</param>
    /// <returns>The string</returns>
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

    /// <summary>
    /// Orientations the commands
    /// </summary>
    /// <returns>A dictionary of string and decimal</returns>
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


    /// <summary>
    /// Directions the commands
    /// </summary>
    /// <returns>A dictionary of string and decimal</returns>
    public static Dictionary<string, decimal> DirectionCommands() 
    {
        return new Dictionary<string, decimal>
        {
            { "L", -90m/360 },
            { "R", 90m/360 },
            // Add more direction commands as needed
        };
    }

    /// <summary>
    /// Movements the commands
    /// </summary>
    /// <returns>A dictionary of string and decimal</returns>
    public static Dictionary<string, decimal> MovementCommands()
    {
        return new Dictionary<string, decimal>
        {
            { "F", 1m },
            // Add more movement commands as needed
        };
        
    }
}