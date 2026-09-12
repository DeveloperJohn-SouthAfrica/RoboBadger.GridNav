using System.Text.RegularExpressions;

namespace RoboBadger.GridNav.Constraints.BadgerNodeUnitOfWorkInterpretationExtensions;

public static class BadgerNodeUnitOfWorkPositionExtensions
{
    public static string UnitOfWorkPositionHorizontalPosition(this (int BadgerNumber, string Position, string Instructions) badgerNodeQueueUntOfWork)
    {
        var sanitizedPosition = badgerNodeQueueUntOfWork.UnitOfWorkPositionSanitised();
        var horizontalGridPoint = sanitizedPosition[0];
        return horizontalGridPoint;
    }

    public static string UnitOfWorkPositionVerticalPosition(this (int BadgerNumber, string Position, string Instructions) badgerNodeQueueUntOfWork)
    {
        var sanitizedPositionCharacters = badgerNodeQueueUntOfWork.UnitOfWorkPositionSanitised();
        var verticalGridPoint = sanitizedPositionCharacters[1];
        return verticalGridPoint;
    }

    public static string UnitOfWorkOrientation(this (int BadgerNumber, string Position, string Instructions) badgerNodeQueueUntOfWork)
    {
        var sanitizedPositionCharacters = badgerNodeQueueUntOfWork.UnitOfWorkPositionSanitised();
        var orientation = sanitizedPositionCharacters[2];
        return orientation;
    }

    public static string[] UnitOfWorkPositionSanitised(this (int BadgerNumber, string Position, string Instructions) badgerNodeQueueUntOfWork)
    {
        var nonWhitspaceCharacteRegex = new Regex("\\S");
        return
        [
            .. nonWhitspaceCharacteRegex.Matches(badgerNodeQueueUntOfWork.Position)
                .Select(match => match.Value[0].ToString())
        ];
    }

   
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