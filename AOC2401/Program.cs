using System.Linq;

var path = Path.Combine("..", "..", "..", "..","inputs", "Input01.txt");
String[] input = File.ReadAllLines(path);

var left = input.Select(line => int.Parse(line.Substring(0, 5))).ToList();
var right = input.Select(line => int.Parse(line.Substring(8, 5))).ToList();

//long total = Problem1(left, right);
long total = Problem2(left, right);

Console.WriteLine(total);

static long Problem1(List<int> left, List<int> right)
{
    var leftOrdered = left.OrderBy(value => value).ToList();
    var rightOrdered = right.OrderBy(value => value).ToList();

    long total = leftOrdered.Zip(rightOrdered, (l, r) => Math.Abs(l - r)).Sum();
    return total;
}

static long Problem2(List<int> left, List<int> right)
{
    long total = left.Sum(l => l * right.Count(r => r == l));

    return total;
}