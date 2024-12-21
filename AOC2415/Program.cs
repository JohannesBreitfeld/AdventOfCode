var path = Path.Combine("..", "..", "..", "..", "input15Map.txt");
var inputMap = File.ReadAllLines(path);

path = Path.Combine("..", "..", "..", "..", "input15Dir.txt");
var inputDirections = File.ReadAllText(path);

char[,] map;

(int, int) startPosition;

GetMapAndStartPartTwo(inputMap, out map, out startPosition);

var mapAfterMovement = MovementPartTwo(map, startPosition, inputDirections);
Console.WriteLine(CalculateGPSPartTwo(mapAfterMovement));

//for (int i = 0; i < map.GetLength(0); i++)
//{
//    for (int j = 0; j < map.GetLength(1); j++)
//    {
//        Console.Write(map[i, j]);
//    }
//    Console.WriteLine();
//}

static char[,] Movement(char[,] map, (int x, int y) startPosition, string directions)
{
    var position = startPosition;

    for (int i = 0; i < directions.Length; i++)
    {
        var tiles = 0;
        if (directions[i] == '<')
        {
            while (true)
            {
                tiles++;

                if (map[position.y, position.x - tiles] == '#')
                {
                    break;
                }
                else if (map[position.y, position.x - tiles] == '.')
                {
                    if (tiles > 1)
                    {
                        map[position.y, position.x - tiles] = 'O';
                    }
                    position.x -= 1;
                    break;
                }
            }
        }
        else if (directions[i] == '>')
        {
            while (true)
            {
                tiles++;

                if (map[position.y, position.x + tiles] == '#')
                {
                    break;
                }
                else if (map[position.y, position.x + tiles] == '.')
                {
                    if (tiles > 1)
                    {
                        map[position.y, position.x + tiles] = 'O';
                    }
                    position.x += 1;
                    break;
                }
            }
        }
        else if (directions[i] == '^')
        {
            while (true)
            {
                tiles++;

                if (map[position.y - tiles, position.x] == '#')
                {
                    break;
                }
                else if (map[position.y - tiles, position.x] == '.')
                {
                    if (tiles > 1)
                    {
                        map[position.y - tiles, position.x] = 'O';
                    }
                    position.y -= 1;
                    break;
                }
            }
        }
        else if (directions[i] == 'v')
        {
            while (true)
            {
                tiles++;

                if (map[position.y + tiles, position.x] == '#')
                {
                    break;
                }
                else if (map[position.y + tiles, position.x] == '.')
                {
                    if (tiles > 1)
                    {
                        map[position.y + tiles, position.x] = 'O';
                    }
                    position.y += 1;
                    break;
                }
            }
        }
        map[position.y, position.x] = '.';
    }
    return map;
}

static long CalculateGPS(char[,] map)
{
    long totalGPS = 0;
    for (int i = 0; i < map.GetLength(0); i++)
    {
        for (int j = 0; j < map.GetLength(1); j++)
        {
            if (map[i, j] == 'O')
            {
                totalGPS += i * 100;
                totalGPS += j;
            }
        }
    }
    return totalGPS;
}

static void GetMapAndStart(string[] inputMap, out char[,] map, out (int, int) startPosition)
{
    int rows = inputMap.Length;
    int cols = inputMap[0].Length;

    map = new char[rows, cols];
    startPosition = (0, 0);
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            map[i, j] = inputMap[i][j];
            if (map[i, j] == '@')
            {
                map[i, j] = '.';
                startPosition = (j, i);
            }
        }
    }
}

static void GetMapAndStartPartTwo(string[] inputMap, out char[,] map, out (int, int) startPosition)
{
    int rows = inputMap.Length;
    int cols = inputMap[0].Length * 2;

    map = new char[rows, cols];
    startPosition = (0, 0);
    for (int i = 0; i < rows; i++)
    {
        var columnCounter = 0;
        for (int j = 0; j < cols; j++)
        {
            if (j % 2 == 0 && !(j >= cols -1) && j != 0) columnCounter++;

            if (inputMap[i][columnCounter] == 'O' && j % 2 == 0)
            {
                map[i, j] = '[';
            }
            else if (inputMap[i][columnCounter] == 'O' && j % 2 == 1)
            {
                map[i, j] = ']';
            }
            else if (inputMap[i][columnCounter] == '@')
            {
                map[i, j] = '.';
                if (j %2 == 0)
                {
                    startPosition = (j, i);
                }
            }
            else
            {
                map[i, j] = inputMap[i][columnCounter];
            }
        }
    }
}

