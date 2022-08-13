using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobotSimulator.DomainModels;

namespace ToyRobotSimulator.Contracts.ToyBoard
{
    /// <summary>
    /// This Contract enable returning true or false based on whether the position of the robot is within the board
    /// </summary>
    public interface IToyBoard
    {
        
        bool IsValidPosition(Position position);
    }
}
