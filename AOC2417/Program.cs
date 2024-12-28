using AOC2417;

var partOne = new PartOne();
partOne.Excecute();

Console.WriteLine();

var partTwo = new PartTwo();
partTwo.Solve(partTwo.expectedOutput.Length - 1, 0);

Console.WriteLine(partTwo.bestA);
