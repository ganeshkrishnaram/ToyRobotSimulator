using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotSimulator.DomainModels
{
    // Stores the entire command sequence given as raw input request by the user 
    public class CommandSequence
    {
        public Position Position { get; set; }
        public Direction Direction { get; set; }

        public CommandSequence(Position position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }
    }
}
