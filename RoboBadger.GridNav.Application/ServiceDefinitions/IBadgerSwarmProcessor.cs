namespace RoboBadger.GridNav.Application.ServiceDefinitions;

public interface IBadgerSwarmProcessor
{
    Task<string> ProcessTransmission(string groundControlPayload);
    
    Task<List<(string X, string Y, string Heading, string? Status)>> ProcessDispatchQueue(
        string groundControlPayload, 
        Queue<(int BadgerNumber, string Position, string Instructions)> swarmOrchestrationQueue,
        List<(string X, string Y, string Heading, string? Status)> results);
}