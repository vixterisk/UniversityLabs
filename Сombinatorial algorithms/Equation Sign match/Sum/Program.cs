using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sum
{
    class Program
    {
        static int sum(int[] mask, List<int> numbers)
        {
            int res = 0;
            for (int i = 0; i < numbers.Count; i++)
                if (mask[i] == 1)
                    res += numbers[i];
                else
                    res -= numbers[i];
            return res;
        }
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var n = int.Parse(input[0].Split()[0]);
            var s = int.Parse(input[0].Split()[1]);
            var numbers = new List<int>();
            var stringNumbers = input[1].Split();
            foreach (var stringNumber in stringNumbers)
                numbers.Add(int.Parse(stringNumber));
            var sarray = new int[stringNumbers.Length];
            bool solutionFound = false;
            var summ = s - numbers[0];
            var b = new int[n + 1];
            for (int i = 0; i < n + 1; i++)
                b[i] = i + 1;
            int x = 0;
            int curSum = 0;
            while (x <= n)
            {
                x = b[0];
                if (x > n) break;
                b[0] = 1;
                b[x - 1] = b[x];
                b[x] = x + 1;
                sarray[x - 1] ^= 1;
                if (sarray[x - 1] == 1)
                {
                    curSum += numbers[x - 1];
                }
                if (sarray[x - 1] == 0)
                {
                    curSum -= numbers[x - 1];
                }
                Console.WriteLine(sum(sarray, numbers));
                if (sum(sarray, numbers) == s)
                {
                    solutionFound = true;
                    break;
                }
            }
            /*while (sarray[stringNumbers.Length - 1] == 0)
            {
                if (sum(sarray, numbers) == summ)
                {
                    solutionFound = true;
                    break;
                }
                var j = 0;
                while (sarray[j] == 1)
                {
                    sarray[j] = 0;
                    j++;
                }
                sarray[j] = 1;
                if (j == stringNumbers.Length) continue;
            }*/
            var result = new StringBuilder();
            if (!solutionFound)
                result.Append("No solution");
            else
            {
                result.Append(numbers[0]);
                for (int i = 1; i < stringNumbers.Length; i++)
                {
                    if (sarray[i] == 1)
                        result.Append("+");
                    if (sarray[i] == 0)
                        result.Append("-");
                    result.Append(numbers[i]);
                }
                result.Append("=");
                result.Append(s);
            }
            File.WriteAllText("OUTPUT.TXT", result.ToString());
            //var sarray = new int[stringNumbers.Length + 1];
            //var answer = new int[stringNumbers.Length + 1];
            //int sum = 0;
            //var minusarray = new int[numbers.Count + 1];
            //for (int i = 0; i < numbers.Count; i++)
            //    minusarray[i + 1] = minusarray[i] - numbers[numbers.Count - i - 1];
            //sum = minusarray[numbers.Count];
            //int maxj = 0;
            //while (sarray[stringNumbers.Length] == 0)
            //{
            //    Console.WriteLine(sum);
            //    if (sum == s)
            //    {
            //        sarray.CopyTo(answer, 0);
            //        break;
            //    }
            //    var j = 0;
            //    sum = 0;
            //    while (sarray[j] == 1)
            //    {
            //        sarray[j] = 0;
            //        sum -= numbers[j];
            //        j++;
            //    }
            //    if (j >= maxj)
            //        maxj = j;
            //    sarray[j] = 1;
            //    if (j == stringNumbers.Length) continue;
            //    sum += numbers[j];
            //    var rest = minusarray[numbers.Count - maxj - 1];
            //    sum += rest;
            //}
        }
    }
}


