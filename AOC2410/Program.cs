var path = Path.Combine("..", "..", "..", "..", "input10.txt");
var input = File.ReadAllLines(path);

int rows = input.Length;
int cols = input[0].Length;

int[,] map = new int[rows, cols];
for (int i = 0; i < input.Length; i++)
{
    char[] row = input[i].ToArray();
    for (int j = 0; j < row.Length; j++)
    {
        map[i, j] = (int)row[j] -'0';
    }
}

int totalScore = 0;

for (int i = 0; i < rows; i++)
{
    for (int j = 0; j < cols; j++)
    {
        if (map[i, j] == 0)
        {
           totalScore += BFS(map, i, j, rows, cols);
        }
    }
}

Console.WriteLine("Problem 1: " + totalScore);
Ratings(map, rows, cols);


static bool IsInBounds(int x, int y, int rows, int cols)
    {
        return x >= 0 && x < rows && y >= 0 && y < cols;
    }

static int BFS(int[,] map, int startX, int startY, int rows, int cols)
{
    int[] dx = { -1, 1, 0, 0 };
    int[] dy = { 0, 0, -1, 1 };
    Queue<(int, int)> queue = new Queue<(int, int)>(); 
    bool[,] visited = new bool[rows, cols]; 
    HashSet<(int, int)> reachableNines = new HashSet<(int, int)>(); 

    queue.Enqueue((startX, startY)); 
    visited[startX, startY] = true;

    while (queue.Count > 0)
    {
        var (x, y) = queue.Dequeue(); 

        if (map[x, y] == 9)
        {
            reachableNines.Add((x, y));
        }

        for (int i = 0; i < 4; i++)
        {
            int newX = x + dx[i];
            int newY = y + dy[i];

            if (IsInBounds(newX, newY, rows, cols) 
                && !visited[newX, newY] 
                && map[newX, newY] == map[x, y] + 1)
            {
                visited[newX, newY] = true;
                queue.Enqueue((newX, newY));
            }
        }
    }

    return reachableNines.Count;
}

static void Ratings(int[,] map, int rows, int cols)
{
    int[] dx = { -1, 1, 0, 0 };
    int[] dy = { 0, 0, -1, 1 };
    int[,] paths = new int[rows, cols];

    Queue<(int, int)> queue = new Queue<(int, int)>();

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            if (map[i, j] == 0) 
            {
                queue.Enqueue((i, j));
                paths[i, j] = 1; 
            }
        }
    }

    while (queue.Count > 0)
    {
        var (x, y) = queue.Dequeue();

        for (int i = 0; i < 4; i++)
        {
            int newX = x + dx[i];
            int newY = y + dy[i];

            if (IsInBounds(newX, newY, rows, cols) 
                && map[newX, newY] == map[x, y] + 1)
            {
                paths[newX, newY] += paths[x, y];

                if (paths[newX, newY] == paths[x, y])
                {
                    queue.Enqueue((newX, newY));
                }
            }
        }
    }

    int totalScore = 0;
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            if (map[i, j] == 9)
            {
                totalScore += paths[i, j]; 
            }
        }
    }
    Console.WriteLine("Problem2: " + totalScore);
}






