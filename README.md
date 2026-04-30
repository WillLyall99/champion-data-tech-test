# Champion Data technical test

## Program Features:
- Automatic detection of table bounds.
- The robot can move in all 4 cardinal directions.
- All robot commands will be ignored until a "place" command has been issued.
- Multiple "place" commands can be issued to the robot.
- A Commands.txt file can be read to automatically issue commands before handing control back to the user.
- A user can exit the application by issuing the "exit" command.
- Issuing the "report" command will print the robots current location and direction.

## Program installation:
- Clone/download this repository and ensure you have the DotNet10 SDK installed.
- These commands assume you are on Windows, the application will compile and run on Linux, but steps will differ slightly.
- All steps below are via the Windows `cli`

1. Change into the root directory of the repository. 
    - `cd champion-data-tech-test`
2. Enter the following command to compile the code.
    - `dotnet publish`
3. Assuming a successful build, change into the `RobotSimulator` directory
   - `cd RobotSimulator`
5. Copy the `Commands.txt` file to the application directory.
   - `copy Commands.txt .\bin\Release\net10.0\` 
6. Change into the application directory.
   - `cd .\bin\Release\net10.0`
6. Run the `RobotSimulator` binary to start the program.
   - `RobotSimulator.exe`

## Program usage:
1. On startup the program will ask whether you want to load a commands file.
   - Entering `Y` will run through all commands in the Commands.txt file.
   - Entering `N` will not run the Commands.txt file and will instead prompt the user for an individual command to run.
2. If you do not load the `Commands.txt` file on startup.
   - You must use the `PLACE` command first to position the robot on the table, otherwise no commands will be processed.
   - To place the robot on the table enter the `PLACE` command, followed by a grid coordinate and a cardinal direction for where the robot should be facing. E.G `PLACE 1,2,North`

3. Once the robot has been placed on the table the following commands can be issued:
    1. `Move` 
       - This command can only be issued if the robot has been placed on the table. It will move the robot 1 unit in the cardinal direction it is facing.
    2. `Left`
       - This command can only be issued if the robot has been placed on the table. It will rotate the robot 90 degrees to the left.
    3. `RIGHT`
       - This command can only be issued if the robot has been placed on the table. It will rotate the robot 90 degrees to the right.
    4. `REPORT`
       - This command can only be issued if the robot has been placed on the table. It will print the robots current position and direction into the CLI.
    5. `PLACE`
       - This command can be issued more than once to place the robot at a new point on the table, E.G `PLACE 3,2,East`
    5. `EXIT`
       - This command can be issued at any time and will shut down the application.


## Commands.txt file format:
### An Example Commands.txt
```
place 0,0,north
report
move
right
report
```

- All commands can be issued within the `Commands.txt` file. However any commands issued before a valid place command will be ignored.
- Each command should be placed as a new line.
- The `Commands.txt` file is located in the "RobotSimulator" root folder.