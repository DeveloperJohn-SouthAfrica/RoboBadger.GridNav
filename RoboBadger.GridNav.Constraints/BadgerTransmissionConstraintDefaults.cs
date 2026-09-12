using System.Text.RegularExpressions;
using RoboBadger.GridNav.Constraints.TransmissionInterpretationExtensions;

namespace RoboBadger.GridNav.Constraints;

public static class BadgerTransmissionConstraintDefaults
{
    public const int PayloadLinesPerBadger = 2;
    public const int MaximumCharactersPerInstructionLine = 100;
    public const int MaximumLongitudeGridSize = 50;
    public const int MaximumLatitudeGridSize = 50;
    public static bool IsRectangularGrid(int longitudeGridSize, int latitudeGridSize) =>
        longitudeGridSize > 0 && longitudeGridSize <= MaximumLongitudeGridSize &&
        latitudeGridSize > 0 && latitudeGridSize <= MaximumLatitudeGridSize &&
        longitudeGridSize != latitudeGridSize;
    public static readonly Regex NonWhitespace = new (@"\S");
    public static readonly Regex ValidGridLine = new ($"^[0-9]{{1,2}} [0-9]{{1,2}}$");
    public static readonly Regex ValidInstructionLine = new ($"^[LRM]{{1,{MaximumCharactersPerInstructionLine}}}$");
    public static readonly Regex ValidPositionLine = new ($"^[0-9]{{1,2}} [0-9]{{1,2}} [NSEW]$");
}

public class GroundTransmissionValidator : IGroundTransmissionValidator
{
    public bool IsValidGridLine(string line) => BadgerTransmissionConstraintDefaults.ValidGridLine.IsMatch(line);
    public bool IsValidInstructionLine(string line) => BadgerTransmissionConstraintDefaults.ValidInstructionLine.IsMatch(line);
    public bool IsValidPositionLine(string line) => BadgerTransmissionConstraintDefaults.ValidPositionLine.IsMatch(line);
    public bool IsRectangularBoundedGrid(int longitudeGridSize, int latitudeGridSize) => BadgerTransmissionConstraintDefaults.IsRectangularGrid(longitudeGridSize, latitudeGridSize);
    public bool IsNonWhitespace(string line) => BadgerTransmissionConstraintDefaults.NonWhitespace.IsMatch(line);
    public bool NotExceedingMaxInstructionLineLength(string line) => line.Length <= BadgerTransmissionConstraintDefaults.MaximumCharactersPerInstructionLine;
    public bool NotExceedingMaxLongitude(string line)
    {
        var parts = line.Split(' ');
        if (parts.Length < 2 || !int.TryParse(parts[0], out int longitude))
            return false;
        return longitude <= BadgerTransmissionConstraintDefaults.MaximumLongitudeGridSize;
    }
    public bool NotExceedingMaxLatitude(string line)
    {
        var parts = line.Split(' ');
        if (parts.Length < 2 || !int.TryParse(parts[1], out int latitude))
            return false;
        return latitude <= BadgerTransmissionConstraintDefaults.MaximumLatitudeGridSize;
    }

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