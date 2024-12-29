namespace AOC2419;

internal class PartOne
{
    private string[] colors = null!;
    private string[] patterns = null!;

    public int Solve()
    {
        ReadInput();

        int patternCount = 0;

        foreach (var pattern in patterns)
        {
            if(IsPossible("", pattern, new HashSet<string>()))
            {
                patternCount++;
            }
        }

        return patternCount;
    }

    private bool IsPossible(string patternBuild, string pattern, HashSet<string> visited)
    {
        if (patternBuild == pattern)
        {
            return true;
        }
        if (visited.Contains(patternBuild))
        {
            return false;
        }
        visited.Add(patternBuild);

        for (int i = 0; i < colors.Length; i++)
        {
            var testBuild = patternBuild + colors[i];
            
            if (pattern.StartsWith(testBuild) && IsPossible(testBuild, pattern, visited))
            {
                return true;
            }
        }
        return false;
    }

    private void ReadInput()
    {
        var path = Path.Combine("..", "..", "..", "..", "Input19Patterns.txt");
        patterns = File.ReadAllLines(path);

        path = Path.Combine("..", "..", "..", "..", "Input19Colors.txt");
        var colorString = File.ReadAllText(path);

        colors = colorString.Split(", ").ToArray();
    }
}
