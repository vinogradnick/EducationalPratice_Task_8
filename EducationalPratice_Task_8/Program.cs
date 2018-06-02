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
            Graph graph= new Graph(5,4);//Создание графа заданого количества вершин и ребер
            Graph.GraphGenerator.GenerateTree(20,19);
            Graph.GraphGenerator.GenerateAnyGraph(5, 4);
            Console.ReadKey();
        }

        
    }

   
}
