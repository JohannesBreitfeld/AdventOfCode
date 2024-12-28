
var PartOne = new PartOne();

PartOne.ReadMap();

Console.WriteLine(PartOne.FindBestPath());
//var map = PartOne.map;
//var bestPath = PartOne.bestPaths;


//for (int i = 0; i < map.GetLength(0); i++)
//{
//    for (int j = 0; j < map.GetLength(1); j++)
//    {
//       Console.Write(map[i, j]);
//    }
//    Console.WriteLine();
//}
//foreach (var item in bestPath)
//{
//    Console.WriteLine(item.Count);
//}
//Console.ForegroundColor = ConsoleColor.Blue;
//foreach (var path in bestPath)
//{
//    foreach (var coordinate in path)
//    {
//        Console.SetCursorPosition(coordinate.x, coordinate.y);
//        Console.Write('O');
//    }
//}
//Console.ResetColor();


class PartOne
{
    private int rows;
    private int cols;
    public char[,]? map;
    private int startX;
    private int startY;
    private int endX;
    private int endY;
    private int[] dx = { 0, 1, 0, -1 };
    private int[] dy = { -1, 0, 1, 0 };
    public HashSet<(int, int)> visited = new();
 
    public void ReadMap()
    {
        var path = Path.Combine("..", "..", "..", "..", "test16.txt");
        var inputMap = File.ReadAllLines(path);

        rows = inputMap.Length;
        cols = inputMap[0].Length;
        map = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            var line = inputMap[i];
            for (int j = 0; j < cols; j++)
            {
                map[i, j] = line[j];
                if (map[i, j] == 'S')
                {
                    startX = j;
                    startY = i;
                }
                else if (map[i, j] == 'E')
                {
                    endX = j;
                    endY = i;
                }
            }
        }
    }

    public int CalculateScore()
    {
        var pq = new PriorityQueue<(int x, int y, int dir), int>();
        var score = new Dictionary<(int x, int y, int dir), int>();
        int lowestScore = int.MaxValue;

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                for (int d = 0; d < 4; d++)
                    score[(i, j, d)] = int.MaxValue;

        score[(startX, startY, 1)] = 0;
        pq.Enqueue((startX, startY, 1), 0);

        while (pq.Count > 0)
        {
            var (x, y, dir) = pq.Dequeue();
            int currentScore = score[(x, y, dir)];

            if (x == endX && y == endY)
            {
                lowestScore = Math.Min(lowestScore, currentScore);
                break;
            }

            int newX = x + dx[dir];
            int newY = y + dy[dir];

            if (map[newY, newX] != '#')
            {
                if (currentScore + 1 < score[(newX, newY, dir)])
                {
                    score[(newX, newY, dir)] = currentScore + 1;
                    pq.Enqueue((newX, newY, dir), score[(newX, newY, dir)]);
                }
            }

            int newDirClockwise = (dir + 1) % 4;
            if (currentScore + 1000 < score[(x, y, newDirClockwise)])
            {
                score[(x, y, newDirClockwise)] = currentScore + 1000;
                pq.Enqueue((x, y, newDirClockwise), score[(x, y, newDirClockwise)]);
            }

            int newDirCounterClockwise = (dir + 3) % 4;
            if (currentScore + 1000 < score[(x, y, newDirCounterClockwise)])
            {
                score[(x, y, newDirCounterClockwise)] = currentScore + 1000;
                pq.Enqueue((x, y, newDirCounterClockwise), score[(x, y, newDirCounterClockwise)]);
            }

        }
        return lowestScore;
    }

    public int FindBestPath()
    {
        var pq = new PriorityQueue<(int x, int y, int dir), int>();
        var minimumCost = new Dictionary<(int x, int y, int dir), int>();
        var finalized = new HashSet<(int x, int y, int dir)>();
        var previous = new Dictionary<(int x, int y, int dir), HashSet<(int x, int y, int dir)>>();

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                for (int d = 0; d < 4; d++)
                    minimumCost[(i, j, d)] = int.MaxValue;

        minimumCost[(startX, startY, 1)] = 0;
        pq.Enqueue((startX, startY, 1), 0);

        while (pq.Count > 0)
        {
            var (x, y, dir) = pq.Dequeue();
            finalized.Add((x, y, dir));

            int newX = x + dx[dir];
            int newY = y + dy[dir];

            var nextState = (newX, newY, dir);
            AddNextIfImproved(nextState, minimumCost[(x, y, dir)] + 1, (x, y, dir));

            int newDirClockwise = (dir + 1) % 4;
            nextState = (x, y, newDirClockwise);
            AddNextIfImproved(nextState, minimumCost[(x, y, dir)] + 1000, (x, y, dir));

            int newDirCounterClockwise = (dir + 3) % 4;
            nextState = (x, y, newDirCounterClockwise);
            AddNextIfImproved(nextState, minimumCost[(x, y, dir)] + 1000, (x, y, dir));

        }
        var bestEndState = int.MaxValue;

        for (int i = 0; i < 4; i++)
        {
            bestEndState = Math.Min(bestEndState, minimumCost.GetValueOrDefault((endX, endY, i)));
        }
        Console.WriteLine(bestEndState);

        var bestPaths = new HashSet<(int x, int y)>();
        Queue<(int x, int y, int dir)> backTrackQueue = new();
        
        for (int i = 0; i < 4; i++)
        {
            if (minimumCost.TryGetValue((endX, endY, i), out int value) && value == bestEndState)
            {
                backTrackQueue.Enqueue((endX, endY, i));
            }
        }

        while (backTrackQueue.Count > 0)
        {
            var current = backTrackQueue.Dequeue();
            bestPaths.Add((current.x, current.y));
            
            if (previous.TryGetValue(current, out var sources))
            {
                foreach (var source in sources)
                {
                    backTrackQueue.Enqueue(source);
                }
            }
        }

        return bestPaths.Count();


        void AddNextIfImproved((int x, int y, int dir) nextState, int cost, (int x, int y, int dir) state)
        {
            if (map[nextState.y, nextState.x] == '#')
            {
                return;
            }
            //if (!finalized.Contains(nextState))
            //{
                if (!minimumCost.TryGetValue(nextState, out var nextCost) || nextCost > minimumCost[state])
                {
                    minimumCost[nextState] = cost;
                    previous[nextState] = new();
                    pq.Enqueue(nextState, minimumCost[nextState]);
                }
                if (minimumCost[nextState] == cost)
                {
                    previous[nextState].Add(state);
                }
            //}
        }

    }
}










