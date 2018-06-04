using System;
using System.Collections.Generic;

namespace EducationalPratice_Task_8
{
    partial class Graph
    {
        private int _Peaks;

        private int _Edges;

        public List<Edge> Edges = new List<Edge>();

        private bool[,] _encidence;
        /// <summary>
        /// Конструктор с параметрами графа
        /// </summary>
        /// <param name="peaks"> количество вершин</param>
        /// <param name="edges">количество ребер</param>
        public Graph(int peaks,int edges)
        {
            _Peaks = peaks;
            _Edges = edges;
        }
        /// <summary>
        /// Найти 2 вершины по столбцу матрицы инцеденции
        /// </summary>
        /// <param name="edgeMatrix"></param>
        /// <returns>2 вершины</returns>
        public Edge FindPeak(bool[] edgeMatrix)
        {
            List<int> temp =new List<int>();//Временный список
            for (int i = 0; i < edgeMatrix.Length; i++)
                if (edgeMatrix[i])
                    temp.Add(i);//Добавление в список
            return new Edge(temp[0],temp[1]);//Вернуть ребро
        }
        /// <summary>
        /// Конвертирование матрицы инциденции в матрицу смежности
        /// </summary>
        /// <param name="matrix">матрица инциденции</param>
        public void ConvertMatrixToGraph(bool[,] matrix)
        {
            List<Edge> tempEdge = new List<Edge>();//Временный набор ребер графа для построения матрицы смежности
            bool[,] ctx = new bool[matrix.GetLength(0),matrix.GetLength(0)];//Матрица смежности
            int eg = 0;//Переменная для подсчета столбцов

            
            while (matrix.GetLength(1)>eg)//Пока все столбцы матрицы инциденции не перебраны
            {
                bool[] temp = new bool[matrix.GetLength(0)];//Временнный массив для заполнения вершинами
                for (int peaks = 0; peaks < matrix.GetLength(0); peaks++)
                    temp[peaks] = matrix[peaks, eg];//Заполняем матрицу элементами
                tempEdge.Add(FindPeak(temp));//Добавляем ребро выполняя поиск 2-х вершин
                eg++;//Увелечение счетчика
            }
            
            //Заполнение элементов матрицы
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                ctx[tempEdge[i][0], tempEdge[i][1]] = true;
                ctx[tempEdge[i][1], tempEdge[i][0]] = true;
            }

            //Печать ребер матрицы
            foreach (var edge in tempEdge)
                Console.WriteLine(edge.ToString());

            ctx.Show();//Вывод матрицы смежности и запись файл
        }
        /// <summary>
        /// Конструктор графа по матрице инцидентности
        /// </summary>
        /// <param name="matrix"></param>
        public Graph(bool[,] matrix)
        {
            _Peaks = matrix.GetLength(0);
            _Edges = matrix.GetLength(1);
            _encidence = matrix;
        }
        /// <summary>
        /// Проверка является ли граф деревом
        /// </summary>
        public void Check(bool[,] matrix )
        {
            if (_Edges != _Peaks - 1)
                Console.WriteLine("Граф не является деревом");
            else
            {
            }
        }

    }

   
}
