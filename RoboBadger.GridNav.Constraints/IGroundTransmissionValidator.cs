namespace RoboBadger.GridNav.Constraints;

/// <summary>
/// The ground transmission validator interface
/// </summary>
public interface IGroundTransmissionValidator
{
    /// <summary>
    /// Ises the valid grid line using the specified line
    /// </summary>
    /// <param name="line">The line</param>
    /// <returns>The bool</returns>
    public bool IsValidGridLine(string line);
    /// <summary>
    /// Ises the valid instruction line using the specified line
    /// </summary>
    /// <param name="line">The line</param>
    /// <returns>The bool</returns>
    public bool IsValidInstructionLine(string line);
    /// <summary>
    /// Ises the valid position line using the specified line
    /// </summary>
    /// <param name="line">The line</param>
    /// <returns>The bool</returns>
    public bool IsValidPositionLine(string line);
    /// <summary>
    /// Ises the rectangular bounded grid using the specified longitude grid size
    /// </summary>
    /// <param name="longitudeGridSize">The longitude grid size</param>
    /// <param name="latitudeGridSize">The latitude grid size</param>
    /// <returns>The bool</returns>
    public bool IsRectangularBoundedGrid(int longitudeGridSize, int latitudeGridSize);
    /// <summary>
    /// Ises the non whitespace using the specified line
    /// </summary>
    /// <param name="line">The line</param>
    /// <returns>The bool</returns>
    public bool IsNonWhitespace(string line);
    /// <summary>
    /// Nots the exceeding max instruction line length using the specified line
    /// </summary>
    /// <param name="line">The line</param>
    /// <returns>The bool</returns>
    public bool NotExceedingMaxInstructionLineLength(string line);
    /// <summary>
    /// Nots the exceeding max longitude using the specified line
    /// </summary>
    /// <param name="line">The line</param>
    /// <returns>The bool</returns>
    public bool NotExceedingMaxLongitude(string line);
    /// <summary>
    /// Nots the exceeding max latitude using the specified line
    /// </summary>
    /// <param name="line">The line</param>
    /// <returns>The bool</returns>
    public bool NotExceedingMaxLatitude(string line);
    
    /// <summary>
    /// Runs the transmission validation using the specified ground control payload
    /// </summary>
    /// <param name="groundControlPayload">The ground control payload</param>
    /// <returns>A list of bool valid and string message</returns>
    public List<(bool Valid, string Message)> RunTransmissionValidation(string groundControlPayload);
}