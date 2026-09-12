using System.Text.RegularExpressions;
using RoboBadger.GridNav.Constraints.TransmissionInterpretationExtensions;

namespace RoboBadger.GridNav.Constraints;

/// <summary>
/// The badger transmission constraint defaults class
/// </summary>
public static class BadgerTransmissionConstraintDefaults
{
    /// <summary>
    /// The payload lines per badger
    /// </summary>
    public const int PayloadLinesPerBadger = 2;
    /// <summary>
    /// The maximum characters per instruction line
    /// </summary>
    public const int MaximumCharactersPerInstructionLine = 100;
    /// <summary>
    /// The maximum longitude grid size
    /// </summary>
    public const int MaximumLongitudeGridSize = 50;
    /// <summary>
    /// The maximum latitude grid size
    /// </summary>
    public const int MaximumLatitudeGridSize = 50;
    /// <summary>
    /// Ises the rectangular grid using the specified longitude grid size
    /// </summary>
    /// <param name="longitudeGridSize">The longitude grid size</param>
    /// <param name="latitudeGridSize">The latitude grid size</param>
    /// <returns>The bool</returns>
    public static bool IsRectangularGrid(int longitudeGridSize, int latitudeGridSize) =>
        longitudeGridSize > 0 && longitudeGridSize <= MaximumLongitudeGridSize &&
        latitudeGridSize > 0 && latitudeGridSize <= MaximumLatitudeGridSize &&
        longitudeGridSize != latitudeGridSize;
    /// <summary>
    /// The non whitespace
    /// </summary>
    public static readonly Regex NonWhitespace = new (@"\S");
    /// <summary>
    /// The valid grid line
    /// </summary>
    public static readonly Regex ValidGridLine = new ($"^[0-9]{{1,2}} [0-9]{{1,2}}$");
    /// <summary>
    /// The maximum characters per instruction line
    /// </summary>
    public static readonly Regex ValidInstructionLine = new ($"^[LRM]{{1,{MaximumCharactersPerInstructionLine}}}$");
    /// <summary>
    /// The valid position line
    /// </summary>
    public static readonly Regex ValidPositionLine = new ($"^[0-9]{{1,2}} [0-9]{{1,2}} [NSEW]$");
}

/// <summary>
/// The ground transmission validator class
/// </summary>
/// <seealso cref="IGroundTransmissionValidator"/>
public class GroundTransmissionValidator : IGroundTransmissionValidator
{
    /// <summary>
    /// Ises the valid grid line using the specified line
    /// </summary>
    /// <param name="line">The line</param>
    /// <returns>The bool</returns>
    public bool IsValidGridLine(string line) => BadgerTransmissionConstraintDefaults.ValidGridLine.IsMatch(line);
    /// <summary>
    /// Ises the valid instruction line using the specified line
    /// </summary>
    /// <param name="line">The line</param>
    /// <returns>The bool</returns>
    public bool IsValidInstructionLine(string line) => BadgerTransmissionConstraintDefaults.ValidInstructionLine.IsMatch(line);
    /// <summary>
    /// Ises the valid position line using the specified line
    /// </summary>
    /// <param name="line">The line</param>
    /// <returns>The bool</returns>
    public bool IsValidPositionLine(string line) => BadgerTransmissionConstraintDefaults.ValidPositionLine.IsMatch(line);
    /// <summary>
    /// Ises the rectangular bounded grid using the specified longitude grid size
    /// </summary>
    /// <param name="longitudeGridSize">The longitude grid size</param>
    /// <param name="latitudeGridSize">The latitude grid size</param>
    /// <returns>The bool</returns>
    public bool IsRectangularBoundedGrid(int longitudeGridSize, int latitudeGridSize) => BadgerTransmissionConstraintDefaults.IsRectangularGrid(longitudeGridSize, latitudeGridSize);
    /// <summary>
    /// Ises the non whitespace using the specified line
    /// </summary>
    /// <param name="line">The line</param>
    /// <returns>The bool</returns>
    public bool IsNonWhitespace(string line) => BadgerTransmissionConstraintDefaults.NonWhitespace.IsMatch(line);
    /// <summary>
    /// Nots the exceeding max instruction line length using the specified line
    /// </summary>
    /// <param name="line">The line</param>
    /// <returns>The bool</returns>
    public bool NotExceedingMaxInstructionLineLength(string line) => line.Length <= BadgerTransmissionConstraintDefaults.MaximumCharactersPerInstructionLine;
    /// <summary>
    /// Nots the exceeding max longitude using the specified line
    /// </summary>
    /// <param name="line">The line</param>
    /// <returns>The bool</returns>
    public bool NotExceedingMaxLongitude(string line)
    {
        var parts = line.Split(' ');
        if (parts.Length < 2 || !int.TryParse(parts[0], out int longitude))
            return false;
        return longitude <= BadgerTransmissionConstraintDefaults.MaximumLongitudeGridSize;
    }
    /// <summary>
    /// Nots the exceeding max latitude using the specified line
    /// </summary>
    /// <param name="line">The line</param>
    /// <returns>The bool</returns>
    public bool NotExceedingMaxLatitude(string line)
    {
        var parts = line.Split(' ');
        if (parts.Length < 2 || !int.TryParse(parts[1], out int latitude))
            return false;
        return latitude <= BadgerTransmissionConstraintDefaults.MaximumLatitudeGridSize;
    }

