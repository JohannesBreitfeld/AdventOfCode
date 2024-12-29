using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2418;

internal class PartTwo
{
    public char[,] map = null!;
    private int rows = 71;
    private int cols = 71;
    private int fallenBytes = 1024;
    private int[] dx = { 0, 1, 0, -1 }; // N - E - S - W 
    private int[] dy = { -1, 0, 1, 0 };
    private List<(int x, int y)> incommingBytes = null!;

    public (int x, int y) Solve()
    {
        ReadMap();

        while (true)
        {
            var next = incommingBytes[fallenBytes - 1];
            AddObstacle(next);
            var coordinate = FindBestPath();
            if(coordinate.x == -1)
            {
                return next;
            }
            fallenBytes++;
        }
    }

    private void AddObstacle((int x, int y) next)
    {
        map[next.y, next.x] = '#';
    }

    public void ReadMap()
    {
        var path = Path.Combine("..", "..", "..", "..", "input18.txt");
        var input = File.ReadAllLines(path);

        incommingBytes = new();
        map = new char[rows, cols];

        foreach (var line in input)
        {
            var coordinates = line
                .Split(',')
                .Select(c => int.Parse(c))
                .ToArray();

            incommingBytes.Add((coordinates[0], coordinates[1]));
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                map[i, j] = '.';
            }
        }

        for (int i = 0; i < fallenBytes; i++)
        {
            map[incommingBytes[i].y, incommingBytes[i].x] = '#';
        }
    }

    public (int x, int y) FindBestPath()
    {
        (int x, int y) start = (0, 0);
        (int x, int y) end = (cols - 1, rows - 1);

        Queue<(int x, int y)> queue = new();

        var visited = new HashSet<(int x, int y)>();
        queue.Enqueue((start.x, start.y));

        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();
            visited.Add((x, y));

            if (x == end.x && y == end.y)
            {
                return (x, y);
            }

            for (int i = 0; i < 4; i++)
            {
                int newX = x + dx[i % 4];
                int newY = y + dy[i % 4];

                if (newX < 0 ||
                    newX >= cols ||
                    newY < 0 ||
                    newY >= rows ||
                    map[newY, newX] == '#' ||
                    visited.Contains((newX, newY)))
                {
                    continue;
                }
                visited.Add((newX, newY));
                queue.Enqueue((newX, newY));
            }
        }
        return (-1,-1);
    }
}
