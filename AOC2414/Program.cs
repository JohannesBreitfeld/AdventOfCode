using System.Text.RegularExpressions;

var path = Path.Combine("..", "..", "..", "..", "input14.txt");
var input = File.ReadAllLines(path);

string regexPattern = @"p=(-?\d+),(-?\d+)\s*v=(-?\d+),(-?\d+)";

var robotStartList = new List<((int X, int Y) p, (int X, int Y) v)>();
var robotEndList = new List<(int X, int Y)>();

Regex regex = new Regex(regexPattern);
foreach (var line in input)
{
    foreach (Match match in regex.Matches(line))
    {
        int pX = int.Parse(match.Groups[1].Value);
        int pY = int.Parse(match.Groups[2].Value);
        int vX = int.Parse(match.Groups[3].Value);
        int vY = int.Parse(match.Groups[4].Value);

        robotStartList.Add(((pX, pY), (vX, vY)));
    }
}

foreach (var robot in robotStartList)
{
    var startX = robot.p.X;
    var startY = robot.p.Y;
    var speedX = robot.v.X;
    var speedY = robot.v.Y;

    var robotEndPosition = CalcaulateMovement(startX, startY, speedX, speedY);

    robotEndList.Add(robotEndPosition);
}

var firstQuadrant = 0;
var secondQuadrant = 0;
var thirdQuadrant = 0;
var fourthQuadrant = 0;

var rows = 103;
var cols = 101;

foreach (var robot in robotEndList)
{
    if (robot.X < cols / 2 && robot.Y < rows / 2) firstQuadrant++;
    if (robot.X > cols / 2 && robot.Y < rows / 2) secondQuadrant++;
    if (robot.X < cols / 2 && robot.Y > rows / 2) thirdQuadrant++;
    if (robot.X > cols / 2 && robot.Y > rows / 2) fourthQuadrant++;
}

long safetyFactor = firstQuadrant * secondQuadrant * thirdQuadrant * fourthQuadrant;
Console.WriteLine(safetyFactor);

static (int x, int y) CalcaulateMovement(int startX, int startY, int speedX, int speedY)
{
    var rows = 103;
    var cols = 101;

    var X = startX;
    var Y = startY;

    for (int i = 0; i < 100; i++)
    {
        X += speedX;
        Y += speedY;
        
        if (X > cols -1)
        {
            X -= cols;
        }
        else if(X < 0)
        {
            X += cols;
        }
        if(Y > rows -1)
        {
            Y -= rows;
        }
        else if(Y < 0)
        {
            Y += rows;
        }
    }

    return (X, Y);
}