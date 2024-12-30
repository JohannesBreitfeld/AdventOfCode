using System.Drawing;

namespace AOC2420;

internal class PartOne
{
    private int rows;
    private int cols;
    private char[,] charMap = null!;
    private Point startPoint;
    private Point endPoint;
    private int[] dx = { 0, 1, 0, -1 }; // N - E - S - W 
    private int[] dy = { -1, 0, 1, 0 };
    private Dictionary<(int x, int y), int > visitedCost = null!;
    public int[,] CostMap = null!;

    private void ReadInput()
    {
        var path = Path.Combine("..", "..", "..", "..", "Input20.txt");
        var input = File.ReadAllLines(path);

        rows = input.Length;
        cols = input[0].Length;

        charMap = new char[rows,cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                charMap[i, j] = input[i][j];

                if (charMap[i,j] == 'S')
                {
                    startPoint.X = j;
                    startPoint.Y = i;
                }
                else if (charMap[i, j] == 'E')
                {
                    endPoint.X = j;
                    endPoint.Y = i;
                }
            }
        }
    }

    private void FindPath()
    {
        ReadInput();

        (int x, int y) start = (startPoint.X, startPoint.Y);
        (int x, int y) end = (endPoint.X, endPoint.Y);

        Queue<(int x, int y, int steps)> queue = new();

        visitedCost = new Dictionary<(int x, int y), int>();
        queue.Enqueue((start.x, start.y, 0));

        while (queue.Count > 0)
        {
            var (x, y, steps) = queue.Dequeue();
            visitedCost[(x, y)] = steps;

            if (x == end.x && y == end.y)
            {
                break;
            }

            for (int i = 0; i < 4; i++)
            {
                int newX = x + dx[i % 4];
                int newY = y + dy[i % 4];

                if (newX < 0 ||
                    newX >= cols ||
                    newY < 0 ||
                    newY >= rows ||
                    charMap[newY, newX] == '#' ||
                    visitedCost.ContainsKey((newX, newY)))
                {
                    continue;
                }
                queue.Enqueue((newX, newY, steps + 1));
            }
        }
    }

    public void MapCosts()
    {
        FindPath();

        CostMap = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (charMap[i,j] == '#')
                {
                    CostMap[i, j] = -1;
                }
                else if(visitedCost.ContainsKey((j, i)))
                {
                    CostMap[i, j] = visitedCost[(j, i)];
                }
            }
        }
    }
    public int FindShortcuts()
    {
        MapCosts();

        var shortcuts = new HashSet<(Point start, Point end)>();

        foreach (var step in visitedCost)
        {
            var startX = step.Key.x;
            var startY = step.Key.y;
            var cost = step.Value;

            for (int i = 0; i < 4; i++)
            {
                var newX = startX + dx[i];
                var newY = startY + dy[i];

                if (newY > 0 && newY < rows && newX > 0 && newX < cols && CostMap[newY, newX] != -1)
                {
                    if(CostMap[newY, newX] - cost > 100)
                    {
                        shortcuts.Add((new Point(startX, startY), new Point(newX, newY)));
                    }
                }

                for (int j = 0; j < 4; j++)
                {
                    var nextX = newX + dx[i];
                    var nextY = newY + dy[i];

                    if (nextY > 0 && nextY < rows && nextX > 0 && nextX < cols && CostMap[nextY, nextX] != -1)
                    {
                        if (CostMap[nextY, nextX] - cost > 100)
                        {
                            shortcuts.Add((new Point(startX, startY), new Point(nextX, nextY)));
                        }
                    }
                }
            }

        }

        return shortcuts.Count;
    }
}
