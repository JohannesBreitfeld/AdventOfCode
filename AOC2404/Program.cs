
var path = Path.Combine("..", "..", "..", "..", "input04.txt");
var input = File.ReadAllLines(path);

int xmasCount = Problem1(input);
int masCount = Problem2(input);

Console.WriteLine("Problem 1: " + xmasCount);
Console.WriteLine("Probelem 2: " + masCount);

static int Problem1(string[] input)
{
    string word = "XMAS";
    int count = 0;
    for (int row = 0; row < input.Length; row++)
    {
        for (int col = 0; col < input[row].Length; col++)
        {
            // Horisontellt framåt
            if (CheckDirection(input, row, col, 0, 1, word))
                count++;
            // Horisontellt bakåt
            if (CheckDirection(input, row, col, 0, -1, word))
                count++;
            // Vertikalt nedåt
            if (CheckDirection(input, row, col, 1, 0, word))
                count++;
            // Vertikalt uppåt
            if (CheckDirection(input, row, col, -1, 0, word))
                count++;
            // Diagonalt nedåt höger
            if (CheckDirection(input, row, col, 1, 1, word))
                count++;
            // Diagonalt uppåt höger
            if (CheckDirection(input, row, col, -1, 1, word))
                count++;
            // Diagonalt nedåt vänster
            if (CheckDirection(input, row, col, 1, -1, word))
                count++;
            // Diagonalt uppåt vänster
            if (CheckDirection(input, row, col, -1, -1, word))
                count++;
        }
    }
    return count;
}

static int Problem2(string[] input)
{
    int count = 0;
    for (int row = 1; row < input.Length - 1; row++)
    {
        for (int col = 1; col < input[row].Length - 1; col++)
        {
            if (input[row][col] == 'A')
            {
                if (isXmas(input, row, col))
                {
                    count++;
                }
            }
        }
    }
    return count;
}

static bool CheckDirection(string[] input, int row, int col, int rowDirection, int colDirection, string word)
{
    for (int i = 0; i < word.Length; i++)
    {
        int newRow = row + i * rowDirection;
        int newCol = col + i * colDirection;

        if (newRow < 0 || newRow >= input.Length || newCol < 0 || newCol >= input.Length)
            return false;

        if (input[newRow][newCol] != word[i])
            return false;
    }
    return true;
}

static bool isXmas(string[] input, int row, int col)
{
    char[] mas = new char[] { 'M', 'A', 'S' };
    char[] sam = new char[] { 'S', 'A', 'M' };

    char[] diagonalUpDown = new char[] { input[row + 1][col - 1], input[row][col], input[row - 1][col + 1] };
    char[] diagonalDownUp = new char[] { input[row - 1][col - 1], input[row][col], input[row + 1][col + 1] };

    if ((diagonalUpDown.SequenceEqual(mas) || diagonalUpDown.SequenceEqual(sam)) &&
        (diagonalDownUp.SequenceEqual(mas) || diagonalDownUp.SequenceEqual(sam)))
    {
        return true;
    }

    return false;
}