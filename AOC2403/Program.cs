using System.Text.RegularExpressions;
using System.Linq;

var path = Path.Combine("..", "..", "..", "..", "input03.txt");

string input = File.ReadAllText(path);
var pattern1 = @"mul\((\d{1,3}),(\d{1,3})\)";
var pattern2 = @"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)";

MatchCollection matches = Regex.Matches(input, pattern2);

int total = Problem2(matches);

Console.WriteLine(total);

static int Problem1(MatchCollection matches)
{
    int total = 0;
    foreach (Match match in matches)
    {
        int X = int.Parse(match.Groups[1].Value);
        int Y = int.Parse(match.Groups[2].Value);                     
        total += X * Y;
    }
    return total;
}

static int Problem2(MatchCollection matches)
{
    int total = 0;
    bool mulEnabled = true; 

    foreach (Match match in matches)
    {
        if (match.Value == "do()")
        {
            mulEnabled = true;
        }
        else if (match.Value == "don't()")
        {
            mulEnabled = false;
        }
        else if (mulEnabled)
        {
            int X = int.Parse(match.Groups[1].Value);
            int Y = int.Parse(match.Groups[2].Value);

            total += X * Y;
        }
    }
    return total;
}