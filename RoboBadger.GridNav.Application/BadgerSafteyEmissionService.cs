using RoboBadger.GridNav.Application.ServiceDefinitions;
using RoboBadger.GridNav.Constraints.TransmissionInterpretationExtensions;

namespace RoboBadger.GridNav.Application;

public class BadgerSafetyEmissionService : IBadgerSafetyEmissionService
{
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