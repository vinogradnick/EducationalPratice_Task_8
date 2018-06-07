using System;
using System.Collections.Generic;

namespace EducationalPratice_Task_8
{
    public partial class Graph
    {
        public int Peaks { get; set; }
        public int Edges { get; set; }
        public List<Edge> Edgelist;

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
           
            if (Edges == Peaks - 1 && this.SearchCircle())
                Console.WriteLine("Граф  является деревом");
            else
                Console.WriteLine("Граф не явлется деревом");
        }
        /// <summary>
        /// Поиск циклов в графе
        /// </summary>
        /// <returns></returns>
        public  bool SearchCircle()
        {
            List<Edge> list = new List<Edge>();
            List<int> used = new List<int>();//Использованные вершины
            int counter = 0;
            foreach (Edge egd in  Edgelist)
            {
                
                //Проверка если вершины не содержится
                if (!used.Contains(egd.EndPeak))
                    used.Add(egd.EndPeak);
                else
                    counter++;
            }

            return counter==0;
        }

      

       
    }

   
}
