using System;
using System.Collections.Generic;
using System.IO;
using static EducationalPratice_Task_8.Graph;

namespace EducationalPratice_Task_8
{
    public static class GraphExtension
    {
        /// <summary>
        /// Печать графа
        /// </summary>
        /// <param name="matrix"></param>
        public static void Show(this bool[,] matrix)
        {
            
            StreamWriter writer = new StreamWriter($"graph_gen_size_{matrix.GetLength(0)}-{matrix.GetLength(1)}.txt");
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
                tempEdge=new List<Graph.Edge>();
                for (int i = 0; i < ctx.GetLength(0); i++)
                {
                    for (int j = 0; j < ctx.GetLength(1); j++)
                    {
                        if(ctx[i,j])
                        tempEdge.Add(new Graph.Edge(i,j));
                    }
                }

                tempEdge.RemoveRepeat();
                Console.WriteLine();
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

        public static List<Edge> RemoveRepeat(this List<Edge> edgelist)
        {
            int counter = 0;
            while (counter<edgelist.Count)
            {
                Edge edge = edgelist[counter];
                for (int i = 1; i < edgelist.Count; i++)
                {
                    if(edge!=edgelist[i])
                        if (edge.StartPeak == edgelist[i].EndPeak & edgelist[i].StartPeak == edge.EndPeak)
                        edgelist.RemoveAt(i);
                }

                counter++;

            }

            return edgelist;

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