    /// <summary>
    /// Runs the transmission validation using the specified ground control payload
    /// </summary>
    /// <param name="groundControlPayload">The ground control payload</param>
    /// <returns>The payload validation results</returns>
    public List<(bool Valid, string Message)> RunTransmissionValidation(string groundControlPayload)
    {
        var payloadValidationResults = new List<(bool Valid, string Message)>();

        static (bool Valid, string Message) ValidationMessage(bool valid, string successMessage, string failureMessage) =>
            (valid, valid ? successMessage : failureMessage);

        var gridLineValid = IsValidGridLine(groundControlPayload.BadgerGrid());
        payloadValidationResults.Add(ValidationMessage(gridLineValid, "The format of the Grid Line Command is valid", "The format of the Grid Line Command is not valid"));

        var rectangularGridValid = IsRectangularBoundedGrid(Convert.ToInt32(groundControlPayload.BadgerGrid().Split(" ")[0]), Convert.ToInt32(groundControlPayload.BadgerGrid().Split(" ")[1]));
        payloadValidationResults.Add(ValidationMessage(rectangularGridValid, "The grid is rectangular bounded", "The grid is not rectangular bounded"));

        var longitudeValid = NotExceedingMaxLongitude(groundControlPayload.BadgerGrid());
        payloadValidationResults.Add(ValidationMessage(longitudeValid, "The grid longitude is within the maximum allowed size", "The grid exceeds the maximum allowed longitude size"));

        var latitudeValid = NotExceedingMaxLatitude(groundControlPayload.BadgerGrid());
        payloadValidationResults.Add(ValidationMessage(latitudeValid, "The grid latitude is within the maximum allowed size", "The grid exceeds the maximum allowed latitude size"));

        var workQueue = groundControlPayload.BadgerInstructionDispatchQueue();
        var badgerNodeCount = groundControlPayload.BadgerSwarmNodeCount();
        
        for (int i = 0; i < badgerNodeCount; i++)
        {
            var workItem = workQueue.Dequeue();
            var instructionLengthValid = NotExceedingMaxInstructionLineLength(workItem.Instructions);
            payloadValidationResults.Add(ValidationMessage(
                instructionLengthValid,
                $"The instruction set for Badger {workItem.BadgerNumber} is within the allowed maximum",
                $"The instruction set for Badger {workItem.BadgerNumber} exceeds the allowed maximum"));
            
        }
        
        return payloadValidationResults;
    }
}