
var path = Path.Combine("..", "..", "..", "..", "input12.txt");
var input = File.ReadAllLines(path);

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
        //char c = '_';
        
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
            //c = map[x, y];

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
        //Console.WriteLine($"Character: {c} Area: {area}, Perimeter: {perimeter}, Price: {price}");
    }
}

Console.WriteLine(totalPrice);

static bool IsInBounds(int x, int y, int rows, int cols)
{
    return x >= 0 && x < rows && y >= 0 && y < cols;
}