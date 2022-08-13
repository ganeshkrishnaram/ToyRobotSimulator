// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToyRobotSimulator.Contracts.Simulator;
using ToyRobotSimulator.Contracts.ToyBoard;
using ToyRobotSimulator.Contracts.ToyRobot;
using ToyRobotSimulator.Contracts.TransformRawRequestToCommands;
using ToyRobotSimulator.Services;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.AddSingleton<IToyBoard>(new ToyBoard(5,5))
                .AddSingleton<IRawRequestTransformer, RawRequestTransformer>()
                .AddSingleton<IToyRobot, ToyRobot>()
                .AddSingleton<ISimulator, Simulator>()
                )
    .Build();


const string userManual =
@"  **************************************
  **************************************
  **                                  **
  **        TOY ROBOT SIMULATOR       **
  **                                  **
  **************************************
  **************************************

     Welcome!

  1: Place the toy on a 5 x 5 grid
     using the following command:

     PLACE X,Y,F (Where X and Y are integers and F 
     must be either NORTH, SOUTH, EAST or WEST)

  2: When the toy is placed, have at go at using
     the following commands.
                
     REPORT – Shows the current status of the toy. 
     LEFT   – turns the toy 90 degrees left.
     RIGHT  – turns the toy 90 degrees right.
     MOVE   – Moves the toy 1 unit in the facing direction.
     EXIT   – Closes the toy Simulator.
";
var stopApplication = false;
Console.WriteLine(userManual);

do
{
    var command = Console.ReadLine();
    if (command == null) continue;

    if (command.Equals("EXIT"))
        stopApplication = true;
    else
    {
        try
        {
            ISimulator? simulator = host.Services.GetService<ISimulator>();
            if (simulator != null)
            {
                string? output = simulator.ProcessCommand(command.Split(' '));
                if (!string.IsNullOrEmpty(output))
                    Console.WriteLine(output);
            }
            else
                Console.WriteLine("System Error. We will notify as soon as the issue is resolved");
                //Ideally, we will capture and log the exception for determining the root cause.
                //User friendly message to be returned back to the console 
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
} while (!stopApplication);

await host.RunAsync();