static char[,] MovementPartTwo(char[,] map, (int x, int y) startPosition, string directions)
{
    var position = startPosition;

    for (int i = 0; i < directions.Length; i++)
    {
        
        if (directions[i] == '<')
        {
            var tiles = 0;
            while (true)
            {
                tiles++;

                if (map[position.y, position.x - tiles] == '#')
                {
                    break;
                }
                else if (map[position.y, position.x - tiles] == '.')
                {
                    if (tiles > 1)
                    {
                        for (int k = 2; k <= tiles; k++)
                        {
                            if (k % 2 == 0)
                            {
                                map[position.y, position.x - k] = ']';
                            }
                            else
                            {
                                map[position.y, position.x - k] = '[';
                            }
                        }
                    }
                    position.x--;
                    break;
                }
            }
        }
        else if (directions[i] == '>')
        {
            var tiles = 0;
            while (true)
            {
                tiles++;

                if (map[position.y, position.x + tiles] == '#')
                {
                    break;
                }
                else if (map[position.y, position.x + tiles] == '.')
                {
                    if (tiles > 1)
                    {
                        for (int k = 2; k <= tiles; k++)
                        {
                            if (k % 2 == 0)
                            {
                                map[position.y, position.x + k] = '[';
                            }
                            else
                            {
                                map[position.y, position.x + k] = ']';
                            }
                        }
                    }
                    position.x++;
                    break;
                }
            }
        }
        else if (directions[i] == '^')
        {
            var StartPosition = position;
            var SearchQueue = new Queue<(int X, int Y)>();
            SearchQueue.Enqueue(StartPosition);
            var visitedPositions = new List<((int X, int Y), char c)>();
            bool hasHitWall = false;
            var control = new HashSet<(int, int)>();
                
            while (SearchQueue.Count > 0)
            {
                var currentPosition = SearchQueue.Dequeue();
                control.Add(currentPosition);

                visitedPositions.Add((currentPosition, map[currentPosition.Y,currentPosition.X]));

                if (map[currentPosition.Y - 1, currentPosition.X] == '#')
                {
                    hasHitWall = true;
                    break;
                }
                else if (map[currentPosition.Y - 1, currentPosition.X] == '[' ||
                    map[currentPosition.Y - 1, currentPosition.X] == ']')
                {
                    SearchQueue.Enqueue((currentPosition.X, currentPosition.Y - 1));

                }
                    
                if (map[currentPosition.Y, currentPosition.X] == '[')
                {
                    if (!control.Contains((currentPosition.X + 1, currentPosition.Y)))
                    {
                         SearchQueue.Enqueue((currentPosition.X + 1, currentPosition.Y));
                    }
                }
                else if (map[currentPosition.Y, currentPosition.X] == ']')
                {
                    if (!control.Contains((currentPosition.X - 1, currentPosition.Y)))
                    {
                        SearchQueue.Enqueue((currentPosition.X - 1, currentPosition.Y));
                    }
                }
            }
            if (!hasHitWall)
            {
                foreach (var pos in visitedPositions)
                {
                    map[pos.Item1.Y, pos.Item1.X] = '.';
                }
                foreach (var pos in visitedPositions)
                {
                    map[pos.Item1.Y - 1, pos.Item1.X] = pos.c;
                }
                position.y--;
            }
        }
        else if (directions[i] == 'v')
        {
            var StartPosition = position;
            var SearchQueue = new Queue<(int X, int Y)>();
            SearchQueue.Enqueue(StartPosition);
            var visitedPositions = new List<((int X, int Y), char c)>();
            bool hasHitWall = false;
            var control = new HashSet<(int, int)>();

            while (SearchQueue.Count > 0)
            {
                var currentPosition = SearchQueue.Dequeue();
                control.Add(currentPosition);
                visitedPositions.Add((currentPosition, map[currentPosition.Y, currentPosition.X]));

                if (map[currentPosition.Y + 1, currentPosition.X] == '#')
                {
                    hasHitWall = true;
                    break;
                }
                else if (map[currentPosition.Y + 1, currentPosition.X] == '[' ||
                    map[currentPosition.Y + 1, currentPosition.X] == ']')
                {
                    SearchQueue.Enqueue((currentPosition.X, currentPosition.Y + 1));
                }

                if (map[currentPosition.Y, currentPosition.X] == '[')
                {
                    if (!control.Contains((currentPosition.X + 1, currentPosition.Y)))
                    {
                        SearchQueue.Enqueue((currentPosition.X + 1, currentPosition.Y));
                    }
                }
                else if (map[currentPosition.Y, currentPosition.X] == ']')
                {
                    if (!control.Contains((currentPosition.X - 1, currentPosition.Y)))
                    {
                        SearchQueue.Enqueue((currentPosition.X - 1, currentPosition.Y));
                    }
                }
            }
            if (!hasHitWall)
            {
                foreach (var pos in visitedPositions)
                {
                    map[pos.Item1.Y, pos.Item1.X] = '.';
                }
                foreach (var pos in visitedPositions)
                {
                    map[pos.Item1.Y + 1, pos.Item1.X] = pos.c;
                }
                position.y++;
            }
        }
        map[position.y, position.x] = '.';
    }
    return map;
}

static long CalculateGPSPartTwo(char[,] map)
{
    long totalGPS = 0;
    for (int i = 0; i < map.GetLength(0); i++)
    {
        for (int j = 0; j < map.GetLength(1); j++)
        {
            if (map[i, j] == '[')
            {
                totalGPS += i * 100;
                totalGPS += j;
            }
        }
    }
    return totalGPS;
}