namespace RoboBadger.GridNav.Application.ServiceDefinitions;

public interface IBadgerSafetyEmissionService
{
    Task<(bool Lost, bool Bounce)> EmmitOrDetectScentAsync(string groundControlPayload, int currentXPosition, int currentYPosition, decimal stepOrientation, List<(string X, string Y, string Heading, string? Status)> results);
}