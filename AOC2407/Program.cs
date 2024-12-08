var path = Path.Combine("..", "..", "..", "..", "input07.txt");
var input = File.ReadAllLines(path);

long totalCalibrationResult = 0;

foreach (var line in input)
{
    var parts = line.Split(": ");
    long targetValue = long.Parse(parts[0]);
    var numbers = Array.ConvertAll(parts[1].Split(' '), int.Parse);

    if (Problem2(targetValue, numbers))
    {
        totalCalibrationResult += targetValue;
    }
}

Console.WriteLine(totalCalibrationResult);

static bool Problem1(long targetValue, int[] numbers)
{
    int n = numbers.Length;
    if (n == 1)
    {
        return numbers[0] == targetValue;
    }

    int totalCombinations = (int)Math.Pow(2,(n-1));

    for (int i = 0; i < totalCombinations; i++)
    {
        var operators = new List<char>();

        for (int j = 0; j < n - 1; j++)
        {
            if ((i / (int)Math.Pow(2, j)) % 2 == 1)
            {
                operators.Add('*');
            }
            else
            {
                operators.Add('+');
            }
        }

        if (EvaluateExpression(numbers, operators) == targetValue)
        {
            return true; 
        }
    }
    return false; 
}

static long EvaluateExpression(int[] numbers, List<char> operators)
{
    long result = numbers[0];
    for (int i = 1; i < numbers.Length; i++)
    {
        if (operators[i - 1] == '+')
        {
            result += numbers[i];
        }
        else if (operators[i - 1] == '*')
        {
            result *= numbers[i];
        }
    }
    return result;
}

static bool Problem2(long targetValue, int[] numbers)
{
    int n = numbers.Length;
    if (n == 1)
    {
        return numbers[0] == targetValue;
    }

    int totalCombinations = (int)Math.Pow(3, (n - 1));

    for (int i = 0; i < totalCombinations; i++)
    {
        var operators = new List<char>();

        for (int j = 0; j < n - 1; j++)
        {
            int operatorChoice = (i / (int)Math.Pow(3, j)) % 3;

            if (operatorChoice == 0) 
            {
                operators.Add('+');
            }
            else if (operatorChoice == 1) 
            {
                operators.Add('*');
            }
            else 
            {
                operators.Add('|');
            }
        }
    
        if (EvaluateExpressionThreeOperands(numbers, operators) == targetValue)
        {
            return true;
        }
    }
    return false;
}

static long EvaluateExpressionThreeOperands(int[] numbers, List<char> operators)
{
    long result = numbers[0];
    int operatorIndex = 0;

    for (int i = 1; i < numbers.Length; i++)
    {
        if (operators[operatorIndex] == '+')
        {
            result += numbers[i];
        }
        else if (operators[operatorIndex] == '*')
        {
            result *= numbers[i];
        }
        else if (operators[operatorIndex] == '|')
        {
            result = long.Parse(result.ToString() + numbers[i].ToString());
        }

        operatorIndex++;
    }
    return result;
}