namespace EducationalPratice_Task_8
{
    public partial class Graph
    {
        /// <summary>
        /// Ребро
        /// </summary>
        public class Edge
        {
            public int StartPeak,//Начальная вершина
                        EndPeak;//Конечная вершина
            /// <summary>
            /// Конструктор с параметрами
            /// </summary>
            /// <param name="start"></param>
            /// <param name="end"></param>
            public Edge(int start, int end)
            {
                StartPeak = start;
                EndPeak = end;
            }

            public Edge()
            {
                StartPeak = 0;
                EndPeak = 0;
            }
            /// <summary>
            /// Индексатор ребер для получения
            /// </summary>
            /// <param name="index">индекс элемента</param>
            /// <returns></returns>
            public int this[int index]
            {
                
                get => index == 0 ? StartPeak : EndPeak;
                set
                {
                    if(index == 0)
                        StartPeak = value;
                    else
                        EndPeak = value;
                }
            }

            public bool Equals(Edge obj) => this.StartPeak == obj.StartPeak && this.EndPeak == obj.EndPeak;

            public override string ToString() => $"({StartPeak})->({EndPeak})";
        }

    }

   
}
