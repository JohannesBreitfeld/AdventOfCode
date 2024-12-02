var path = Path.Combine("..", "..", "..", "..", "input02.txt");
string[] input = File.ReadAllLines(path);

var reports = input
    .Select(r => r.Split(" ")
    .Select(s => int.Parse(s))
    .ToList())
    .ToList();

int safeReportsCount = reports.Count(r => 
    IsSafeAscendingWithDampener(r) ||
    IsSafeDescendingWithDampener(r));

Console.WriteLine(safeReportsCount);

static bool IsSafeAscending(List<int> report)
{
    return report.Zip(report.Skip(1), (prev, curr) => curr - prev)
                 .All(diff => diff >= 1 && diff <= 3);
}
static bool IsSafeDescending(List<int> report)
{
    return report.Zip(report.Skip(1), (prev, curr) => prev - curr)
                 .All(diff => diff >= 1 && diff <= 3);
}

static bool IsSafeAscendingWithDampener(List<int> report)
{
    if (IsSafeAscending(report)) return true;

    return report
        .Select((value, index) => report.Where((_, i) => i != index).ToList())
        .Any(modifiedReport => IsSafeAscending(modifiedReport));
}
static bool IsSafeDescendingWithDampener(List<int> report)
{
    if(IsSafeDescending(report)) return true;

    return report
        .Select((value, index) => report.Where((_, i) => i != index).ToList())
        .Any(modifiedReport => IsSafeDescending(modifiedReport));
}