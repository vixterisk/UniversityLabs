using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GraphColoring
{
    class Graph
    {
        public int[,] AdjacencyMatrix { get; set; }
        int nodesAmount;
        public Graph()
        {
        }

        public Graph(int[,] adjacencyMatrix)
        {
            AdjacencyMatrix = adjacencyMatrix;
            nodesAmount = adjacencyMatrix.GetLength(0);
        }

        public void PrintAdjacencyMatrix()
        {
            Console.WriteLine("Adjacency Matrix: ");
            for (int i = 0; i < nodesAmount; i++)
            {
                for (int j = 0; j < nodesAmount; j++)
                    Console.Write(AdjacencyMatrix[i, j] + " ");
                Console.WriteLine();
            }
        }

        int[,] AdjacencyMatrixGeneratedRandomly(int n)
        {
            var rand = new Random();
            nodesAmount = n;
            var result = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                result[i, i] = 0;
                for (int j = i + 1; j < n; j++)
                    result[i, j] = result[j, i] = rand.Next(0, 2);
            }
            return result;
        }

        public void GenerateAdjacencyMatrixRandomly(int nodesCount)
        {
            AdjacencyMatrix = AdjacencyMatrixGeneratedRandomly(nodesCount);
        }

        int[,] AdjacencyMatrixFromFile(string path)
        {
            var input = File.ReadAllLines(path);
            int n = nodesAmount = input.GetLength(0);
            var array = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                array[i, i] = 0;
                var splitted = input[i].Split();
                for (int j = i + 1; j < n; j++)
                    array[i, j] = array[j, i] = Int32.Parse(splitted[j]);
            }
            return array;
        }

        public void GetAdjacencyMatrixFromFile(string path)
        {
            AdjacencyMatrix = AdjacencyMatrixFromFile(path);
        }

        bool isColoringCorrect(int[] coloring, int positionStart, int positionEnd)
        {
            if (positionStart == positionEnd) return true;
            for (int i = positionStart + 1; i < positionEnd; i++)
                if (coloring[positionStart] == coloring[i] && AdjacencyMatrix[positionStart, i] == 1) return false;
            return isColoringCorrect(coloring, positionStart + 1, positionEnd);
        }

        int GenerateAllColorings(int[] nodes, int position, int minCount, ref List<int> correctMinimalColoring)
        {
            if (position == nodes.Length)
            {
                if (isColoringCorrect(nodes, 0, nodes.Length))
                {
                    if (minCount >= nodes.Distinct().Count())
                    {
                        minCount = nodes.Distinct().Count();
                        correctMinimalColoring = nodes.ToList();
                    }
                    //foreach (int i in nodes)
                    //    Console.Write(i + " ");
                    //Console.WriteLine();
                }
                return minCount;
            }
            if (nodes.Distinct().Count() - 1 > minCount || !isColoringCorrect(nodes, 0, position)) return minCount;
            for (int i = nodes.Length - 1; i >=0; i--)
            {
                nodes[position] = i + 1;
                minCount = GenerateAllColorings(nodes, position + 1, minCount, ref correctMinimalColoring);
            }
            return minCount;
        }

        public void GetBestColoringUsingBacktracking()
        {
            List<int> minColoring = new List<int>();
            var minCount = GenerateAllColorings(new int[nodesAmount], 0, nodesAmount, ref minColoring);
            var result = Tuple.Create(minCount, minColoring);
            Console.WriteLine("Minimal color number using Backtracking: " + result.Item1);
            for (int i = 0; i < result.Item2.Count; i++)
            {
                Console.WriteLine("Node: " + (i + 1) + ", Color: " + result.Item2[i]);
            }
        }

        Tuple<int, int>[] CountDegree()
        {
            var result = new Tuple<int, int>[nodesAmount];
            for (int i = 0; i < nodesAmount; i++)
            {
                var s = 0;
                result[i] = Tuple.Create(i, s);
                for (int j = 0; j < nodesAmount; j++)
                {
                    if (AdjacencyMatrix[i, j] == 1)
                    {
                        s++;
                        result[i] = Tuple.Create(i, s);
                    }
                }
            }
            return result;
        }

        class DegreeComparer : IComparer
        {
            int IComparer.Compare(Object x, Object y)
            {
                var X = (Tuple<int, int>)x;
                var Y = (Tuple<int, int>)y;
                return Y.Item2.CompareTo(X.Item2);
            }
        }

        public void GreedyColoring(bool withSort)
        {
            Tuple<int, int>[] degrees = new Tuple<int, int>[nodesAmount];
            if (withSort)
            {
                degrees = CountDegree();
                Array.Sort(degrees, new DegreeComparer());
                var newAdjacencyMatrix = new int[nodesAmount, nodesAmount];
                var newMatrix = new int[nodesAmount, nodesAmount];
                for (int i = 0; i < nodesAmount; i++)
                    for (int j = 0; j < nodesAmount; j++)
                        newMatrix[i, j] = AdjacencyMatrix[degrees[i].Item1, degrees[j].Item1];
                AdjacencyMatrix = newMatrix;
            }
            int[] color = new int[nodesAmount];
            color[0] = 0;    //Assign first color for the first node
            bool[] colorUsed = new bool[nodesAmount];    //Used to check whether color is used or not

            for (int i = 1; i < nodesAmount; i++)
                color[i] = -1;    //initialize all other vertices are unassigned

            for (int i = 0; i < nodesAmount; i++)
                colorUsed[i] = false;    //initially any colors are not chosen

            for (int u = 1; u < nodesAmount; u++)
            {    //for all other NODE - 1 vertices
                for (int v = 0; v < nodesAmount; v++)
                {
                    if (AdjacencyMatrix[u, v] == 1)
                    {
                        if (color[v] != -1)    //when one color is assigned, make it unavailable
                            colorUsed[color[v]] = true;
                    }
                }

                int col;
                for (col = 0; col < nodesAmount; col++)
                    if (!colorUsed[col])    //find a color which is not assigned
                        break;

                color[u] = col;    //assign found color in the list

                for (int v = 0; v < nodesAmount; v++)
                {    //for next iteration make color availability to false
                    if (AdjacencyMatrix[u, v] == 1)
                    {
                        if (color[v] != -1)
                            colorUsed[color[v]] = false;
                    }
                }

            }
            if (withSort)
                Console.WriteLine("Minimal color number using Greedy Coloring with Sort: " + color.Distinct().Count());
            else
                Console.WriteLine("Minimal color number using Greedy Coloring: " + color.Distinct().Count());
            for (int u = 0; u < nodesAmount; u++)
                if (withSort)
                    Console.WriteLine("Node: " + degrees[u].Item1 + ", Color: " + (color[degrees[u].Item1] + 1));
                else
                    Console.WriteLine("Node: " + u + ", Color: " + (color[u] + 1));
        }
    }
}
