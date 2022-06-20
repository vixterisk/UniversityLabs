using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GraphColoring
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            var graph = new Graph();
            //graph.GetAdjacencyMatrixFromFile(@"D:\input.txt");
            graph.GenerateAdjacencyMatrixRandomly(12);
            graph.PrintAdjacencyMatrix(); 
            stopWatch.Start();
            graph.GetBestColoringUsingBacktracking(); 
            stopWatch.Stop();
            Console.WriteLine((stopWatch.Elapsed));
            stopWatch.Reset();
            stopWatch.Start();
            graph.GreedyColoring(true);
            stopWatch.Stop();
            Console.WriteLine((stopWatch.Elapsed));
            stopWatch.Reset();
            stopWatch.Start();
            graph.GreedyColoring(false);
            stopWatch.Stop();
            Console.WriteLine((stopWatch.Elapsed));
        }
    }
}
