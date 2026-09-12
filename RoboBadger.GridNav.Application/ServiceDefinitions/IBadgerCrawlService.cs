namespace RoboBadger.GridNav.Application.ServiceDefinitions;

public interface IBadgerCrawlService
{
    Task <(int currentXPosition, int currentYPosition)> CrawlForwardAsync(decimal stepOrientation, int currentYPosition, int currentXPosition);
}