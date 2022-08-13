using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotSimulator.Contracts.Simulator
{
    /// <summary>
    /// Contract facilitating the execution of Toy Robot Simulator commands and generating report
    /// </summary>
    public interface ISimulator
    {
        public string ProcessCommand(string[] input);
        public string GetReport();
    }
}
