
var PartOne = new PartOne();

PartOne.ReadMap();
Console.WriteLine(PartOne.CalculateScore());


class PartOne
{
    private int rows;
    private int cols;
    private char[,]? map;
    private int startX;
    private int startY;
    private int endX;
    private int endY;
    private int[] dx = { 0, 1, 0, -1 }; 
    private int[] dy = { -1, 0, 1, 0 }; 
    //private char[] directions = { 'N', 'E', 'S', 'W' };

    public void ReadMap()
    {
        var path = Path.Combine("..", "..", "..", "..", "input16.txt");
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

    // Läs på mer om Djikstras algortihm och se över denna funktionen igen.
    // Blir nog fel på första rörelsen eftersom det alltid diffar +/-1000.
    
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
            }

            int nx = x + dx[dir];
            int ny = y + dy[dir];

            if (nx >= 0 && ny >= 0 && nx < rows && ny < cols && map![nx, ny] != '#')
            {
                if (currentScore + 1 < score[(nx, ny, dir)])
                {
                    score[(nx, ny, dir)] = currentScore + 1;
                    pq.Enqueue((nx, ny, dir), score[(nx, ny, dir)]);
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
}









