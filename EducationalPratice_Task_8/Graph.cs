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
            for (int i = 0; i < Edgelist.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    findNewCycles(new int[]{Edgelist[i][j]});
                }
            }
            
          
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
        /// <param name="path">Ребро графа</param>
        static void findNewCycles(int[] path)
        {
            int n = path[0];//Присвоение стартовой вершины
            int x;//Временная переменная
            int[] sub = new int[path.Length + 1];//Следующее ребро
            //Цикл по не перебраны все ребра в графе
            for (int i = 0; i < Edgelist.Count; i++)
                for (int y = 0; y <= 1; y++)

                    if (Edgelist[i][y] == n)//Если стартовая вершина ребра графа = вершине ребра графа
                                         //с которого выполняется обход
                                         //  edge referes to our current node
                    {
                        //Получаем конечную вершину ребра графа
                        x = Edgelist[i][(y + 1) % 2];
                        if (!visited(x, path))//Если вершина не посещена в пути
                        {
                            sub[0] = x;//добавляем вершину в ребро
                            Array.Copy(path, 0, sub, 1, path.Length);//Копируем верину в следующее ребро
                            findNewCycles(sub);//ищем новый путь
                        }
                        else if ((path.Length > 2) && (x == path[path.Length - 1]))//Проверка если длина пути > 2 тогда есть есть цикл и
                                                                                   //(вершина = последней вершине в пути)
                        {
                            int[] p = normalize(path);//поворот пути чтобы начался с самый маленькой вершины
                            int[] inv = invert(p);//Инвертирование пути
                            if (isNew(p) && isNew(inv))//Если путь новый и другой путь новый
                                cycles.Add(p);//Добавляем путь  в цикл
                        }
                    }
        }
        //проверка двух ребер
        static bool equals(int[] a, int[] b)
        {
            bool ret = (a[0] == b[0]) && (a.Length == b.Length);

            for (int i = 1; ret && (i < a.Length); i++)
                if (a[i] != b[i])
                {
                    ret = false;
                }

            return ret;
        }
        //Инвертирование графа в пути
        static int[] invert(int[] path)
        {
            int[] p = new int[path.Length];

            for (int i = 0; i < path.Length; i++)
                p[i] = path[path.Length - 1 - i];

            return normalize(p);
        }

        //  повернуть путь цикла чтобы он начинался с самый маленькой вершниы
        static int[] normalize(int[] path)
        {
            int[] p = new int[path.Length];//путь 
            int x = smallest(path);//Минимальная вершина в пути
            int n;//вершина

            Array.Copy(path, 0, p, 0, path.Length);//копирование из 1 вершины в другую на размер

            while (p[0] != x)//Пока первая вершина в пути не = минимальной вершине в пути
            {
                n = p[0];//Минимальная вершина в пути
                Array.Copy(p, 1, p, 0, p.Length - 1);//Копирование 
                p[p.Length - 1] = n;
            }

            return p;
        }
        //Если путь новый
        static bool isNew(int[] path)
        {
            bool ret = true;

            foreach (int[] p in cycles)
                if (equals(p, path))
                {
                    ret = false;
                    break;
                }

            return ret;
        }
        //Поиск минимальной вершниы
        static int smallest(int[] path)
        {
            int min = path[0];//первая вершина

            foreach (int p in path)
                if (p < min)
                    min = p;//Происвоение минимального

            return min;
        }
        /// <summary>
        /// Проверка посещения вершниы графа
        /// </summary>
        /// <param name="n"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        static bool visited(int n, int[] path)
        {
            bool ret = false;

            foreach (int p in path)//Перебираем путь
                if (p == n)//Если вершина есть в пути
                {
                    ret = true;
                    break;
                }

            return ret;
        }
    }

   
}
