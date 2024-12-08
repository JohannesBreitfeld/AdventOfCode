var path = Path.Combine("..", "..", "..", "..", "input08.txt");
var input = File.ReadAllLines(path);

var cols = input[0].Length;
var rows = input.Length;

var antennas = GetAntennas(input);

var antinodes = GetAntinodes2(antennas, cols, rows);

Console.WriteLine(antinodes.Count);

//for (int i = 0; i < rows; i++)
//{
//    for (int j = 0; j < cols; j++)
//    {
//        if(antinodes.Contains((j, i)))
//        {
//            Console.Write('#');
//        }
//        else
//        {
//            Console.Write('-');
//        }
//    }
//    Console.WriteLine();
//}

static Dictionary<char, List<(int, int)>> GetAntennas(string[] map)
{
	Dictionary<char, List<(int, int)>> antennas = new();

    for (int i = 0; i < map.Length; i++)
	{
		for (int j = 0; j < map[0].Length; j++)
		{
			if (map[i][j] == '.')
			{
				continue;
			}
			if (!antennas.ContainsKey(map[i][j]))
			{
				antennas[map[i][j]] = new List<(int, int)>();
			}
			antennas[map[i][j]].Add((i, j));
		}
	}
	return antennas;
}

static HashSet<(int, int)> GetAntinodes(
    Dictionary<char, List<(int, int)>> antennas,
    int columns,
    int rows)
{
    var antinodes = new HashSet<(int, int)>();

    foreach (var frequency in antennas)
    {
        var positions = frequency.Value;

        for (int i = 0; i < positions.Count; i++)
        {
            for (int j = i+1; j < positions.Count; j++)
            {
                var A1 = positions[i];
                var A2 = positions[j];

                var horizontalDiff = Math.Abs(A1.Item1 - A2.Item1);
                var verticalDiff = Math.Abs(A1.Item2 - A2.Item2);

                int antinode1_x = 0;
                int antinode1_y = 0;

                int antinode2_x = 0;
                int antinode2_y = 0;

                if (A1.Item1 > A2.Item1)
                {
                    antinode1_x = A1.Item1 + horizontalDiff;
                    antinode2_x = A2.Item1 - horizontalDiff;
                }
                else if(A1.Item1 == A2.Item1)
                {
                    antinode1_x = A1.Item1;
                    antinode2_x = A1.Item1;
                }
                else
                {
                    antinode1_x = A1.Item1 - horizontalDiff;
                    antinode2_x = A2.Item1 + horizontalDiff;
                }

                if (A1.Item2 > A2.Item2)
                {
                    antinode1_y = A1.Item2 + verticalDiff;
                    antinode2_y = A2.Item2 - verticalDiff;
                }
                else if (A1.Item2 == A2.Item2)
                {
                    antinode1_y = A1.Item2;
                    antinode2_y = A1.Item2;
                }
                else
                {
                    antinode1_y = A1.Item2 - verticalDiff;
                    antinode2_y = A2.Item2 + verticalDiff;
                }

                if (IsInBounds((antinode1_x, antinode1_y), columns, rows))
                {
                    antinodes.Add((antinode1_x, antinode1_y));
                }

                if (IsInBounds((antinode2_x, antinode2_y), columns, rows))
                {
                    antinodes.Add((antinode2_x, antinode2_y));
                }
            }
        }
    }

    return antinodes;
}

static bool IsInBounds((int, int) position, int columns, int rows)
{
    return position.Item1 >= 0 && position.Item1 < columns &&
           position.Item2 >= 0 && position.Item2 < rows;
}

static HashSet<(int, int)> GetAntinodes2(
    Dictionary<char, List<(int, int)>> antennas,
    int columns,
    int rows)
{
    var antinodes = new HashSet<(int, int)>();

    foreach (var frequency in antennas)
    {
        var positions = frequency.Value;

        for (int i = 0; i < positions.Count; i++)
        {
            for (int j = i + 1; j < positions.Count; j++)
            {
                var A1 = positions[i];
                var A2 = positions[j];

                int antinode1_x = A1.Item1;
                int antinode1_y = A1.Item2;

                int antinode2_x = A2.Item1;
                int antinode2_y = A2.Item2;

                antinodes.Add((antinode1_x, antinode1_y));
                antinodes.Add((antinode2_x, antinode2_y));

                while (IsInBounds((antinode1_x, antinode1_y), columns, rows)
                    || IsInBounds((antinode2_x, antinode2_y), columns, rows))
                {

                    var horizontalDiff = Math.Abs(A1.Item1 - A2.Item1);
                    var verticalDiff = Math.Abs(A1.Item2 - A2.Item2);

                    if (A1.Item1 > A2.Item1)
                    {
                        antinode1_x +=  horizontalDiff;
                        antinode2_x -=  horizontalDiff;
                    }
                    else if (A1.Item1 == A2.Item1)
                    {
                        antinode1_x = A1.Item1;
                        antinode2_x = A1.Item1;
                    }
                    else
                    {
                        antinode1_x -= horizontalDiff;
                        antinode2_x += horizontalDiff;
                    }

                    if (A1.Item2 > A2.Item2)
                    {
                        antinode1_y += verticalDiff;
                        antinode2_y -= verticalDiff;
                    }
                    else if (A1.Item2 == A2.Item2)
                    {
                        antinode1_y = A1.Item2;
                        antinode2_y = A1.Item2;
                    }
                    else
                    {
                        antinode1_y -= verticalDiff;
                        antinode2_y += verticalDiff;
                    }

                    if (IsInBounds((antinode1_x, antinode1_y), columns, rows))
                    {
                        antinodes.Add((antinode1_x, antinode1_y));
                    }

                    if (IsInBounds((antinode2_x, antinode2_y), columns, rows))
                    {
                        antinodes.Add((antinode2_x, antinode2_y));
                    }
                }
            }
        }
    }

    return antinodes;
}

