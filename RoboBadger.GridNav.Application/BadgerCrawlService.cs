using RoboBadger.GridNav.Application.ServiceDefinitions;
using RoboBadger.GridNav.Constraints.BadgerNodeUnitOfWorkInterpretationExtensions;

namespace RoboBadger.GridNav.Application;

public class BadgerCrawlService : IBadgerCrawlService
{
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