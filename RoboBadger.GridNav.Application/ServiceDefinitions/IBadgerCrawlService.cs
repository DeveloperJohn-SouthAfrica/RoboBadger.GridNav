namespace RoboBadger.GridNav.Application.ServiceDefinitions;

/// <summary>
/// The badger crawl service interface
/// </summary>
public interface IBadgerCrawlService
{
    /// <summary>
    /// Crawls the forward using the specified step orientation
    /// </summary>
    /// <param name="stepOrientation">The step orientation</param>
    /// <param name="currentYPosition">The current position</param>
    /// <param name="currentXPosition">The current position</param>
    /// <returns>A task of int current x position and int current y position</returns>
    Task <(int currentXPosition, int currentYPosition)> CrawlForwardAsync(decimal stepOrientation, int currentYPosition, int currentXPosition);
}