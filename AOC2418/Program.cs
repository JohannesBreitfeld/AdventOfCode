using AOC2418;

//var partOne = new PartOne();
//Console.WriteLine(partOne.FindBestPath());

var partTwo = new PartTwo();

var lastByte = partTwo.Solve();

Console.WriteLine($"{lastByte.x},{lastByte.y}");


//for (int i = 0; i < partOne.map.GetLength(0); i++)
//{
//	for (int j = 0; j < partOne.map.GetLength(1); j++)
//	{
//		Console.Write(partOne.map[i, j]);
//	}
//    Console.WriteLine();
//}