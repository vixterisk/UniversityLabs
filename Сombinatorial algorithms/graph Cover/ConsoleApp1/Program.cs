using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Graph
    {
        int[,] A;
        List<List<int>> adjList;
        int n;
        int L;

        public Graph(string filename)
        {
            ReadFromFile(filename);
        }

        public Graph(int[,] A, int L)
        {
            this.A = A;
            this.L = L;
        }


        void ReadFromFile(string filename)
        {
            var input = File.ReadAllLines(filename);

            n = Int32.Parse(input[0]);

            adjList = new List<List<int>>(n);
            for (int i = 0; i < n; i++)
                adjList.Add(new List<int> { });

            L = Int32.Parse(input[n + 1]);
            A = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                var line = input[i + 1].Split();
                for (int j = 0; j < n; j++)
                {
                    if (line[j] == "-")
                        A[i, j] = Int32.MaxValue;
                    else
                    {
                        A[i, j] = Int32.Parse(line[j]);
                        //if (A[i, j] != 0)
                            adjList[i].Add(j);
                    }
                }
            }
        }
        public List<int> Greedy()
        {
            var result = new List<int>();
            var coveredVertices = new List<int>();
            while (coveredVertices.Count < n)
            {
                var currMax = 0;
                var curVert = 0;
                // На каждом шаге добавляем вершину
                // с наибольшим покрытием еще не покрытых вершин
                for (int i = 0; i < n; i++)
                {
                    var notYetAdded = 0;
                    foreach (var adjE in adjList[i])
                        if (!coveredVertices.Contains(adjE) && A[i, adjE] <= L)
                            notYetAdded++;
                    if (notYetAdded >= currMax)
                    {
                        currMax = notYetAdded;
                        curVert = i;
                    }
                }
                foreach (var adjE in adjList[curVert])
                    if (A[curVert, adjE] <= L && !coveredVertices.Contains(adjE))
                        coveredVertices.Add(adjE);
                result.Add(curVert + 1);
            }
            return result;
        }

        public List<int> ExhaustiveSearch()
        {
            var result = new List<int>();
            var backupResult = new List<int>();
            var s = new int[n];
            var b = new int[n + 1];
            for (int i = 0; i < n + 1; i++)
                b[i] = i + 1;
            int x = 0;
            var currAdjVert = new List<int>();
            var minLength = n; 
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
                    //　добавлять только те вершины в покрытие, которые отстают не больше чем на L
                    foreach (var i in adjList[x - 1])
                    {
                        if (A[x - 1, i] <= L)
                            currAdjVert.Add(i);
                    }
                }
                if (s[x - 1] == 0)
                {
                    foreach (var i in adjList[x - 1])
                    {
                        if (A[x - 1, i] <= L)
                            currAdjVert.Remove(i);
                    }
                }
                //for (int i = 0; i < n; i++)
                //    if (s[i] == 1)
                //        Console.Write(i + 1);
                //Console.WriteLine();
                var noDupes = currAdjVert.Distinct().ToList();
                if (noDupes.Count == n)
                {
                    var sum = 0;
                    for (int i = 0; i < n; i++)
                    {
                        if (s[i] == 1)
                        {
                            sum += s[i];
                        }
                    }
                    if (sum <= minLength)
                    {
                        result.Clear();
                        minLength = sum;
                        for (int i = 0; i < n; i++)
                            if (s[i] == 1)
                                result.Add(i + 1);

                    }
                }
            }
            return result;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph("input.txt");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var exhaustiveSearch = graph.ExhaustiveSearch();
            stopWatch.Stop();
            foreach (int num in exhaustiveSearch)
                Console.Write(num + " ");
            Console.WriteLine(" - Алгоритм полного перебора (" + stopWatch.ElapsedMilliseconds + " мсек)");
            stopWatch.Reset();
            stopWatch.Start();
            var greedy = graph.Greedy();
            stopWatch.Stop();
            foreach (int num in greedy)
                Console.Write(num + " ");
            Console.WriteLine(" - Жадный алгоритм (" + stopWatch.ElapsedMilliseconds + " мсек)");
            Console.ReadKey();
            var a = new StringBuilder();
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (i == j)
                        a.Append(0);
                    else
                        a.Append(10);
                    a.Append(" ");
                }
                a.Append("\n");
            }
            File.WriteAllText("output.txt", a.ToString());
        }
    }
}
