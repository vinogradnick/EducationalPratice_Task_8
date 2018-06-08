using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EducationalPratice_Task_8
{
    public partial class Graph
    {
        public int Peaks { get; set; }
        public int Edges { get; set; }
        public List<Edge> Edgelist;
        public bool[] bolleans;
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
            bolleans = new bool[Peaks];
        }
        /// <summary>
        /// Проверка является ли граф деревом
        /// </summary>
        public void Check()
        {
           
            if (Edges == Peaks - 1 && SearchCircle())
                Console.WriteLine("Граф  является деревом");
            else
                Console.WriteLine("Граф не явлется деревом");
        }

        public bool SearchCircle()
        {
            bool[,] temp = AdjacencyMatrix;
            int peak1 = -1;
            int peak2 = -1;
            string[] peaks = new string[Peaks];
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    if (temp[i, j])
                    {
                        Console.Write($"({i})->({j})=>");
                        int count = 0;
                        while (count<Edgelist.Count)
                        {
                            
                        }
                    }
                }
            }
            Console.WriteLine();
           
            return true;
        }

       
    }

   
}
