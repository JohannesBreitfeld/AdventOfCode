using System.Text;

namespace AOC2417
{
    internal class PartTwo
    {
        public string expectedOutput = "2413751503425530";
        public ulong bestA = ulong.MaxValue;

        public void Solve(int index, ulong A)
        {
            if (index == -1)
            {
                bestA = A;
                return;
            }

            for (ulong i = 0; i < 8; i++)
            {
                ulong nextA = A * 8 + i;
                var result = Execute(nextA);
                if (expectedOutput.EndsWith(result))
                {
                    Solve(index - 1, nextA);
                }
            }
        }

        public string Execute(ulong A)
        {
            StringBuilder output = new();
            do
            {
                // 2,4, 1,3 ,7,5 ,1,5 ,0,3 ,4,2, 5,5, 3,0
                ulong B = A % 8;
                B = B ^ 3;
                ulong C = A / (ulong)Math.Pow(2, B);
                B = B ^ 5;
                A = A / (ulong)Math.Pow(2, 3);
                B = B ^ C;
                output.Append((B % 8).ToString());
            } while (A != 0);

            return output.ToString();
        }
    }
}
