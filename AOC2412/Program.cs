
var path = Path.Combine("..", "..", "..", "..", "input12.txt");
var input = File.ReadAllLines(path);

Console.WriteLine(Problem1(input));
Console.WriteLine(Problem2(input));

static long Problem1(string[] input)
{
    int rows = input.Length;
    int cols = input[0].Length;

    char[,] map = new char[rows, cols];
    for (int i = 0; i < input.Length; i++)
    {
        char[] row = input[i].ToArray();
        for (int j = 0; j < row.Length; j++)
        {
            map[i, j] = row[j];
        }
    }

    HashSet<(int, int)> visitedPositions = new();
    int[] dx = { -1, 1, 0, 0 };
    int[] dy = { 0, 0, -1, 1 };

    long totalPrice = 0;

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            Queue<(int, int)> searchQueue = new Queue<(int, int)>();
            int area = 0;
            int perimeter = 0;

            if (!visitedPositions.Contains((i, j)))
            {
                searchQueue.Enqueue((i, j));
                visitedPositions.Add((i, j));
            }
            else
            {
                continue;
            }

            while (searchQueue.Count > 0)
            {
                var (x, y) = searchQueue.Dequeue();
                visitedPositions.Add((x, y));
                area++;

                for (int k = 0; k < 4; k++)
                {
                    int newX = x + dx[k];
                    int newY = y + dy[k];

                    if (!IsInBounds(newX, newY, rows, cols))
                    {
                        perimeter++;
                    }
                    else if (map[newX, newY] != map[x, y])
                    {
                        perimeter++;
                    }
                    else if (map[newX, newY] == map[x, y]
                        && !visitedPositions.Contains((newX, newY)))
                    {
                        searchQueue.Enqueue((newX, newY));
                        visitedPositions.Add((newX, newY));
                    }

                }
            }
            long price = area * perimeter;
            totalPrice += price;
        }
    }
    return totalPrice;
}

static long Problem2(string[] input)
{
    int rows = input.Length;
    int cols = input[0].Length;

    char[,] map = new char[rows, cols];
    for (int i = 0; i < input.Length; i++)
    {
        char[] row = input[i].ToArray();
        for (int j = 0; j < row.Length; j++)
        {
            map[i, j] = row[j];
        }
    }

    var sidesMapping = new Dictionary<(int, int), List<int>>();
    int[] dx = { -1, 1, 0, 0 };
    int[] dy = { 0, 0, -1, 1 };

    long totalPrice = 0;

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            Queue<(int, int)> searchQueue = new Queue<(int, int)>();
            int area = 0;
            int sides = 0;

            if (!sidesMapping.ContainsKey((i, j)))
            {
                searchQueue.Enqueue((i, j));
            }
            else
            {
                continue;
            }

            while (searchQueue.Count > 0)
            {
                var (x, y) = searchQueue.Dequeue();

                if (!sidesMapping.ContainsKey((x, y)))
                {
                    sidesMapping[(x, y)] = new List<int>();

                    area++;

                    for (int k = 0; k < 4; k++)
                    {
                        int newX = x + dx[k];
                        int newY = y + dy[k];
                        bool isValidLeft = x - 1 >= 0;
                        bool isValidRight = x + 1 < rows;
                        bool isValidUp = y - 1 >= 0;
                        bool isValidDown = y + 1 < cols;

                        if (!IsInBounds(newX, newY, rows, cols))
                        {
                            sidesMapping[(x, y)].Add(k);

                            if (k == 0 || k == 1)
                            {
                                if (!(sidesMapping.TryGetValue((x, y - 1), out var list) && list.Contains(k) && isValidUp && map[x, y - 1] == map[x, y])
                                    && !(sidesMapping.TryGetValue((x, y + 1), out var list1) && list1.Contains(k) && isValidDown && map[x, y + 1] == map[x, y]))
                                {
                                    sides++;
                                }
                            }
                            else if (k == 2 || k == 3)
                            {
                                if (!(sidesMapping.TryGetValue((x - 1, y), out var list) && list.Contains(k) && map[x - 1, y] == map[x, y] && isValidLeft && map[x - 1, y] == map[x, y])
                                && !(sidesMapping.TryGetValue((x + 1, y), out var list1) && list1.Contains(k) && map[x + 1, y] == map[x, y] && isValidRight && map[x + 1, y] == map[x, y]))
                                {
                                    sides++;
                                }
                            }
                        }
                        else if (map[newX, newY] != map[x, y])
                        {
                            sidesMapping[(x, y)].Add(k);

                            if (k == 0 || k == 1)
                            {
                                if (!(sidesMapping.TryGetValue((x, y - 1), out var list) && list.Contains(k) && isValidUp && map[x, y - 1] == map[x, y])
                                    && !(sidesMapping.TryGetValue((x, y + 1), out var list1) && list1.Contains(k) && isValidDown && map[x, y + 1] == map[x, y]))
                                {
                                    sides++;
                                }
                            }
                            else if (k == 2 || k == 3)
                            {
                                if (!(sidesMapping.TryGetValue((x - 1, y), out var list) && list.Contains(k) && isValidLeft && map[x - 1, y] == map[x, y])
                                && !(sidesMapping.TryGetValue((x + 1, y), out var list1) && list1.Contains(k) && isValidRight && map[x + 1, y] == map[x, y]))
                                {
                                    sides++;
                                }
                            }
                        }
                        else if (map[newX, newY] == map[x, y]
                            && !sidesMapping.ContainsKey((newX, newY)))
                        {
                            searchQueue.Enqueue((newX, newY));
                        }
                    }
                }
            }
            long price = area * sides;
            totalPrice += price;
        }
    }
    return totalPrice;
}

static bool IsInBounds(int x, int y, int rows, int cols)
{
    return x >= 0 && x < rows && y >= 0 && y < cols;
}