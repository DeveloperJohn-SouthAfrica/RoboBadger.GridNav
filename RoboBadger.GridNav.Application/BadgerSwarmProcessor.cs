using System.Text;
using RoboBadger.GridNav.Application.ServiceDefinitions;
using RoboBadger.GridNav.Constraints;
using RoboBadger.GridNav.Constraints.BadgerNodeUnitOfWorkInterpretationExtensions;
using RoboBadger.GridNav.Constraints.TransmissionInterpretationExtensions;

namespace RoboBadger.GridNav.Application;

/// <summary>
/// The badger swarm processor class
/// </summary>
/// <seealso cref="IBadgerSwarmProcessor"/>
public class BadgerSwarmProcessor(
    IBadgerSafetyEmissionService badgerSafetyEmissionService,
    IBadgerCrawlService badgerCrawlService, 
    IGroundTransmissionValidator groundTransmissionValidator) : IBadgerSwarmProcessor
{

    /// <summary>
    /// Processes the transmission using the specified ground control payload
    /// </summary>
    /// <param name="groundControlPayload">The ground control payload</param>
    /// <returns>The final result</returns>
    public async Task<string> ProcessTransmission(string groundControlPayload)
    {
        SanitiseInputs(groundControlPayload);

        Queue<(int BadgerNumber, string Position, string Instructions)> swarmOrchestrationQueue = groundControlPayload.BadgerInstructionDispatchQueue();
        var results = new List<(string X, string Y, string Heading, string? Status)>();
        
         results = await ProcessDispatchQueue(groundControlPayload, swarmOrchestrationQueue, results);

        var resultBuilder = new StringBuilder();
        foreach (var result in results)
        {
            resultBuilder.AppendLine(string.Format(
                $"{result.X} {result.Y} {result.Heading}{(!string.IsNullOrWhiteSpace(result.Status) 
                    ? $" {result.Status}" 
                    : string.Empty)}"
            ));
        }
        var finalResult = resultBuilder.ToString().Trim();
        return finalResult;
    }

    /// <summary>
    /// Sanitises the inputs using the specified ground control payload
    /// </summary>
    /// <param name="groundControlPayload">The ground control payload</param>
    private void SanitiseInputs(string groundControlPayload)
    {
        var valid = groundTransmissionValidator.RunTransmissionValidation(groundControlPayload);
        valid.ForEach(results =>
        {
            if (!results.Valid)
            {
                Console.Error.WriteLine($"Invalid transmission detected. Validation message: {results.Message}");
            }
        });
    }

    /// <summary>
    /// Processes the dispatch queue using the specified ground control payload
    /// </summary>
    /// <param name="groundControlPayload">The ground control payload</param>
    /// <param name="swarmOrchestrationQueue">The swarm orchestration queue</param>
    /// <param name="results">The results</param>
    /// <exception cref="InvalidOperationException">Crawl sequence queue is empty</exception>
    /// <returns>The results</returns>
    public async Task<List<(string X, string Y, string Heading, string? Status)>> ProcessDispatchQueue(string groundControlPayload, Queue<(int BadgerNumber, string Position, string Instructions)> swarmOrchestrationQueue, List<(string X, string Y, string Heading, string? Status)> results)
    {
        for (int i = 0; i < groundControlPayload.BadgerInstructionDispatchQueue().Count; i++)
        {
            var weLostOurBadger = false;
            var unitOfWork = swarmOrchestrationQueue.Dequeue();
            var crawCommandNumber = unitOfWork.Instructions.UnitOfWorkCrawlSequence().Count;
            var crawlSequence = unitOfWork.Instructions.UnitOfWorkCrawlSequence();
            var currentXPositionCommand = unitOfWork.UnitOfWorkPositionHorizontalPosition();
            var currentXPosition = Convert.ToInt32(currentXPositionCommand);
            var currentYPositionCommand = unitOfWork.UnitOfWorkPositionVerticalPosition();
            var currentYPosition = Convert.ToInt32(currentYPositionCommand);
            var currentOrientationCommand = unitOfWork.UnitOfWorkOrientation();
            var currentOrientation = BadgerNavigationExtensions.OrientationCommands()[currentOrientationCommand];

            for (int index = 0; index < crawCommandNumber && !weLostOurBadger; index++)
            {
                var stepOrientation = currentOrientation;
                var sequenceQueueItem = crawlSequence.TryDequeue(out var command) ? command : throw new InvalidOperationException("Crawl sequence queue is empty");
                (stepOrientation, currentXPosition, currentYPosition, weLostOurBadger, index) = await RunBadgerNodeCommandSequenceAsync(groundControlPayload, results, sequenceQueueItem, currentOrientation, crawlSequence, crawCommandNumber, stepOrientation, currentXPosition, currentYPosition, weLostOurBadger, index);

                // Keep orientation in [0.0, 1.0) for both clockwise and counter-clockwise turns.
                currentOrientation = ((stepOrientation % 1m) + 1m) % 1m;
            }
            if (!weLostOurBadger)
            {
                results.Add((currentXPosition.ToString(), currentYPosition.ToString(), currentOrientation.OrientationHeading(), string.Empty));
            }
        }

        return results;
    }

    /// <summary>
    /// Runs the badger node command sequence using the specified ground control payload
    /// </summary>
    /// <param name="groundControlPayload">The ground control payload</param>
    /// <param name="results">The results</param>
    /// <param name="sequenceQueueItem">The sequence queue item</param>
    /// <param name="currentOrientation">The current orientation</param>
    /// <param name="crawlSequence">The crawl sequence</param>
    /// <param name="crawCommandNumber">The craw command number</param>
    /// <param name="stepOrientation">The step orientation</param>
    /// <param name="currentXPosition">The current position</param>
    /// <param name="currentYPosition">The current position</param>
    /// <param name="weLostOurBadger">The we lost our badger</param>
    /// <param name="j">The </param>
    /// <returns>A task containing the decimal step orientation int current position int current position bool we lost our badger int</returns>
    private async Task<(decimal stepOrientation, int currentXPosition, int currentYPosition, bool weLostOurBadger, int j)> RunBadgerNodeCommandSequenceAsync(string groundControlPayload, List<(string X, string Y, string Heading, string? Status)> results, (string Command, decimal Value) sequenceQueueItem, decimal currentOrientation, Queue<(string Command, decimal Value)> crawlSequence, int crawCommandNumber, decimal stepOrientation,
        int currentXPosition, int currentYPosition, bool weLostOurBadger, int j)
    {
        switch (sequenceQueueItem.Command)
        {
            case "L":
            {
                stepOrientation = currentOrientation + BadgerNavigationExtensions.DirectionCommands()["L"];
                break;
            }
            case "R":
            {
                stepOrientation = currentOrientation + BadgerNavigationExtensions.DirectionCommands()["R"];
                break;
            }
            case "F":
            {
                var guard = await badgerSafetyEmissionService.EmmitOrDetectScentAsync(groundControlPayload, currentXPosition, currentYPosition, stepOrientation, results); // Skip this crawl step and move to the next one
                if (guard is { Lost: false, Bounce: false })
                {
                    var crawlPositionChange = await badgerCrawlService.CrawlForwardAsync(stepOrientation, currentYPosition, currentXPosition);
                    currentXPosition = crawlPositionChange.currentXPosition;
                    currentYPosition = crawlPositionChange.currentYPosition;
                }

                weLostOurBadger = guard.Lost;
                if (weLostOurBadger)
                {
                    results.Add((currentXPosition.ToString(), currentYPosition.ToString(), currentOrientation.OrientationHeading(), "LOST"));
                    crawlSequence.Clear();
                    j = crawCommandNumber;
                }

                break;
            }
        }

        return (stepOrientation, currentXPosition, currentYPosition, weLostOurBadger, j);
    }
}