using System.Drawing;

namespace AOC2421;

internal class PartTwo
{
    private readonly char[,] numpad = new char[,]
 {
        {'7', '8', '9' },
        {'4', '5', '6' },
        {'1', '2', '3' },
        {'#', '0', 'A' }
 };
    private readonly char[,] keypad = new char[,]
    {
        {'#', '^', 'A' },
        {'<', 'v', '>' }
    };
    private readonly string[] input = new[] { "836A", "540A", "965A", "480A", "789A" };
    private Dictionary<(char from, char to), List<string>> shortestPaths = new();
    private Dictionary<(string code, int level), ulong> shortestSubsequenceLengths = new();
    private readonly int[] dx = { 0, 1, 0, -1 }; // N - E - S - W 
    private readonly int[] dy = { -1, 0, 1, 0 };

    public void Solve()
    {
        CacheAllShortestSequences();

        ulong result = 0;

        foreach (var code in input)
        {
            var numericCode = int.Parse(string.Join("", code.Where(char.IsDigit)));
            var shortestSequence = GetShortestSequence(code, 0);
            result += shortestSequence * (ulong)numericCode;
        }

        Console.WriteLine(result);
    }

    private ulong GetShortestSequence(string code, int layer)
    {
        if (shortestSubsequenceLengths.TryGetValue((code, layer), out var cached))
        {
            return cached;
        }

        if (layer == 26)
        {
            shortestSubsequenceLengths[(code, layer)] = (ulong)code.Length;
            return (ulong)code.Length;
        }

        ulong best = 0;
        char previous = 'A';
        for (int i = 0; i < code.Length; i++)
        {
            var current = code[i];

            var cachedPaths = shortestPaths[(previous, current)];

            ulong currentBest = ulong.MaxValue;
            foreach (var path in cachedPaths)
            {
                var length = GetShortestSequence(path, layer + 1);
                if (currentBest > length)
                {
                    currentBest = length;
                }
            }

            best += currentBest;

            previous = current;
        }

        shortestSubsequenceLengths[(code, layer)] = best;
        return best;
    }

    private void CacheAllShortestSequences()
    {
        CacheAllShortestSequences(numpad);
        CacheAllShortestSequences(keypad);
    }

    private void CacheAllShortestSequences(char[,] keypad)
    {
        foreach (var character in keypad)
        {
            foreach (var otherCharacter in keypad)
            {
                shortestPaths[(character, otherCharacter)] = new List<string>();

                if (character == otherCharacter)
                {
                    shortestPaths[(character, otherCharacter)].Add("A");
                }
                else
                {
                    CacheShortestSequence(character, otherCharacter, keypad);
                }

            }
        }
    }

    private void CacheShortestSequence(char from, char to, char[,] keypad)
    {
        var fromPoint = FindCharacter(from, keypad);
        var toPoint = FindCharacter(to, keypad);

        var shortestPathLength = Math.Abs(fromPoint.X - toPoint.X) + Math.Abs(fromPoint.Y - toPoint.Y);

        Queue<(Point point, string sequence)> queue = new();
        queue.Enqueue((fromPoint, ""));
        while (queue.Count > 0)
        {
            var (point, sequence) = queue.Dequeue();

            if (point == toPoint && sequence.Length == shortestPathLength)
            {
                shortestPaths[(from, to)].Add(sequence + "A");
                continue;
            }

            if (sequence.Length >= shortestPathLength)
            {
                continue;
            }

            for (int i = 0; i < 4; i++)
            {
                var nextPoint = new Point();
                nextPoint.X = point.X + dx[i];
                nextPoint.Y = point.Y + dy[i];

                if (nextPoint.X >= 0 && nextPoint.X < keypad.GetLength(1) &&
                    nextPoint.Y >= 0 && nextPoint.Y < keypad.GetLength(0) &&
                    keypad[nextPoint.Y, nextPoint.X] != '#')
                {
                    var dirChar = i switch
                    {
                        0 => '^',
                        1 => '>',
                        2 => 'v',
                        3 => '<',
                    };
                    queue.Enqueue((nextPoint, sequence + dirChar));
                }
            }
        }

    }

    private Point FindCharacter(char c, char[,] keypad)
    {
        for (int y = 0; y < keypad.GetLength(0); y++)
        {
            for (int x = 0; x < keypad.GetLength(1); x++)
            {
                if (keypad[y, x] == c)
                {
                    return new Point(x, y);
                }
            }
        }
        throw new InvalidOperationException();
    }
}
