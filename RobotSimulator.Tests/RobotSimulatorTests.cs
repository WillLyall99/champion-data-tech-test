using System.Numerics;

namespace RobotSimulator.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void RotateRobot_WrapsToZero()
    {
        var robot = new Robot
        {
            robotRotation = 270,
            hasBeenPlaced = true
        };

        Program.RotateRobot("right", robot);

        Assert.That(robot.robotRotation, Is.EqualTo(0));
    }

    [Test]
    public void RotateRobot_PreventsNegativeRotation()
    {
        var robot = new Robot
        {
            robotRotation = 0,
            hasBeenPlaced = true
        };

        Program.RotateRobot("left", robot);

        Assert.That(robot.robotRotation, Is.EqualTo(270));
    }

    [Test]
    public void RotateRobot_MoveNorth()
    {
        var robot = new Robot
        {
            robotLocation = new Vector2(1, 1),
            robotRotation = 0,
            hasBeenPlaced = true
        };

        Program.MoveRobot("move", robot);

        Assert.That(robot.robotLocation, Is.EqualTo(new Vector2(1, 2)));
    }

    [Test]
    public void RotateRobot_MoveSouth()
    {
        var robot = new Robot
        {
            robotLocation = new Vector2(1, 1),
            robotRotation = 180,
            hasBeenPlaced = true
        };

        Program.MoveRobot("move", robot);

        Assert.That(robot.robotLocation, Is.EqualTo(new Vector2(1, 0)));
    }

    [Test]
    public void RotateRobot_MoveEast()
    {
        var robot = new Robot
        {
            robotLocation = new Vector2(1, 1),
            robotRotation = 90,
            hasBeenPlaced = true
        };

        Program.MoveRobot("move", robot);

        Assert.That(robot.robotLocation, Is.EqualTo(new Vector2(2, 1)));
    }

    [Test]
    public void RotateRobot_MoveWest()
    {
        var robot = new Robot
        {
            robotLocation = new Vector2(1, 1),
            robotRotation = 270,
            hasBeenPlaced = true
        };

        Program.MoveRobot("move", robot);

        Assert.That(robot.robotLocation, Is.EqualTo(new Vector2(0, 1)));
    }

    [Test]
    public void RotateRobot_MoveOutOfBounds()
    {
        var robot = new Robot
        {
            robotLocation = new Vector2(0, 0),
            robotRotation = 180,
            hasBeenPlaced = true
        };

        Program.MoveRobot("move", robot);

        Assert.That(robot.robotLocation, Is.EqualTo(new Vector2(0, 0)));
    }

    [Test]
    public void RotateRobot_MoveDoesNotPlaceRobot()
    {
        var robot = new Robot
        {
            robotLocation = new Vector2(0, 0),
            robotRotation = 0,
            hasBeenPlaced = false
        };

        Program.MoveRobot("move", robot);

        Assert.That(robot.hasBeenPlaced, Is.EqualTo(false));
    }

    [Test]
    public void RotateRobot_PlaceRobot()
    {
        var robot = new Robot
        {
            robotLocation = new Vector2(0, 0),
            robotRotation = 0,
            hasBeenPlaced = false
        };

        Program.MoveRobot("place 1,1,east", robot);

        Assert.That(robot.hasBeenPlaced, Is.EqualTo(true));
        Assert.That(robot.robotLocation, Is.EqualTo(new Vector2(1, 1)));
        Assert.That(robot.robotRotation, Is.EqualTo(90));
    }

}
