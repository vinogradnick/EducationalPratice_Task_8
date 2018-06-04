using System;
using System.Collections.Generic;

namespace EducationalPratice_Task_8
{
    partial class Graph
    {

        public int _Peaks;

        public int _Edges;

        public List<Edge> Edges = new List<Edge>();

        public bool[,] Encidence;
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

        public Edge Azaza(bool[] azaz)
        {
            List<int> add =new List<int>();
            for (int i = 0; i < azaz.Length; i++)
            {
                if (azaz[i])
                {
                    add.Add(i);
                }
            }
            return new Edge(add[0],add[1]);
        }
        public void ConvertMatrixToGraph(bool[,] matrix)
        {
            int n = 0;
            List<Edge> tempEdge = new List<Edge>();
            bool[,] ctx = new bool[matrix.GetLength(0),matrix.GetLength(0)];
            int eg = 0;

            while (matrix.GetLength(1)>eg)
            {
                bool[] az = new bool[matrix.GetLength(0)];
                for (int peaks = 0; peaks < matrix.GetLength(0); peaks++)
                {
                    az[peaks] = matrix[peaks, eg];
                }
                tempEdge.Add(Azaza(az));
                eg++;
            }

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                ctx[tempEdge[i][0], tempEdge[i][1]] = true;
                ctx[tempEdge[i][1], tempEdge[i][0]] = true;

            }
            
          

            foreach (var edge in tempEdge)
            {
                Console.WriteLine(edge.ToString());
            }
            ctx.Show();
        }
        public Graph(bool[,] matrix)
        {
            _Peaks = matrix.GetLength(0);
            _Edges = matrix.GetLength(1);
            Encidence = matrix;
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
