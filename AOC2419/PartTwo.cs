
namespace AOC2419;

internal class PartTwo
{
    private HashSet<string> colors = null!;
    private string[] patterns = null!;

    public ulong Solve()
    {
        ReadInput();

        ulong possibleOptions = 0;

        foreach (var pattern in patterns)
        {
            possibleOptions += NumberOfPossibilities(pattern);
        }

        return possibleOptions;
    }

    private ulong NumberOfPossibilities(string pattern)
    {
        ulong[] possibilities = new ulong[pattern.Length + 1];
        possibilities[0] = 1;

        for (var length = 1; length <= pattern.Length; length++)
        {
            for (int previousLength = 0; previousLength < length; previousLength++)
            {
                if (possibilities[previousLength] == 0)
                {
                    continue;
                }

                var current = pattern.Substring(previousLength, length - previousLength);
                if (colors.Contains(current))
                {
                    possibilities[length] += possibilities[previousLength];
                }
            }
        }

        return possibilities[pattern.Length];
    }

    private void ReadInput()
    {
        var path = Path.Combine("..", "..", "..", "..", "Input19Patterns.txt");
        patterns = File.ReadAllLines(path);

        path = Path.Combine("..", "..", "..", "..", "Input19Colors.txt");
        var colorString = File.ReadAllText(path);

        colors = new HashSet<string>(colorString.Split(", "));
    }

}
