namespace RoboBadger.GridNav.Application.ServiceDefinitions;

/// <summary>
/// The badger safety emission service interface
/// </summary>
public interface IBadgerSafetyEmissionService
{
    /// <summary>
    /// Emmits the or detect scent using the specified ground control payload
    /// </summary>
    /// <param name="groundControlPayload">The ground control payload</param>
    /// <param name="currentXPosition">The current position</param>
    /// <param name="currentYPosition">The current position</param>
    /// <param name="stepOrientation">The step orientation</param>
    /// <param name="results">The results</param>
    /// <returns>A task containing the bool lost bool bounce</returns>
    Task<(bool Lost, bool Bounce)> EmmitOrDetectScentAsync(string groundControlPayload, int currentXPosition, int currentYPosition, decimal stepOrientation, List<(string X, string Y, string Heading, string? Status)> results);
}