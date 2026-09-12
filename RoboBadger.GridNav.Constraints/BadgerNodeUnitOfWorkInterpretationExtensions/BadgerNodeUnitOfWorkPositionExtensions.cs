using System.Text.RegularExpressions;

namespace RoboBadger.GridNav.Constraints.BadgerNodeUnitOfWorkInterpretationExtensions;

/// <summary>
/// The badger node unit of work position extensions class
/// </summary>
public static class BadgerNodeUnitOfWorkPositionExtensions
{
    /// <summary>
    /// Units the of work position horizontal position using the specified badger node queue unt of work
    /// </summary>
    /// <param name="badgerNodeQueueUntOfWork">The badger node queue unt of work</param>
    /// <returns>The horizontal grid point</returns>
    public static string UnitOfWorkPositionHorizontalPosition(this (int BadgerNumber, string Position, string Instructions) badgerNodeQueueUntOfWork)
    {
        var sanitizedPosition = badgerNodeQueueUntOfWork.UnitOfWorkPositionSanitised();
        var horizontalGridPoint = sanitizedPosition[0];
        return horizontalGridPoint;
    }

    /// <summary>
    /// Units the of work position vertical position using the specified badger node queue unt of work
    /// </summary>
    /// <param name="badgerNodeQueueUntOfWork">The badger node queue unt of work</param>
    /// <returns>The vertical grid point</returns>
    public static string UnitOfWorkPositionVerticalPosition(this (int BadgerNumber, string Position, string Instructions) badgerNodeQueueUntOfWork)
    {
        var sanitizedPositionCharacters = badgerNodeQueueUntOfWork.UnitOfWorkPositionSanitised();
        var verticalGridPoint = sanitizedPositionCharacters[1];
        return verticalGridPoint;
    }

    /// <summary>
    /// Units the of work orientation using the specified badger node queue unt of work
    /// </summary>
    /// <param name="badgerNodeQueueUntOfWork">The badger node queue unt of work</param>
    /// <returns>The orientation</returns>
    public static string UnitOfWorkOrientation(this (int BadgerNumber, string Position, string Instructions) badgerNodeQueueUntOfWork)
    {
        var sanitizedPositionCharacters = badgerNodeQueueUntOfWork.UnitOfWorkPositionSanitised();
        var orientation = sanitizedPositionCharacters[2];
        return orientation;
    }

    /// <summary>
    /// Units the of work position sanitised using the specified badger node queue unt of work
    /// </summary>
    /// <param name="badgerNodeQueueUntOfWork">The badger node queue unt of work</param>
    /// <returns>The string array</returns>
    public static string[] UnitOfWorkPositionSanitised(this (int BadgerNumber, string Position, string Instructions) badgerNodeQueueUntOfWork)
    {
        var nonWhitspaceCharacteRegex = new Regex("\\S");
        return
        [
            .. nonWhitspaceCharacteRegex.Matches(badgerNodeQueueUntOfWork.Position)
                .Select(match => match.Value[0].ToString())
        ];
    }

   
    /// <summary>
    /// Units the of work crawl sequence using the specified instructions
    /// </summary>
    /// <param name="instructions">The instructions</param>
    /// <exception cref="ArgumentException">Invalid command '{command}' in instructions.</exception>
    /// <returns>The crawl sequence queue</returns>
    public static Queue<(string Command, decimal Value)> UnitOfWorkCrawlSequence(this string instructions)
    {
        var nonWhitspaceCharacteRegex = new Regex("\\S");
        var charArray =  nonWhitspaceCharacteRegex.Matches(instructions)
            .Select(match => match.Value[0].ToString());
        
        var orientationCommands = BadgerNavigationExtensions.OrientationCommands();
        var directionCommands = BadgerNavigationExtensions.DirectionCommands();
        var movementCommands = BadgerNavigationExtensions.MovementCommands();
        
        var crawlSequenceQueue = new Queue<(string Command, decimal Value)>();
        foreach (var command in charArray)
        {
            // if (orientationCommands.TryGetValue(command, out var orientationValue))
            // {
            //     crawlSequenceQueue.Enqueue((command, orientationValue));
            // }
            if (directionCommands.TryGetValue(command, out var directionValue))
            {
                crawlSequenceQueue.Enqueue((Command: command, Value: directionValue));
            }
            else if (movementCommands.TryGetValue(command, out var movementValue))
            {
                crawlSequenceQueue.Enqueue((Command: command, Value: movementValue));
            }
            else
            {
                throw new ArgumentException($"Invalid command '{command}' in instructions.");
            }
        }
        return crawlSequenceQueue;
    }
}