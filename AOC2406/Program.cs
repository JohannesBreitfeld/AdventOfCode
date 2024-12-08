using System.Diagnostics;
using System.Drawing;
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
var path = Path.Combine("..", "..", "..", "..", "input06.txt");
var input = File.ReadAllLines(path);

var visitedPositions = GetVisitedPositions(input);
Console.WriteLine(visitedPositions.Count);

//Problem 2
char[,] map = new char[input.Length, input[0].Length];
for (int i = 0; i < input.Length; i++)
{
    char[] row = input[i].ToArray();
    for (int j = 0; j < row.Length; j++)
    {
        map[i, j] = row[j];
    }
}

Console.WriteLine(CountLoopPositions(map, visitedPositions, input));
stopwatch.Stop();
Console.WriteLine($"{stopwatch.ElapsedMilliseconds}");

static (Point, int) GetStartPosition(string[] map)
{
    int rows = map.Length;
    int cols = map[0].Length;

    int startX = 0;
    int startY = 0;
    int direction = 0;
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            if (map[i][j] == '^')
            {
                startX = i;
                startY = j;
                direction = 0;
                break;
            }
            else if (map[i][j] == '>')
            {
                startX = i;
                startY = j;
                direction = 1;
                break;
            }
            else if (map[i][j] == 'v')
            {
                startX = i;
                startY = j;
                direction = 2;
                break;
            }
            else if (map[i][j] == '<')
            {
                startX = i;
                startY = j;
                direction = 3;
                break;
            }
        }
    }
    var startposition = new Point(startX, startY);
    return (startposition, direction);
}


static HashSet<(int, int)> GetVisitedPositions(string[] map)
{
    int[] dx = { -1, 0, 1, 0 };
    int[] dy = { 0, 1, 0, -1 };

    int rows = map.Length;
    int cols = map[0].Length;

    var start = GetStartPosition(map);

    var visitedPositions = new HashSet<(int, int)>();
    int x = start.Item1.X;
    int y = start.Item1.Y;
    int direction = start.Item2;

    while (true)
    {
        visitedPositions.Add((x, y));

        int nextX = x + dx[direction];
        int nextY = y + dy[direction];

        if (nextX < 0 || nextX >= rows || nextY < 0 || nextY >= cols)
        {
            break;
        }
        if (map[nextX][nextY] == '#')
        {
            direction = (direction + 1) % 4;
        }
        else
        {
            x = nextX;
            y = nextY;
        }
    }
    return visitedPositions;
}

static int CountLoopPositions(char[,] map, HashSet<(int, int)> visitedPositions, string[] mapforstart)
{
    var start = GetStartPosition(mapforstart);

    int startX = start.Item1.X;
    int startY = start.Item1.Y;
    int direction = start.Item2;
    int loopCount = 0;

    foreach (var position in visitedPositions)
    {
        int x = position.Item1;
        int y = position.Item2;

        
            map[x, y] = '#';

            if (IsLoop(map, startX, startY, direction))
            {
                loopCount++;
            }
            map[x, y] = '.';
        
    }
    return loopCount;
}

static bool IsLoop(char[,] map, int startX, int startY, int Direction)
{
    int[] dx = { -1, 0, 1, 0 };
    int[] dy = { 0, 1, 0, -1 };

    int rows = map.GetLength(0);
    int cols = map.GetLength(1);

    int x = startX;
    int y = startY;
    int direction = Direction;

    int counter = 0;

    while (true)
    {
        if (counter == rows*cols*4)
        {
            return true;
        }

        int nextX = x + dx[direction];
        int nextY = y + dy[direction];

        if (nextX < 0 || nextX >= rows || nextY < 0 || nextY >= cols)
        {
            return false;
        }
        if (map[nextX, nextY] == '#')
        {
            direction = (direction + 1) % 4;
        }
        else
        {
            x = nextX;
            y = nextY;
        }
        counter++;
    }
}



