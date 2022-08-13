using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobotSimulator.Contracts.Simulator;
using ToyRobotSimulator.Contracts.ToyBoard;
using ToyRobotSimulator.Contracts.ToyRobot;
using ToyRobotSimulator.Contracts.TransformRawRequestToCommands;
using ToyRobotSimulator.DomainModels;

namespace ToyRobotSimulator.Services
{
    /// <summary>
    /// Process the Toy Robot Command Sequences as part of simulation and generates the report
    /// </summary>
    public class Simulator : ISimulator
    {
        private readonly IToyRobot _toyRobot;
        private readonly IToyBoard _toyBoard;
        private readonly IRawRequestTransformer _rawRequestTransformer;
        
        public Simulator(IToyRobot toyRobot, IToyBoard toyBoard, IRawRequestTransformer rawRequestTransformer)
        {
            _toyRobot = toyRobot;
            _toyBoard = toyBoard;
            _rawRequestTransformer = rawRequestTransformer;
        }
        /// <summary>
        /// Process the raw sequence of Toy Robot Place and Movement Commands
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ProcessCommand(string[] input)
        {
            var command = _rawRequestTransformer.TransformToPlaceCommand(input);
            if (command != Command.Place && _toyRobot.Position == null) return string.Empty;

            switch (command)
            {
                case Command.Place:
                    var commandSequence = _rawRequestTransformer.TransformToSequenceOfCommands(input);
                    if (_toyBoard.IsValidPosition(commandSequence.Position))
                        _toyRobot.Place(commandSequence.Position, commandSequence.Direction);
                    break;
                case Command.Move:
                    var newPosition = _toyRobot.GetNextPosition();
                    if (_toyBoard.IsValidPosition(newPosition))
                        _toyRobot.Position = newPosition;
                    break;
                case Command.Left:
                    _toyRobot.RotateLeft();
                    break;
                case Command.Right:
                    _toyRobot.RotateRight();
                    break;
                case Command.Report:
                    return GetReport();
            }
            return string.Empty;
        }
        /// <summary>
        /// Generates the report indicating the current position and direction of Toy Robot 
        /// </summary>
        /// <returns></returns>
        public string GetReport() => $"Output: {_toyRobot.Position.X},{_toyRobot.Position.Y},{_toyRobot.Direction.ToString().ToUpper()}";
    }
}
