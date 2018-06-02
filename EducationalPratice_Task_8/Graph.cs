using System;

namespace EducationalPratice_Task_8
{
    partial class Graph
    {
        private int Peaks;
        private int Edges;
        
        /// <summary>
        /// Конструктор с параметрами графа
        /// </summary>
        /// <param name="peaks"> количество вершин</param>
        /// <param name="edges">количество ребер</param>
        public Graph(int peaks,int edges)
        {
            Peaks = peaks;
            Edges = edges;
        }
        

        public void Check()
        {
            if (Edges != Peaks - 1)
            {
                Console.WriteLine("Граф не является деревом");
            }
            else
            {

            }
        }

    }

   
}
