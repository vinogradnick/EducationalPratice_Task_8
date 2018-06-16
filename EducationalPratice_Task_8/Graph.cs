using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace EducationalPratice_Task_8
{
    public partial class Graph
    {
        public int Peaks { get; set; }
        public int Edges { get; set; }
        static public List<Edge> Edgelist;
        List<string> catalogCycles =new List<string>();
        public bool[,] EncidenceMatrix { get; private set; }
        public bool[,] AdjacencyMatrix { get ; }

        /// <summary>
        /// Конструктор графа по матрице инцедентности
        /// </summary>
        /// <param name="matrix"></param>
        public Graph(bool[,] matrix)
        {
            Peaks = matrix.GetLength(0);
            Edges = matrix.GetLength(1);
            EncidenceMatrix = matrix;
            AdjacencyMatrix = GraphExtension.ConvertMatrixAdjency(matrix,out Edgelist);
           
        }
        /// <summary>
        /// Проверка является ли граф деревом
        /// </summary>
        public void Check()
        {
            if (Edges == Peaks - 1 && !SearchCircle())
                Console.WriteLine("Граф  является деревом");
            else
                Console.WriteLine("Граф не явлется деревом");
        }
        static List<int[]> cycles = new List<int[]>();

        private bool SearchCircle()
        {
            cycles =new List<int[]>();
            bool status = false;
            foreach (var edge in Edgelist)
            {
                SearchRepeats(new int[]{edge.StartPeak});
                SearchRepeats(new int[]{edge.EndPeak});
            }     
                        Console.WriteLine("dfs");
                foreach (int[] cy in cycles)
                {
                    string s = "" + cy[0];

                    for (int i = 1; i < cy.Length; i++)
                        s += "," + cy[i];

                    Console.WriteLine(s);
                    status = true;
                }

            return status;
        }


        /// <summary>
        /// Поиск новых циклов
        /// </summary>
        /// <param name="route">Вершину ребра</param>
        static void SearchRepeats(int[] route)
        {
            int startPeak = route[0];//Присвоение стартовой вершины
            int endPeak;//Временная переменная
            int[] temp = new int[route.Length + 1];//резервируем мето для пути 
            //Цикл по не перебраны все ребра в графе
            foreach (var edge in Edgelist)
                for (int vertex = 0; vertex < 2; vertex++)
                    if (edge[vertex] == startPeak) //Если стартовая вершина ребра графа = вершине ребра графа
                        //с которого выполняется обход
                    {
                        //Получаем конечную вершину ребра графа
                        endPeak = edge[(vertex + 1) % 2]; // y + 1 % 2 для получения конечной вершины по данному индексу
                        if (!IsPeakVisited(endPeak, route)) //Если вершина не посещена в пути
                        {
                            temp[0] = endPeak; //добавляем вершину в ребро
                            Array.Copy(route, 0, temp, 1, route.Length); //Копируем верину в следующее ребро
                            SearchRepeats(temp); //ищем новый путь
                        }
                        else
                        {
                            int[] p = Rotate(route); //поворот пути чтобы начался с самый маленькой вершины
                            if ((route.Length > 2) && (endPeak == route[route.Length - 1]) && IsFindNewRoute(p) &&
                                IsFindNewRoute(Invert(p)))
                                //Проверка если длина пути > 2 тогда есть есть цикл и
                                //(вершина = последней вершине в пути)
                                cycles.Add(p); //Добавляем путь  в цикл
                        }
                    }
        }

        /// <summary>
        /// Проверка двух путей в графе
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static bool equals(int[] a, int[] b)
        {
            bool ret = (a[0] == b[0]) && (a.Length == b.Length);

            for (int i = 1; ret && (i < a.Length); i++)
                if (a[i] != b[i])
                    return false;

            return ret;
        }
        /// <summary>
        /// Инвертирование пути 
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        static int[] Invert(int[] route)
        {
            int[] p = new int[route.Length];

            for (int i = 0; i < route.Length; i++)
                p[i] = route[route.Length - 1 - i];

            return Rotate(p);
        }

        //  повернуть путь цикла чтобы он начинался с самый маленькой вершниы
        static int[] Rotate(int[] route)
        {
            int[] p = new int[route.Length];//путь 
            int x = Smallest(route);//Минимальная вершина в пути

            Array.Copy(route, 0, p, 0, route.Length);//копирование из 1 вершины в другую на размер

            while (p[0] != x)//Пока первая вершина в пути не = минимальной вершине в пути
            {
                var peak = p[0];//вершина
                Array.Copy(p, 1, p, 0, p.Length - 1);//Копирование 
                p[p.Length - 1] = peak;
            }

            return p;
        }
        //Если путь новый
        static bool IsFindNewRoute(int[] route)
        {
            foreach (int[] p in cycles)
                if (equals(p, route))
                    return false;

            return true;
        }
        //Поиск минимальной вершниы
        static int Smallest(int[] route)
        {
            int min = route[0];//первая вершина

            foreach (int p in route)
                if (p < min)
                    min = p;//Происвоение минимального

            return min;
        }
        /// <summary>
        /// Проверка посещения вершниы графа
        /// </summary>
        /// <param name="n"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        static bool IsPeakVisited(int peak, int[] route)
        {

            foreach (int p in route)//Перебираем путь
                if (p == peak) //Если вершина есть в пути

                    return true;
            return false;
        }
    }

   
}
