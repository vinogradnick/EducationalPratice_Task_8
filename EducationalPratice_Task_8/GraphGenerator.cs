using System;
using System.Collections.Generic;

namespace EducationalPratice_Task_8
{
    /// <summary>
    /// Граф
    /// </summary>
    partial class Graph
    {
        public static class GraphGenerator
        {
           static Random _rd = new Random();

            /// <summary>
            /// Генерация ребер графа
            /// </summary>
            /// <param name="tree">для графа типа дерево?</param>
            /// <param name="peaks">вершины</param>
            /// <param name="edges">ребра</param>
            /// <returns></returns>
            public static List<int[]> GenerateEdgeGraph(bool tree, int peaks,int edges)
            {

                List<int[]> Edgelist = new List<int[]>(edges);

                if (tree)
                {
                    for (int i = 0; i < edges; i += 2)
                    {
                        Edgelist.Add(new[] {i, 0});
                        Edgelist.Add(new[] {i, 0});
                    }

                    for (int i = 0; i < edges; i++)
                        Edgelist[i][1] = i + 1;
                }
                else
                    for (int i = 0; i < edges; i++)
                        Edgelist.Add(new[] {_rd.Next(peaks), _rd.Next(peaks)});

                foreach (int[] edge in Edgelist)
                    Console.WriteLine($"{edge[0]}-{edge[1]}");
                return Edgelist;
            }
            /// <summary>
            /// Генерация матрицы инцедентности графа типа деерева
            /// </summary>
            /// <returns></returns>
            ///
            public static bool[,] GenerateTree(int peaks,int edges)
            {
                List<int[]>Edgelist = GenerateEdgeGraph(true,peaks,edges);

                bool[,] matrixTree = new bool[peaks,edges];
               
                for (int i = 0; i < matrixTree.GetLength(0); i++)
                {
                    for (int j = 0; j < matrixTree.GetLength(1); j++)
                    {
                        if (Edgelist[j][0] == i || Edgelist[j][1] == i)
                            matrixTree[i, j] = true;
                        else
                            matrixTree[i, j] = false;
                    }
                }
                matrixTree.Show();

                return matrixTree;
            }
            /// <summary>
            /// Генерация матрицы инцедентности для любого графа
            /// </summary>
            /// <param name="peaks">вершины</param>
            /// <param name="edges">ребра</param>
            /// <returns></returns>
            public static bool[,] GenerateAnyGraph(int peaks, int edges)
            {
                List<int[]> Edgelist = new List<int[]>();
                #region  FillPeaks

                List<int> V1 = new List<int>(5);
                List<int> V2 = new List<int>(5);

                for (int i = 0; i < peaks; i++)
                {
                    int number = _rd.Next(peaks);
                    if (!V1.Contains(number))
                    {
                        V1.Add(number);
                    }
                    else
                    {
                        while (V1.Contains(number))
                            number = _rd.Next(peaks);
                        V1.Add(number);
                    }
                }

                for (int i = 0; i < peaks; i++)
                {
                    int number = _rd.Next(peaks);
                    if (!V2.Contains(number))
                        V2.Add(number);
                    else
                    {
                        while (V2.Contains(number))
                            number = _rd.Next(peaks);
                        V2.Add(number);
                    }
                }

                for (int i = 0; i < edges; i++)
                {
                    Console.WriteLine($"{V1[i]}-{V2[i]}");
                    Edgelist.Add(new int[2] { V1[i], V2[i] });
                }


                #endregion
                bool[,] matrixGraph = new bool[peaks, edges];

                for (int i = 0; i < matrixGraph.GetLength(0); i++)
                for (int j = 0; j < matrixGraph.GetLength(1); j++)
                    if (Edgelist[j][0] == i || Edgelist[j][1] == i)
                        matrixGraph[i, j] = true;
                    else
                        matrixGraph[i, j] = false;
                matrixGraph.Show();
                return matrixGraph;
            }
            
        }
    }

    public static class GraphExtension
    {
        /// <summary>
        /// Печать графа
        /// </summary>
        /// <param name="matrix"></param>
        public static void Show(this bool[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    Console.Write(matrix[i, j] ? "1 " : "0 ");
                Console.WriteLine();
            }
        }
    }

   
}
