using System.Collections.Generic;
using System.Text.RegularExpressions;

var path = Path.Combine("..", "..", "..", "..", "input14.txt");
var input = File.ReadAllLines(path);

string regexPattern = @"p=(-?\d+),(-?\d+)\s*v=(-?\d+),(-?\d+)";

var robotStartList = new List<((int X, int Y) p, (int X, int Y) v)>();

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

//long safetyFactor = PartOne(robotStartList);
//Console.WriteLine(safetyFactor);

var sec = PartTwo(robotStartList);
Console.WriteLine(sec);

static int PartTwo(List<((int X, int Y) p, (int X, int Y) v)> robotsStartList)
{
    long lowestSafetyFactor = 218965032;
    int sec = 0;
    var newPositions = robotsStartList;

    for (int i = 0; i < 10000; i++)
    {
        newPositions = CalcaulateOneSecond(newPositions);

        var firstQuadrant = 0;
        var secondQuadrant = 0;
        var thirdQuadrant = 0;
        var fourthQuadrant = 0;

        var rows = 103;
        var cols = 101;

        foreach (var robot in newPositions)
        {
            if (robot.p.X < cols / 2 && robot.p.Y < rows / 2) firstQuadrant++;
            if (robot.p.X > cols / 2 && robot.p.Y < rows / 2) secondQuadrant++;
            if (robot.p.X < cols / 2 && robot.p.Y > rows / 2) thirdQuadrant++;
            if (robot.p.X > cols / 2 && robot.p.Y > rows / 2) fourthQuadrant++;
        }

        long safetyFactor = firstQuadrant * secondQuadrant * thirdQuadrant * fourthQuadrant;

        if(safetyFactor < lowestSafetyFactor)
        {
            lowestSafetyFactor = safetyFactor;
            sec = i + 1;
            PrintMap(newPositions, sec);
        }
    }
    return sec;
}

static void PrintMap(List<((int X, int Y) p, (int X, int Y) v)> robotsStartList, int second)
{
    var map = GetEmptyMap();

    Console.WriteLine($"******************** {second} *****************************************");
    foreach (var robot in robotsStartList)
    {
        map[robot.p.Y, robot.p.X] = '#';
    }
    for (int i = 0; i < 103; i++)
    {
        for (int j = 0; j < 101; j++)
        {
            if (map[i, j] == '#')
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ResetColor();
            }
            Console.Write(map[i, j]);
        }
        Console.WriteLine();
    }

}

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

        if (X > cols - 1)
        {
            X -= cols;
        }
        else if (X < 0)
        {
            X += cols;
        }
        if (Y > rows - 1)
        {
            Y -= rows;
        }
        else if (Y < 0)
        {
            Y += rows;
        }
    }

    return (X, Y);
}

static long PartOne(List<((int X, int Y) p, (int X, int Y) v)> robotStartList)
{
    var robotEndList = new List<(int X, int Y)>();
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
    return safetyFactor;
}

static char[,] GetEmptyMap()
{
    char[,] map = new char[103, 101];

    for (int i = 0; i < 103; i++)
    {
        for (int j = 0; j < 101; j++)
        {
            map[i, j] = '-';
        }
    }
    return map;
}

static List<((int, int), (int, int))> CalcaulateOneSecond(List<((int X, int Y) p, (int X, int Y) v)> robots)
{
    var rows = 103;
    var cols = 101;

    var newPositions = new List<((int X, int Y) p, (int X, int Y) v)>();

    for (int i = 0; i < robots.Count; i++)
    {
        var X = robots[i].p.X;
        var Y = robots[i].p.Y;
        var speedX = robots[i].v.X;
        var speedY = robots[i].v.Y;

        X += speedX;
        Y += speedY;

        if (X > cols - 1)
        {
            X -= cols;
        }
        else if (X < 0)
        {
            X += cols;
        }
        if (Y > rows - 1)
        {
            Y -= rows;
        }
        else if (Y < 0)
        {
            Y += rows;
        }

        newPositions.Add(((X, Y), (speedX, speedY)));

    }

    return newPositions;
}