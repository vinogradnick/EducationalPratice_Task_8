using System;
using System.Linq;
using System.Text;
using System.Threading;
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
            while (!Console.CapsLock)
            {
                Graph graph1 = new Graph(Graph.GraphGenerator.GenerateTree(11,10));
                graph1.Check();
                Console.ReadKey();
            }
           // Graph graph= new Graph(Graph.GraphGenerator.GenerateTree(5, 4));//Создание графа заданого количества вершин и 
            // bool[,] matrix = Graph.GraphGenerator.GenerateAnyGraph(5, 4);
            
        }

        
    }

   
}
