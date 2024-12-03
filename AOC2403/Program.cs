using System.Text.RegularExpressions;

var path = Path.Combine("..", "..", "..", "..","inputs", "input03.txt");

string input = File.ReadAllText(path);
var pattern1 = @"mul\((\d{1,3}),(\d{1,3})\)";
var pattern2 = @"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)";

MatchCollection matches = Regex.Matches(input, pattern2);

int total = Problem2(matches);

Console.WriteLine(total);

static int Problem1(MatchCollection matches)
{
    return matches.Sum(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
}

static int Problem2(MatchCollection matches)
{
    bool mulEnabled = true;
    return matches.Aggregate(0, (total, match) =>
    {
        if (match.Value == "do()") mulEnabled = true;
        else if (match.Value == "don't()") mulEnabled = false;
        else if (mulEnabled) return total += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        return total;
    });
}
