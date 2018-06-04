using System;
using System.Collections.Generic;
using System.IO;

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
            public static List<Edge> GenerateEdgeGraph(bool tree, int peaks,int edges)
            {

                List<Edge> Edgelist = new List<Edge>(edges);

                if (tree)//Если генерируем граф типа дерево
                {
                    int status = 0;
                    while (status<edges)
                    {
                        for (int i = 0; i < edges; i ++)
                        {
                            for (int r = 0; r < _rd.Next(edges-i); r++)
                            {
                                Edgelist.Add(new Edge(i, 0));
                            }
                            Edgelist[i][1] = status + 1;

                            status++;
                        }
                    }                  
                }
                else
                    for (int i = 0; i < edges; i++)
                    {
                        int num = _rd.Next(peaks);
                        int num1 = _rd.Next(peaks);
                        Edgelist.Add(new Edge(num,num1));
                    }

                foreach (Edge  edge in Edgelist)
                    Console.WriteLine($"{edge.StartPeak}-{edge.EndPeak}");
                return Edgelist;
            }
            /// <summary>
            /// Генерация матрицы инцедентности графа типа деерева
            /// </summary>
            /// <returns></returns>
            ///
            public static bool[,] GenerateTree(int peaks,int edges)
            {
                List<Edge>Edgelist = GenerateEdgeGraph(true,peaks,edges);

                bool[,] matrixTree = new bool[peaks,edges];

                
                for (int i = 0; i < matrixTree.GetLength(0); i++)
                for (int j = 0; j < matrixTree.GetLength(1); j++)
                    if (Edgelist[j][0] == i || Edgelist[j][1] == i)
                        matrixTree[i, j] = true;
                    else
                        matrixTree[i, j] = false;
                
                matrixTree.Show();

                return matrixTree;
            }
            public static bool[,] GenerateTree(Graph graph)
            {
                List<Edge>Edgelist = GenerateEdgeGraph(true,graph._peaks,graph._edges);

                bool[,] matrixTree = new bool[graph._peaks,graph._edges];

                
                for (int i = 0; i < matrixTree.GetLength(0); i++)
                for (int j = 0; j < matrixTree.GetLength(1); j++)
                    if (Edgelist[j][0] == i || Edgelist[j][1] == i)
                        matrixTree[i, j] = true;
                    else
                        matrixTree[i, j] = false;
                
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
                List<Edge> Edgelist = new List<Edge>();
                #region  FillPeaks

                for (int i = 0; i < peaks; i++)
                    Edgelist.Add(new Edge(_rd.Next(peaks), _rd.Next(peaks)));


                for (int i = 0; i < edges; i++)
                    Console.WriteLine($"{Edgelist[i].StartPeak}-{Edgelist[i].EndPeak}");

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
            StreamWriter writer = new StreamWriter($"tree_gen_{matrix.GetLength(0)}-{matrix.GetLength(1)}.txt");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    writer.Write(matrix[i, j] ? "1 " : "0 ");
                    if(matrix.GetLength(1)<100)
                        Console.Write(matrix[i, j] ? "1 " : "0 ");
                }
                writer.WriteLine();
                if(matrix.GetLength(1)<100)
                    Console.WriteLine();
            }
            
            writer.Close();
            writer.Dispose();
        }
        
    }

   
}
