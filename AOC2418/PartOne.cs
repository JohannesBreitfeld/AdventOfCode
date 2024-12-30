namespace AOC2418;

internal class PartOne
{
    public char[,] map;
    private int rows = 71;
    private int cols = 71;
    private int fallenBytes = 1024;
    private int[] dx = { 0, 1, 0, -1 }; // N - E - S - W 
    private int[] dy = { -1, 0, 1, 0 };

    public void ReadMap()
    {
        var path = Path.Combine("..", "..", "..", "..", "input18.txt");
        var input = File.ReadAllLines(path);

        List<(int x, int y)> incommingBytes = new();
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

    public int FindBestPath()
    {
        ReadMap();

        (int x, int y) start = (0, 0);
        (int x, int y) end = (cols -1, rows -1);

        Queue<(int x, int y, int steps)> queue = new();

        var visited = new HashSet<(int x, int y)>();
        queue.Enqueue((start.x, start.y, 0));

        while (queue.Count > 0)
        {
            var (x, y, steps) = queue.Dequeue();
            visited.Add((x, y));

            if(x == end.x && y == end.y)
            {
                return steps;
            }

            for (int i = 0; i < 4; i++)
            {
                int newX = x + dx[i % 4];
                int newY = y + dy[i % 4];

                if (newX < 0 ||
                    newX >= cols ||
                    newY < 0 ||
                    newY >= rows ||
                    map[newY, newX] == '#'||
                    visited.Contains((newX, newY)))
                {
                    continue;
                }
                visited.Add((newX, newY));
                queue.Enqueue((newX, newY, steps + 1));
            }
        }
        return -1;
    }
}
