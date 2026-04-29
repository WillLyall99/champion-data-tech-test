# Champion Data technical test


## Program Features:
- Automatic detection of table bounds.
- The robot can move in all 4 cardinal directions.
- All robot commands will be ignored until a place command has been issued.
- Multiple place commands can be issued to the robot.
- A Commands.txt file can be read to automatically issue commands before handing control back to the user.
- A user can exit the application by issuing the exit command.
- Issuing the report command will print the robots current location and direction.

## Program installation:
1. Download or clone the repository.
2. CD to the root directory of the repository using your CLI
3. Enter the following command and press enter `dotnet publish`
4. Once the build has been completed, in the same CLI window, CD to `.\bin\Release\net10.0`
5. Locate the `Commands.txt` file in the root directory of the repository and copy it to the same location as `RobotSimulator.exe`
5. Enter `RobotSimulator.exe` into the CLI window to start the program.

## Program usage:
1. On startup the program will ask whether you want to load a commands file, entering `Y` will run through all commands in the Commands.txt file. Entering `N` will not run the Commands.txt file and will instead prompt the user for an individual command to run.
2. When entering commands manually, if the robot has not been placed on the table using the `PLACE` command, then no other commands will be processed. To place the robot on the table enter the `PLACE` command, followed by a grid coordinate and a cardinal direction on where the robot should be facing. E.G `PLACE 1,2,North`
3. Once the robot has been placed on the table the following commands can be issued:
    1. `Move`: This command can only be issued if the robot has been placed on the table. It will move the robot 1 unit in the cardinal direction it is facing.
    2. `Left`: This command can only be issued if the robot has been placed on the table. It will rotate the robot 90 degrees to the left.
    3. `RIGHT`: This command can only be issued if the robot has been placed on the table. It will rotate the robot 90 degrees to the right.
    4. `REPORT`: This command can only be issued if the robot has been placed on the table. It will print the robots current position and direction into the CLI.
    5. `EXIT`: This command can be issued at any time and will shutdown the application.
4. In addition to issuing move and rotate commands, the `PLACE` command can be issued more than once to place the robot on a new point on the table.

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
- The `Commands.txt` file is located 