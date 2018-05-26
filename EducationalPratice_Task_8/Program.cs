using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPratice_Task_8
{
    class Program
    {
        /// <summary>
        /// 8.	Граф задан матрицей инциденций. Выяснить, является ли он деревом.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
           Graph gragh = new Graph(10,4);
            gragh.Generate();
            gragh.ShowGraph();
            Console.ReadLine();
        }

        
    }

    class  Graph
    {
        public int CountPeaks => _peaks;
        public int CountEdges => _edges;


        private int[,] MatrixEncidence;
        private List<int[]> EdgeList;//Список ребер
        private List<int> PeakList;//Список вершин
        private static Random rnd = new Random();
        private int _peaks;
        private int _edges;

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Graph()
        {
            _peaks = 7;
            _edges = 8;
            MatrixEncidence =  new int[_peaks,_edges];
            EdgeList = new List<int[]>(8);
        }

        /// <summary>
        /// Конструктор графа
        /// </summary>
        /// <param name="peaks">Количество вершин в графе</param>
        /// <param name="edges">Количество ребер в графе</param>
        public Graph(int peaks,int edges)
        {
            _peaks = peaks;
            _edges = edges;
            MatrixEncidence = new int[peaks,peaks];
            PeakList =new List<int>(_peaks);
            EdgeList = new List<int[]>(_edges);
        }

        public void Generate()
        {
            


            for (int i = 0; i <_edges; i++)
                EdgeList.Add(new int[2] {rnd.Next(_peaks), rnd.Next(_peaks)});

            for (int i = 0; i < PeakList.Capacity; i++)
                PeakList.Add(i);

            for (int i = 0; i < _edges;i++)
            {
                Console.WriteLine($"{EdgeList[i][0] }-{EdgeList[i][1]}");
                MatrixEncidence[EdgeList[i][0], EdgeList[i][1]] = 1;
                MatrixEncidence[EdgeList[i][1], EdgeList[i][0]] = -1;
            }

            for (int i = 0; i < MatrixEncidence.GetLength(0); i++)
            for (int j = 0; j < MatrixEncidence.GetLength(1); j++)
                if (MatrixEncidence[i, j] != 1)
                    MatrixEncidence[i, j] = 0;
        }

        public void ShowGraph()
        {
            Console.Write("Вершины графа :");
            foreach (int peak in PeakList)
                Console.Write(peak + " ");
            Console.WriteLine("Ребра графа");

            for(var index = 0; index < EdgeList.Count; index++)
                Console.WriteLine($"Ребро[{index}]:({EdgeList[index][0]}-{EdgeList[index][1]}) ");

            Console.WriteLine();
            Console.Write(" ");
            for (int i = 0; i < _edges; i++)
                Console.Write(" " + i);
            Console.WriteLine();

            for (int i = 0; i < _peaks; i++)
            {
                Console.Write(PeakList[i]+"|");
                for (int j = 0; j <_edges; j++)
                    Console.Write(MatrixEncidence[i,j] + " ");
                Console.WriteLine();
            }
        }

        public void isTree()
        {
            if (_peaks - 1 != _edges)
                Console.WriteLine("Граф не является деревом");
            else
                Console.WriteLine("Граф является деревом");
        }
    }
}
