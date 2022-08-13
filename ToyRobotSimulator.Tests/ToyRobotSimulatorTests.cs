using NUnit.Framework;
using ToyRobotSimulator.Contracts.Simulator;
using ToyRobotSimulator.Contracts.ToyBoard;
using ToyRobotSimulator.Contracts.ToyRobot;
using ToyRobotSimulator.Contracts.TransformRawRequestToCommands;
using ToyRobotSimulator.DomainModels;
using ToyRobotSimulator.Services;

namespace ToyRobotSimulator.Tests
{
    /// <summary>
    /// Simulator component will be tested with the Use Case Driven Test Cases Approach in line with BDD Principles
    /// Fake Objects not required at the current scope. If any data store introduced in future for data persistence,
    /// appropriate mock/fake objects can be introduced 
    /// </summary>
    public class ToyRobotSimulatorTests
    {
        private IToyBoard _toyBoard;
        private IRawRequestTransformer _rawRequestTransformer;
        private IToyRobot _toyRobot;
        private ISimulator _subject;
                
        [Test]
        public void GivenAnIncomingValidRobotPlaceCommand_WhenPreparingToPlaceToyRobot_ThenToyRobotPlacementShouldBeCorrect()
        {
            //arrange
            PrepareToyRobotCommandRequest();

            //act
            _subject.ProcessCommand("PLACE 1,4,EAST".Split(' '));
            
            //assert
            Assert.AreEqual(1, _toyRobot.Position.X);
            Assert.AreEqual(4, _toyRobot.Position.Y);
            Assert.AreEqual(Direction.East, _toyRobot.Direction);

        }
        [Test]
        public void GivenAnIncomingInValidRobotPlaceCommand_WhenPreparingToPlaceToyRobot_ThenToyRobotPlacementCommandShouldBeIgnoredAndNotExecuted()
        {
            //arrange
            PrepareToyRobotCommandRequest();

            //act
            _subject.ProcessCommand("PLACE 9,7,EAST".Split(' '));

            //assert
            Assert.IsNull(_toyRobot.Position);

        }
        [Test]
        public void GivenAnIncomingValidRobotMovementCommand_WhenPreparingToMoveToyRobot_ThenToyRobotMovementShouldBeCorrect()
        {
            //arrange
            PrepareToyRobotCommandRequest();

            // act
            _subject.ProcessCommand("PLACE 3,2,SOUTH".Split(' '));
            _subject.ProcessCommand("MOVE".Split(' '));

            // assert
            Assert.AreEqual("Output: 3,1,SOUTH", _subject.GetReport());

        }
        [Test]
        public void GivenAnIncomingInValidRobotMovementCommand_WhenPreparingToMoveToyRobot_ThenToyRobotMovementCommandShouldBeIgnoredToPreventFallOutOfBoard()
        {
            //arrange
            PrepareToyRobotCommandRequest();

            // act
            _subject.ProcessCommand("PLACE 2,2,NORTH".Split(' '));
            _subject.ProcessCommand("MOVE".Split(' '));
            _subject.ProcessCommand("MOVE".Split(' '));
            // if the toy robot goes out of the board it ignores the command to prevent falling out of the board
            _subject.ProcessCommand("MOVE".Split(' '));

            // assert
            Assert.AreEqual("Output: 2,4,NORTH", _subject.GetReport());

        }
        [Test]
        public void GivenAnIncomingValidRobotMovementCommand_WhenPreparingToMoveToyRobot_ThenToyRobotMovementShouldBeCorrectAndValidReportGenerated()
        {
            //arrange
            PrepareToyRobotCommandRequest();

            // act
            _subject.ProcessCommand("PLACE 3,3,WEST".Split(' '));
            _subject.ProcessCommand("MOVE".Split(' '));
            _subject.ProcessCommand("MOVE".Split(' '));
            _subject.ProcessCommand("LEFT".Split(' '));
            _subject.ProcessCommand("MOVE".Split(' '));
            var output = _subject.ProcessCommand("REPORT".Split(' '));

            // assert
            Assert.AreEqual("Output: 1,2,SOUTH", output);

        }
        private void PrepareToyRobotCommandRequest()
        {
            _toyBoard = new ToyBoard(5, 5);
            _rawRequestTransformer = new RawRequestTransformer();
            _toyRobot = new ToyRobot();
            _subject = new Simulator(_toyRobot, _toyBoard, _rawRequestTransformer);
        }
        
    }
}