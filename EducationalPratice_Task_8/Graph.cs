using System;
using System.Collections.Generic;

namespace EducationalPratice_Task_8
{
    partial class Graph
    {
        private int _peaks;

        private int _edges;

        public List<Edge> Edges = new List<Edge>();
        private bool[] visited;
        private List<Int32> adjList;

        private bool[,] _encidence;
        /// <summary>
        /// Конструктор с параметрами графа
        /// </summary>
        /// <param name="peaks"> количество вершин</param>
        /// <param name="edges">количество ребер</param>
        public Graph(int peaks,int edges)
        {
            _peaks = peaks;
            _edges = edges;
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
        public bool[,] ConvertMatrixToGraph(bool[,] matrix)
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
            return ctx;//Возращаем матрицу смежности
        }
        /// <summary>
        /// Конструктор графа по матрице инцидентности
        /// </summary>
        /// <param name="matrix"></param>
        public Graph(bool[,] matrix)
        {
            _peaks = matrix.GetLength(0);
            _edges = matrix.GetLength(1);
            _encidence = matrix;
        }
        /// <summary>
        /// Проверка является ли граф деревом
        /// </summary>
        public void Check(bool[,] matrix )
        {
            if (_edges != _peaks - 1)
                Console.WriteLine("Граф не является деревом");
            else
            {
            }
        }
        /// <summary>
        /// Поиск в глубину в графе для поиска циклов
        /// 1- серый цвет
        /// 0- белый цвет
        /// 2- черный цвет
        /// </summary>
        /// <param name="v"></param>
        public static void SearchDeep(ref bool[,] tree) // Метод поиска в глубину и высота дерева   
        {
            int height1 = 0; int height2 = 0;
            List<int> nodeDFS = new List<int>();  // создаем пустой список для перечисления вершин обхода в глубину
            nodeDFS.Add(0); // Добавляем корневую вершину, с которой начинается обход - вершина 0
            bool[,] assist = (bool[,])tree.Clone(); // копируем матрицу смежности во вспомогательный массив
            int var = 0;
            int a, b;
 
            for (a = 0; a < assist.GetLength(0); a++) // !!!здесь идет сбой цикла когда var=b
            {
                if (height2 < height1)
                {
                    height2 = height1;
                    height1 = 0;
                }
                for (b = a + 1; b < assist.GetLength(1); b++)
                {
                    if (assist[a, b])
                    {
                        height1++;
                        nodeDFS.Add(b);
                        assist[a, b] = false;
                        break;
                    }

                }
            }
            Console.WriteLine("Выведем список вершин при обходе дерева в глубину:");
 
            for (int i = 0; i < nodeDFS.Count; i++)
            {
                Console.WriteLine("\t" + nodeDFS[i]);
            }
 
            Console.WriteLine("Высота дерева:" + height2);
            
        }

    }

   
}
