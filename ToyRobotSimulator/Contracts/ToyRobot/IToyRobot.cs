using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobotSimulator.DomainModels;

namespace ToyRobotSimulator.Contracts.ToyRobot
{
    /// <summary>
    /// Handles the Position and Direction Movement of the Toy Robot
    /// </summary>
    public interface IToyRobot
    {
        Direction Direction { get; set; }
        Position Position { get; set; }

        // Sets the toy's position and direction.
        void Place(Position position, Direction direction);

        // Checks the next position of the toy based on the direction it's currently facing.
        Position GetNextPosition();

        // Rotates the direction of the toy 90 degrees to the left.
        void RotateLeft();

        // Rotates the direction of the toy 90 degrees to the right.
        void RotateRight();

        // Checks the new direction of the toy. The new direction is based on current direction and the rotation command (LEFT - Right)
        // identifies the side which the toy should be rotated on Left is -1 Right is 1      
        void Rotate(int rotationNumber);
    }
}
