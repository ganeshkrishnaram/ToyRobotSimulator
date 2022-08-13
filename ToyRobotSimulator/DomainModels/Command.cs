using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotSimulator.DomainModels
{
    // This enumerates the Toy Robot commands for use
    // by the simulator.
    public enum Command
    {
        Place,
        Move,
        Left,
        Right,
        Report
    }
}
