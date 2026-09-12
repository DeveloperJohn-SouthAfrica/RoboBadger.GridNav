namespace RoboBadger.GridNav.Application.ServiceDefinitions;

/// <summary>
/// The badger swarm processor interface
/// </summary>
public interface IBadgerSwarmProcessor
{
    /// <summary>
    /// Processes the transmission using the specified ground control payload
    /// </summary>
    /// <param name="groundControlPayload">The ground control payload</param>
    /// <returns>A task containing the string</returns>
    Task<string> ProcessTransmission(string groundControlPayload);
    
    /// <summary>
    /// Processes the dispatch queue using the specified ground control payload
    /// </summary>
    /// <param name="groundControlPayload">The ground control payload</param>
    /// <param name="swarmOrchestrationQueue">The swarm orchestration queue</param>
    /// <param name="results">The results</param>
    /// <returns>A task containing a list of string x and string y and string heading and string status</returns>
    Task<List<(string X, string Y, string Heading, string? Status)>> ProcessDispatchQueue(
        string groundControlPayload, 
        Queue<(int BadgerNumber, string Position, string Instructions)> swarmOrchestrationQueue,
        List<(string X, string Y, string Heading, string? Status)> results);
}