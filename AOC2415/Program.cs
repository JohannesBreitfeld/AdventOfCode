var path = Path.Combine("..", "..", "..", "..", "input15Map.txt");
var inputMap = File.ReadAllLines(path);

path = Path.Combine("..", "..", "..", "..", "input15Dir.txt");
var inputDirections = File.ReadAllText(path);

int rows = inputMap.Length;
int cols = inputMap[0].Length;

char[,] map = new char[rows, cols];

var startPosition = (0, 0);

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

var mapAfterMovement = Movement(map, startPosition, inputDirections);
Console.WriteLine(CalculateGPS(mapAfterMovement));

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