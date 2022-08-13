using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobotSimulator.DomainModels;

namespace ToyRobotSimulator.Contracts.TransformRawRequestToCommands
{
    // Interface to transform the raw input from the user to valid sequence of Toy Robot Placement Commands.
    public interface IRawRequestTransformer
    {
        // Compares the first element in rawInput as a valid PLACE command
        Command TransformToPlaceCommand(string[] rawInput);

        // This extracts the sequence of command parameters followed by the initial PLACE Command
        CommandSequence TransformToSequenceOfCommands(string[] input);
    }
}
