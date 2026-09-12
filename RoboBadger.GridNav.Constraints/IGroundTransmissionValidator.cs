namespace RoboBadger.GridNav.Constraints;

public interface IGroundTransmissionValidator
{
    public bool IsValidGridLine(string line);
    public bool IsValidInstructionLine(string line);
    public bool IsValidPositionLine(string line);
    public bool IsRectangularBoundedGrid(int longitudeGridSize, int latitudeGridSize);
    public bool IsNonWhitespace(string line);
    public bool NotExceedingMaxInstructionLineLength(string line);
    public bool NotExceedingMaxLongitude(string line);
    public bool NotExceedingMaxLatitude(string line);
    
    public List<(bool Valid, string Message)> RunTransmissionValidation(string groundControlPayload);
}