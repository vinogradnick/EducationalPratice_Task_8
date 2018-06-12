using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
          program();
           
        }

        static void program()
        {
           
                Console.WriteLine("Алгоритм генерации графа и проверки его на дерево");
                int peaks = Input("Введите количество вершин");
                int edges = Input("Введите количество ребер");
                if (peaks - 1 != edges)
            {
                Console.WriteLine("Граф не является деревом т.к. количество ребер не равно количеству вершин -1");
                program();

            }
                else
                {
                    choiseGraph(peaks, edges).Check();
                    Console.WriteLine("Для выхода нажмите 0 или любую другую для продолжения");
                    switch (Console.ReadLine())
                    {
                          case "0":
                              Environment.Exit(Environment.ExitCode);
                              break;
                          default:
                              program();
                              break;
                              
                    }
                }
            
        }
        static int Input(string message)
        {
            Console.WriteLine(message);
            bool status = false;
            int res;
            do
            {
                status = int.TryParse(Console.ReadLine(), out res) & res>0;
                if (!status)
                    Console.WriteLine("Вы ввели неверное число");
            } while (!status);

            return res;

        }

        static Graph choiseGraph(int peak,int edges)
        {

            Console.WriteLine("1-Сгенерировать граф типа дерево");
            Console.WriteLine("2-Сгенерировать обычный граф");
            switch (Console.ReadLine())
            {
                case "1":
                    return new Graph(Graph.GraphGenerator.GenerateTree(peak,edges));
                case "2":
                    return new Graph(Graph.GraphGenerator.GenerateAnyGraph(peak, edges));
                default:
                    Console.WriteLine("Выбран неверный вариант");
                   return choiseGraph(peak,edges);

            }
        }
        


    }

   
}
