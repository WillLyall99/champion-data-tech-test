namespace RobotSimulator
{
    using System.Numerics;
    using System.Linq;

    class Program
    {

        // Table dimensions assuming the table is a square
        const int tableDimensions = 5;
        // Default starting positon when the console recieves an empty input
        const string defaultStartingPosition = "0,0";
        // Units to move the robot per move command
        const int moveUnitsPerMoveCommand = 1;
        // Location from where to load the commands file from
        const string commandsFileLocation = "Commands.txt";

        static void Main()
        {

            var keepConsoleRunning = true;
            string? loadcommandsFileResponse;
            string[] commandsFileCommands = [];

            do
            {
                Console.WriteLine("Would you like to load the commands file? Y/N");

                loadcommandsFileResponse = Console.ReadLine()?.ToLowerInvariant();

                switch (loadcommandsFileResponse)
                {
                    case "y":
                        commandsFileCommands = loadcommandsFile();
                        break;

                    case "n":
                        break;

                    default:
                        Console.WriteLine("Please enter Y/N in order to continue");
                        loadcommandsFileResponse = string.Empty;
                        break;

                }
            } while (loadcommandsFileResponse == string.Empty);

            // Create robot instance for later extension of multiple robots per table
            Robot currentRobot = new Robot();

            // Int for tracking progress through commands.txt
            int commandToRun = 0;

            while (keepConsoleRunning)
            {
                // String for tracking current issued command
                string currentRobotCommand;

                if (commandsFileCommands.Length == 0)
                {
                    Console.WriteLine("Input a robot command: ");
                    currentRobotCommand = Console.ReadLine()?.ToLowerInvariant() ?? "";
                }
                else
                {
                    currentRobotCommand = commandsFileCommands[commandToRun];
                }

                // Variable to split place command from coordinates and orientation
                var splitCommand = currentRobotCommand.Split(" ")[0];

                switch (splitCommand)
                {
                    case "place" or "move":
                        MoveRobot(currentRobotCommand, currentRobot);
                        break;

                    case "left" or "right":
                        RotateRobot(splitCommand, currentRobot);
                        break;

                    case "report":
                        ReportRobotLocation(currentRobot);
                        break;

                    case "exit":
                        keepConsoleRunning = false;
                        break;

                    default:
                        Console.WriteLine("Unknown command please enter a vaild robot command");
                        break;

                }

                commandToRun++;
                if (commandToRun == commandsFileCommands.Length)
                {
                    commandsFileCommands = Array.Empty<string>();
                }

            }
        }

        internal static void ReportRobotLocation(Robot currentRobot)
        {

            if (currentRobot.hasBeenPlaced)
            {
                // Convert degrees to cardinal directions
                string[] rotation = ["North", "East", "South", "West"];
                var index = currentRobot.robotRotation / 90;
                Console.WriteLine(currentRobot.robotLocation.X + "," + currentRobot.robotLocation.Y + "," + rotation[index]);
            }
        }

        internal static void RotateRobot(string rotationCommand, Robot currentRobot)
        {
            switch (rotationCommand)
            {
                case "left":
                    currentRobot.robotRotation = currentRobot.robotRotation - 90;
                    // Check if the robot has moved counter-clockwise
                    if (currentRobot.robotRotation < 360)
                    {
                        // Subtract the current negative rotation from a full rotation to keep the rotation within 360 degrees
                        currentRobot.robotRotation = 360 - Math.Abs(currentRobot.robotRotation);
                    }
                    break;
                case "right":
                    currentRobot.robotRotation = currentRobot.robotRotation + 90;
                    // Check if the robot has completed a full rotation
                    if (currentRobot.robotRotation >= 360)
                    {
                        // Reset robot rotation to 0 on completed rotation 
                        currentRobot.robotRotation = 0;
                    }
                    break;
            }
        }

        internal static void MoveRobot(string rawRobotCommand, Robot currentRobot)
        {
            // Perform seperation of the place command from table coordinates
            string[] selectedRobotCommand = rawRobotCommand.Split(" ");

            var intendedRobotLocation = new Vector2();
            int? intendedRobotRotation = null;

            if (string.Equals(selectedRobotCommand[0], "move") && currentRobot.hasBeenPlaced)
            {
                switch (currentRobot.robotRotation)
                {
                    case 0:
                        //Move the robot the default number of move units in the direction the robot is currently facing
                        intendedRobotLocation = new Vector2(currentRobot.robotLocation.X, currentRobot.robotLocation.Y + moveUnitsPerMoveCommand);
                        break;

                    case 90:
                        intendedRobotLocation = new Vector2(currentRobot.robotLocation.X + moveUnitsPerMoveCommand, currentRobot.robotLocation.Y);
                        break;

                    case 180:
                        intendedRobotLocation = new Vector2(currentRobot.robotLocation.X, currentRobot.robotLocation.Y - moveUnitsPerMoveCommand);
                        break;

                    case 270:
                        intendedRobotLocation = new Vector2(currentRobot.robotLocation.X - moveUnitsPerMoveCommand, currentRobot.robotLocation.Y);
                        break;

                    default:
                        Console.WriteLine("Unknown command please enter a vaild robot command");
                        break;

                }

                if (ValidateRobotBounds(intendedRobotLocation))
                {
                    currentRobot.robotLocation = intendedRobotLocation;
                }

            }
            else if (string.Equals(selectedRobotCommand[0], "place"))
            {
                if (selectedRobotCommand.Length > 1)
                {
                    string[] processedRobotCommand = selectedRobotCommand[1].Split(",");

                    if (processedRobotCommand.Length == 3)
                    {
                        intendedRobotLocation = ConvertStringToVector(processedRobotCommand[0] + "," + processedRobotCommand[1]);
                        switch (processedRobotCommand[2])
                        {
                            case "north":
                                intendedRobotRotation = 0;
                                break;
                            case "south":
                                intendedRobotRotation = 180;
                                break;
                            case "east":
                                intendedRobotRotation = 90;
                                break;
                            case "west":
                                intendedRobotRotation = 270;
                                break;
                            default:
                                Console.WriteLine("Enter a valid compass direction");
                                break;
                        }

                        if (ValidateRobotBounds(intendedRobotLocation))
                        {
                            currentRobot.robotLocation = intendedRobotLocation;

                            if (intendedRobotRotation != null)
                            {
                                currentRobot.robotRotation = intendedRobotRotation ?? 0;
                            }

                            if (!currentRobot.hasBeenPlaced)
                            {
                                currentRobot.hasBeenPlaced = true;
                            }
                        }
                    }
                }

            }

        }

        //Convert string values from user input into Vector2 values
        private static Vector2 ConvertStringToVector(string? input)
        {
            Vector2 conversionResult = (string.IsNullOrEmpty(input) ? defaultStartingPosition : input).Split(',').Select(s => int.Parse(s.Trim())).ToArray() switch
            {
                var v => new Vector2(v[0], v[1])
            };
            return conversionResult;
        }

        private static bool ValidateRobotBounds(Vector2 intendedRobotLocation)
        {
            var maxTableCoordinates = new Vector2(tableDimensions, tableDimensions);
            //Minimum table coordinates are assumed to always be 0,0
            var minTableCoordinates = new Vector2(0, 0);

            //Check if the proposed location is outside the min and max bounds of the table
            if (Vector2.Clamp(intendedRobotLocation, minTableCoordinates, maxTableCoordinates) != intendedRobotLocation)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static string[] loadcommandsFile()
        {
            try
            {
                return File.ReadAllLines(commandsFileLocation);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No Commands.txt file found");
                return Array.Empty<string>();
            }

        }
    };
}

