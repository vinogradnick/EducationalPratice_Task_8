using System;
using System.Collections.Generic;
using System.IO;

namespace EducationalPratice_Task_8
{
    /// <summary>
    /// Граф
    /// </summary>
    public partial class Graph
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
                int status = 0;
                if (tree) //Если генерируем граф типа дерево
                    try
                    {
                        while (status < edges)
                            for (int i = 0; i < edges; i++)
                            {
                                for (int r = 0; r < _rd.Next(edges - i); r++)
                                    Edgelist.Add(new Edge(i, 0));
                                Edgelist[i][1] = status + 1;
                                status++;
                            }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return GenerateEdgeGraph(tree, peaks, edges);
                    }
                else
                    for (int i = 0; i < edges; i++)
                    {
                        int num = _rd.Next(peaks);
                        int num1 = _rd.Next(peaks);
                        Edgelist.Add(new Edge(num, num1));
                    }
                foreach (Edge  edge in Edgelist)
                    Console.WriteLine($"({edge.StartPeak})->({edge.EndPeak})");
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
                if (GraphExtension.ConvertMatrixAdjency(matrixGraph))
                {
                    Console.WriteLine("Матрица инцеденции:");
                    matrixGraph.Show();
                    return matrixGraph;
                }
                else
                    return GenerateAnyGraph(matrixGraph.GetLength(0), matrixGraph.GetLength(1));
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
        /// <summary>
        /// Найти 2 вершины по столбцу матрицы инцеденции
        /// </summary>
        /// <param name="edgeMatrix"></param>
        /// <returns>2 вершины</returns>
        public static Graph.Edge FindPeak(bool[] edgeMatrix)
        {
            List<int> temp = new List<int>();//Временный список
            for (int i = 0; i < edgeMatrix.Length; i++)
                if (edgeMatrix[i])
                    temp.Add(i);//Добавление в список
            return new Graph.Edge(temp[0], temp[1]);//Вернуть ребро
        }
        public  static bool[,] ConvertMatrixAdjency(bool[,] matrix, out List<Graph.Edge> edges )
        {
            List<Graph.Edge> tempEdge = new List<Graph.Edge>();//Временный набор ребер графа для построения матрицы смежности
            bool[,] ctx = new bool[matrix.GetLength(0), matrix.GetLength(0)];//Матрица смежности
            int eg = 0;//Переменная для подсчета столбцов
            try
            {
                while (matrix.GetLength(1) > eg)//Пока все столбцы матрицы инциденции не перебраны
                {
                    bool[] temp = new bool[matrix.GetLength(0)];//Временнный массив для заполнения вершинами
                    for (int peaks = 0; peaks < matrix.GetLength(0); peaks++)
                        temp[peaks] = matrix[peaks, eg];//Заполняем матрицу элементами
                    tempEdge.Add(FindPeak(temp));//Добавляем ребро выполняя поиск 2-х вершин
                    eg++;//Увелечение счетчика
                }
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    ctx[tempEdge[i][0], tempEdge[i][1]] = true;
                    ctx[tempEdge[i][1], tempEdge[i][0]] = true;
                }
                foreach (var edge in tempEdge)
                    Console.WriteLine(edge.ToString());
                edges = tempEdge;
                Console.WriteLine("Матрица смежности:");
                ctx.Show();//Вывод матрицы смежности и запись файл
                return ctx;//Возращаем матрицу смежности
            }
            catch (Exception)
            {
                Console.WriteLine("Матрица инцидентности составлена неправильно \n");
            }
            edges = null;
            return null;
        }
        public static bool ConvertMatrixAdjency(bool[,] matrix)
        {
            List<Graph.Edge> tempEdge = new List<Graph.Edge>();//Временный набор ребер графа для построения матрицы смежности
            bool[,] ctx = new bool[matrix.GetLength(0), matrix.GetLength(0)];//Матрица смежности
            int eg = 0;//Переменная для подсчета столбцов
            try
            {
                while (matrix.GetLength(1) > eg)//Пока все столбцы матрицы инциденции не перебраны
                {
                    bool[] temp = new bool[matrix.GetLength(0)];//Временнный массив для заполнения вершинами
                    for (int peaks = 0; peaks < matrix.GetLength(0); peaks++)
                        temp[peaks] = matrix[peaks, eg];//Заполняем матрицу элементами
                    tempEdge.Add(FindPeak(temp));//Добавляем ребро выполняя поиск 2-х вершин
                    eg++;//Увелечение счетчика
                }
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    ctx[tempEdge[i][0], tempEdge[i][1]] = true;
                    ctx[tempEdge[i][1], tempEdge[i][0]] = true;
                }
                tempEdge =new List<Graph.Edge>();
                for (int i = 0; i < ctx.GetLength(0); i++)
                for (int j = 0; j < ctx.GetLength(1); j++)
                    if (ctx[i, j])
                        tempEdge.Add(new Graph.Edge(i, j));


                foreach (var item in tempEdge)
                    Console.WriteLine(item.ToString());

                return true;//Возращаем матрицу смежности
            }
            catch (Exception)
            {
                Console.WriteLine("Матрица инцидентности составлена неправильно");
                return false;
            }
        }
    }

   
}
