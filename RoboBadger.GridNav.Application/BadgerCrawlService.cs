using RoboBadger.GridNav.Application.ServiceDefinitions;
using RoboBadger.GridNav.Constraints.BadgerNodeUnitOfWorkInterpretationExtensions;

namespace RoboBadger.GridNav.Application;

/// <summary>
/// The badger crawl service class
/// </summary>
/// <seealso cref="IBadgerCrawlService"/>
public class BadgerCrawlService : IBadgerCrawlService
{
    /// <summary>
    /// Crawls the forward using the specified step orientation
    /// </summary>
    /// <param name="stepOrientation">The step orientation</param>
    /// <param name="currentYPosition">The current position</param>
    /// <param name="currentXPosition">The current position</param>
    /// <returns>A task of int current x position and int current y position</returns>
    public async Task <(int currentXPosition, int currentYPosition)> CrawlForwardAsync(decimal stepOrientation, int currentYPosition, int currentXPosition)
    {
        if (stepOrientation == BadgerNavigationExtensions.OrientationCommands()["N"])
        {
            currentYPosition += Convert.ToInt32(BadgerNavigationExtensions.MovementCommands()["F"]);
        }
        else if (stepOrientation == BadgerNavigationExtensions.OrientationCommands()["E"])
        {
            currentXPosition += Convert.ToInt32(BadgerNavigationExtensions.MovementCommands()["F"]);
        }
        else if (stepOrientation == BadgerNavigationExtensions.OrientationCommands()["S"])
        {
            currentYPosition -= Convert.ToInt32(BadgerNavigationExtensions.MovementCommands()["F"]);
        }
        else if (stepOrientation == BadgerNavigationExtensions.OrientationCommands()["W"])
        {
            currentXPosition -= Convert.ToInt32(BadgerNavigationExtensions.MovementCommands()["F"]);
        }

        return (currentXPosition,  currentYPosition);
    }
}