using RoboBadger.GridNav.Application.ServiceDefinitions;
using RoboBadger.GridNav.Constraints.TransmissionInterpretationExtensions;

namespace RoboBadger.GridNav.Application;

/// <summary>
/// The badger safety emission service class
/// </summary>
/// <seealso cref="IBadgerSafetyEmissionService"/>
public class BadgerSafetyEmissionService : IBadgerSafetyEmissionService
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
    public async Task<(bool Lost, bool Bounce)> EmmitOrDetectScentAsync(string groundControlPayload, int currentXPosition, int currentYPosition, decimal stepOrientation, List<(string X, string Y, string Heading, string? Status)> results)
    {
        // Check bounce
        if (groundControlPayload.BadgerNodeMaybeLostSignalEmissions(currentXPosition, currentYPosition, stepOrientation))
        {
            if (groundControlPayload.BadgerNodeLossScentDetected(currentXPosition, currentYPosition, stepOrientation, results))
            {
                // bounce this crawl step, do the next crawl step.
                return (Lost: false, Bounce: true);
            }

            // Whoops! We lost our badger
            return (Lost: true, Bounce: false);
        }

        return (Lost: false, Bounce: false);
    }
}