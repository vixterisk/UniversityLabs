using System;
using System.IO;
using System.Text;

namespace backpack
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            int n = int.Parse(input[0].Split()[0]);
            int m = int.Parse(input[0].Split()[1]);
            var mi = new int[n];
            var ci = new int[n];
            for (int i = 0; i < n; i++)
            {
                mi[i] = int.Parse(input[1].Split()[i]);
                ci[i] = int.Parse(input[2].Split()[i]);
            }
            var s = new int[n];
            var b = new int[n + 1];
            for (int i = 0; i < n + 1; i++)
                b[i] = i + 1;
            int x = 0;
            int curSum = 0;
            int curMas = 0;
            int maxSum = 0;
            var answer = new int[n];
            while (x <= n)
            {
                x = b[0];
                if (x > n) break;
                b[0] = 1;
                b[x - 1] = b[x];
                b[x] = x + 1;
                s[x - 1] ^= 1;
                if (s[x - 1] == 1)
                {
                    curSum += ci[x - 1];
                    curMas += mi[x - 1];
                }
                if (s[x - 1] == 0)
                {
                    curSum -= ci[x - 1];
                    curMas -= mi[x - 1];
                }
                if (curSum > maxSum && curMas <= m)
                {
                    maxSum = curSum;
                    s.CopyTo(answer, 0);
                }
            }
            var result = new StringBuilder();
            for (int i = 0; i < n; i++)
                if (answer[i] == 1)
                    result.Append((i + 1) + " ");
            File.WriteAllText("output.txt", result.ToString());
        }
    }
}
