# ToyRobotSimulator
<h3>Application simulating the placement and movement of Toy Robot</h3>
This is .Net 6 based Console Application to simulate the movement of toy robot on a tabletop. BDD based Test driven development being leveraged for this application. 
<br><h4>Design Patterns Used</h4>
<b>Dependency Injection :</b> The Business components or services such as Simulator,ToyRobot,RawRequestTransformer,ToyBoard adheres to the Dependency Injection principle to enable centralized management of their lifetime scope and to assist in test driven development . <br><br>
<b>Interface Segregation :</b> Interfaces are created and segregated for different Business components or services such as Simulator,ToyRobot,RawRequestTransformer,ToyBoard. This is in adherence to Interface Segregation Principle<br><br>
<b>Simulator</b> Component handles the incoming placement and movement commands of the Robot<br><br>
<b>ToyRobot</b> Component handles the position and direction of movement of ToyRobot<br><br>
<b>RawRequestTransform</b> Component transforms the raw input command request of the user to appropriate valid placement and movement commands that can be interpreted by ToyRobot<br><br>
Domain models persisting the Position,Direction and ToyRobot Commands are implemented<br><br>
<h4>Installing and Running</h4>
The application runs in a single executable file which can be opened by double clicking it. 
<h4>Test Case <h4>
  BDD Test cases are implemented for the Simulator component to ensure the testing is more of a Use Case driven approach. 
<h4>NOTE</h4>
  The Lifetimescope of the core ToyRoboSimulator components are currently kept as Singleton Instance. Both the AddScoped/AddSingleton will work for the current application scenario. Currently used the Singleton scope for the purpose of this exercise.
