public class PartOne
{
    private int regA = 33024962;
    private int regB = 0;
    private int regC = 0;

    public int[] program = "2,4,1,3,7,5,1,5,0,3,4,2,5,5,3,0"
        .Split(',')
        .Select(n => int.Parse(n))
        .ToArray();

    private int pointer = 0;
    public List<int> output = new List<int>();

    public void Excecute()
    {
        while (pointer < program.Length)
        {
            var instruction = program[pointer];
            var operand = program[pointer + 1];

            Operation(instruction, operand);
            pointer += 2;
        }

        for (int i = 0; i < output.Count; i++)
        {
            Console.Write(output[i]);
            if (i != output.Count - 1)
            {
                Console.Write(',');
            }
        }
    }

    private int Combo(int operand)
    {
        if (operand >= 0 && operand <= 3) return operand;
        else if (operand == 4) return regA;
        else if (operand == 5) return regB;
        else if (operand == 6) return regC;
        throw new ArgumentException($"{operand}");
    }

    private void Operation(int instruction, int operand)
    {
        switch (instruction)
        {
            case 0:
                regA = regA >> Combo(operand);
                break;
            case 1:
                regB = regB ^ operand;
                break;
            case 2:
                regB = Combo(operand) % 8;
                break;
            case 3:
                if (regA != 0)
                {
                    pointer = operand - 2;
                }
                break;
            case 4:
                regB = regB ^ regC;
                break;
            case 5:
                output.Add(Combo(operand) % 8);
                break;
            case 6:
                regB = regA >> Combo(operand);
                break;
            case 7:
                regC = regA >> Combo(operand);
                break;
            default:
                throw new ArgumentException($"{instruction}");

        }
    }
}

