using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobotSimulator.Contracts.TransformRawRequestToCommands;
using ToyRobotSimulator.DomainModels;

namespace ToyRobotSimulator.Services
{
    /// <summary>
    /// Transforms the Raw Input Request to the valid Placement and Movement Commands that can be interpreted by Toy Robot Simulator
    /// </summary>
    public class RawRequestTransformer : IRawRequestTransformer
    {
        // Number of parameters provided for "PLACE" Command. (X,Y,F)
        private const int ParameterCount = 3;

        // Number of raw input items expected from user.
        private const int CommandInputCount = 2;
        /// <summary>
        /// Validates the Raw Input Request to determine whether the given Command  is valid
        /// </summary>
        /// <param name="rawInput"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Command TransformToPlaceCommand(string[] rawInput)
        {
            if (!Enum.TryParse(rawInput[0], true, out Command command))
                throw new ArgumentException("Sorry, your command was not recognised. Please try again using the following format: PLACE X,Y,F|MOVE|LEFT|RIGHT|REPORT");
            return command;
        }
        /// <summary>
        /// Transforms the Raw Input Request to Sequence Of Commands
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public CommandSequence TransformToSequenceOfCommands(string[] input)
        {
           
            // Checks that Place command is followed by valid command parameters (X,Y and F toy's face direction).
            if (input.Length != CommandInputCount)
                throw new ArgumentException("Incomplete command. Please ensure that the PLACE command is using format: PLACE X,Y,F");

            // Checks that three command parameters are provided for the PLACE command.   
            var commandParams = input[1].Split(',');
            if (commandParams.Length != ParameterCount)
                throw new ArgumentException("Incomplete command. Please ensure that the PLACE command is using format: PLACE X,Y,F");

            // Checks the direction which the toy is going to be facing.
            if (!Enum.TryParse(commandParams[commandParams.Length - 1], true, out Direction direction))
                throw new ArgumentException("Invalid direction. Please select from one of the following directions: NORTH|EAST|SOUTH|WEST");

            var x = Convert.ToInt32(commandParams[0]);
            var y = Convert.ToInt32(commandParams[1]);
            var position = new Position(x, y);

            return new CommandSequence(position, direction);
        }
    }
}
