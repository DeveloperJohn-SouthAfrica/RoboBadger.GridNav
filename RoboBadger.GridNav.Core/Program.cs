// See https://aka.ms/new-console-template for more information

using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RoboBadger.GridNav.Application.ConfigurationContainer;
using RoboBadger.GridNav.Application.ServiceDefinitions;
using RoboBadger.GridNav.Constraints;
using RoboBadger.GridNav.Constraints.TransmissionInterpretationExtensions;

IServiceCollection serviceCollection = new ServiceCollection();
var services = serviceCollection.ConfigureBadgerCentralCortex();
var provider = services.BuildServiceProvider();


var badgerCrawlService = provider.GetRequiredService<IBadgerCrawlService>();
var badgerSafetyEmissionService = provider.GetRequiredService<IBadgerSafetyEmissionService>();
var badgerSwarmProcessor = provider.GetRequiredService<IBadgerSwarmProcessor>();
var groundTransmissionValidator = provider.GetRequiredService<IGroundTransmissionValidator>();
string input = string.Empty;

while (true)
{
    Console.ResetColor();
    Console.Clear();
    Console.WriteLine("Welcome to RoboBadger.GridNav");
    Console.WriteLine("A little touch of pre-agentic architecture done in a rush, while having fun!");
    Console.WriteLine("It works so please remember, the application is open for addition, and closed for modification");
    Console.WriteLine("Please select your desired action:");
    Console.WriteLine("1. Run the TTD implementation of the Red Badger Developer Programming Problem (default inputs)");
    Console.WriteLine("2. Input your own transmission from ground control to the Badger Mars Base");
    Console.WriteLine("3. Extract the Grid Command from a transmission payload");
    Console.WriteLine("4. See How many robo badgers are in your transmission payload");
    Console.WriteLine("5. Examine Badger Node Swarm Queue");
    Console.WriteLine("6. Examine Transmission payload validation");
    Console.WriteLine("7. Exit");
    input = Console.ReadLine() ?? string.Empty;
    if (string.IsNullOrWhiteSpace(input))
    {
        Console.WriteLine("Please enter a valid option");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        continue;
    }

    if (input == "7")
        break;
    switch (input)
    {
        case "1":
        {
            var defaultTransmission = File.ReadAllText("DeveloperProblemFiles/RoboBadger.GroundControl.Transmission.txt");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("The default transmission is displayed below in red, once the transmission has been processed" +
                          "you will receive a relayed response from the Mars Base");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(defaultTransmission);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press any key to send the transmission");
            var result = await badgerSwarmProcessor.ProcessTransmission(defaultTransmission);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Transmission sent. Awaiting response...");
            Console.WriteLine("Transmission result: " + Environment.NewLine + result);
            Console.WriteLine("Press any key to return to the main menu");
            Console.ReadKey();
            break;
        }
        case "2":
        {
            Console.ResetColor();
            Console.WriteLine("Please enter the path to your transmission file:");
            var userTransmission = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(userTransmission))
            {
                Console.WriteLine("Invalid transmission. Press any key to return to the main menu...");
                Console.ReadKey();
                break;
            }
            if (!File.Exists(userTransmission))
            {
                Console.WriteLine("File not found. Press any key to return to the main menu...");
                Console.ReadKey();
                break;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("The transmission is displayed below in red, once the transmission has been processed" +
                              " you will receive a relayed response from the Mars Base");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(File.ReadAllText(userTransmission));
            
            var userResult = await badgerSwarmProcessor.ProcessTransmission(File.ReadAllText(userTransmission));
            Console.WriteLine("Transmission sent. Awaiting response...");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Transmission result: " + Environment.NewLine + userResult);
            Console.WriteLine("Press any key to return to the main menu");
            Console.ReadKey();
            break;
        }
        case "3":
        {
            Console.ResetColor();
            Console.WriteLine("Please enter the path to your transmission file or enter 'Default':");
            var userTransmission = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(userTransmission))
            {
                Console.WriteLine("Invalid transmission. Press any key to return to the main menu...");
                Console.ReadKey();
                break;
            }

            if (userTransmission.Contains("Default", StringComparison.OrdinalIgnoreCase))
            {
                userTransmission = "DeveloperProblemFiles/RoboBadger.GroundControl.Transmission.txt";
            }
            else
            {
                userTransmission = userTransmission.Trim();
                if (!File.Exists(userTransmission))
                {
                    Console.WriteLine("File not found. Press any key to return to the main menu...");
                    Console.ReadKey();
                    break;
                }
                
                userTransmission = File.ReadAllText(userTransmission);
            }

            var extract = userTransmission.BadgerGrid();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(extract);
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
            break;
        }
        case "4":
        {
            Console.ResetColor();
            Console.WriteLine("Please enter the path to your transmission file or enter 'Default':");
            var userTransmission = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(userTransmission))
            {
                Console.WriteLine("Invalid transmission. Press any key to return to the main menu...");
                Console.ReadKey();
                break;
            }

            if (userTransmission.Contains("Default", StringComparison.OrdinalIgnoreCase))
            {
                userTransmission = File.ReadAllText("DeveloperProblemFiles/RoboBadger.GroundControl.Transmission.txt");
            }
            else
            {
                userTransmission = userTransmission.Trim();
                if (!File.Exists(userTransmission))
                {
                    Console.WriteLine("File not found. Press any key to return to the main menu...");
                    Console.ReadKey();
                    break;
                }
                userTransmission = File.ReadAllText(userTransmission);
            }

            var count = userTransmission.BadgerSwarmNodeCount();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Number of robo badgers in your transmission: " + count);
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
            break;
        }
        case "5":
        {
            Console.ResetColor();
            Console.WriteLine("Please enter the path to your transmission file or enter 'Default':");
            var userTransmission = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(userTransmission))
            {
                Console.WriteLine("Invalid transmission. Press any key to return to the main menu...");
                Console.ReadKey();
                break;
            }

            if (userTransmission.Contains("Default", StringComparison.OrdinalIgnoreCase))
            {
                userTransmission = File.ReadAllText("DeveloperProblemFiles/RoboBadger.GroundControl.Transmission.txt");
            }
            else
            {
                userTransmission = userTransmission.Trim();
                if (!File.Exists(userTransmission))
                {
                    Console.WriteLine("File not found. Press any key to return to the main menu...");
                    Console.ReadKey();
                    break;
                }
                userTransmission = File.ReadAllText(userTransmission);
            }

            var queue = userTransmission.BadgerInstructionDispatchQueue();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Badger swarm queue:");
            Console.WriteLine(JsonConvert.SerializeObject(queue));
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
            break;
        }
        case "6":
        {
            Console.ResetColor();
            Console.WriteLine("Please enter your transmission to the Badger Mars Base to experiment with grid sizes or enter 'Default':");
            var userTransmission = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(userTransmission))
            {
                Console.WriteLine("Invalid transmission. Press any key to return to the main menu...");
                Console.ReadKey();
                break;
            }

            if (userTransmission.Contains("Default", StringComparison.OrdinalIgnoreCase))
            {
                userTransmission = File.ReadAllText("DeveloperProblemFiles/RoboBadger.GroundControl.Transmission.txt");
            }
            else
            {
                userTransmission = userTransmission.Trim();
                if (!File.Exists(userTransmission))
                {
                    Console.WriteLine("File not found. Press any key to return to the main menu...");
                    Console.ReadKey();
                    break;
                }
                userTransmission = File.ReadAllText(userTransmission);
            }   

            var isValid = groundTransmissionValidator.RunTransmissionValidation(userTransmission);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Validation results:");
            foreach (var result in isValid)
            {
                Console.WriteLine($"Valid: {result.Valid}, Message: {result.Message}");
            }
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
            break;
        }

    }
}