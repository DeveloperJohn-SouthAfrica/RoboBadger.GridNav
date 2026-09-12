using System.Text.RegularExpressions;
using RoboBadger.GridNav.Constraints.BadgerNodeUnitOfWorkInterpretationExtensions;

namespace RoboBadger.GridNav.Constraints.TransmissionInterpretationExtensions;

public static class GroundTransmissionSpliceExtensions
{
    /// <param name="transmission"></param>
    extension(string transmission)
    {
        /// <summary>
        /// Ground Control Transmission Payload Interpreted as Grid Parameters 
        /// </summary>
        /// <returns>Dual Integer Cartesian Coordinate for the top right Corner</returns>
        public string BadgerGrid()
        {
            return transmission.Split(Environment.NewLine).First();
        }

        public bool BadgerNodeMaybeLostSignalEmissions(int x, int y, decimal bearingFactor)
        {
            var grid = transmission.BadgerGrid();
            var sanitisedRegex = new Regex("\\S");

            var gridParameters = sanitisedRegex.Matches(grid)
                .Select(match => match.Value[0].ToString())
                .ToArray();

            var gridx = new List<int>();
            for (var i = 0; i <= Convert.ToInt32(gridParameters[0]); i++)
            {
                gridx.Add(i);
            }

            var gridy = new List<int>();
            for (var i = 0; i <= Convert.ToInt32(gridParameters[1]); i++)
            {
                gridy.Add(i);
            }

            var farNorth = gridy.Max();
            const decimal farNorthHorizon = 0.0m;
            var farSouth = gridy.Min();
            const decimal farSouthHorizon = 0.5m;
            var farEast = gridx.Max();
            const decimal farEastHorizon = 0.25m;
            var farWest = gridx.Min();
            const decimal farWestHorizon = 0.75m;

            return (x == farEast && bearingFactor == farEastHorizon) ||
                   (x == farWest && bearingFactor == farWestHorizon) ||
                   (y == farSouth && bearingFactor == farSouthHorizon) ||
                   (y == farNorth && bearingFactor == farNorthHorizon);
        }


        public bool BadgerNodeLossScentDetected(int x, int y, decimal bearingFactor, List<(string X, string Y, string Heading, string? Status)> results)
        {
            var lostScentDetected = results.Any(result =>
                result.X == x.ToString() &&
                result.Y == y.ToString() &&
                result.Heading == bearingFactor.OrientationHeading() &&
                result.Status == "LOST");

            return lostScentDetected;
        }

        /// <summary>
        /// Ground Control Transmission Interpreted as the total number of Robo Badgers 
        /// </summary>
        /// <returns>Single Positive Integer</returns>
        public int BadgerSwarmNodeCount()
        {
            var lines = transmission.BadgerSwarmComputationInstructionRows();
            return lines.Length / BadgerTransmissionConstraintDefaults.PayloadLinesPerBadger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] BadgerSwarmComputationInstructionRows()
        {
            return
            [
                .. transmission
                    .Split(Environment.NewLine)
                    .Where(line =>
                    {
                        var notBlank = !string.IsNullOrWhiteSpace(line);
                        var notGridParameters = !line.Equals(transmission.BadgerGrid(), StringComparison.OrdinalIgnoreCase);

                        return notGridParameters && notBlank;
                    })
            ];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Queue<(int BadgerNumber, string Position, string Instructions)> BadgerInstructionDispatchQueue()
        {
            var instructionSet = new Queue<(int, string, string)>();
            var commandQueue = new Queue<string>(transmission.BadgerSwarmComputationInstructionRows());
            for (var i = 1; i <= transmission.BadgerSwarmNodeCount(); i++)
            {
                var position = commandQueue.Dequeue();
                var instructions = commandQueue.Dequeue();
                var badgerNumber = i;
                instructionSet.Enqueue((badgerNumber, position, instructions));
            }

            return instructionSet;
        }
    }
}