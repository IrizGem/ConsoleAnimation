using System;
using System.Text;

namespace ConsoleTree
{
    internal class Program
    {
        static Random random = new Random();
        static Drawer drawer = new Drawer(50, 150);
        
        static void Main(string[] args)
        {
            //Console.ReadLine();
            //Console.ReadKey();

            Console.SetWindowSize(Console.WindowWidth, Console.LargestWindowHeight / 2);
            //Console.SetWindowPosition(Console.WindowLeft-1,Console.WindowTop);
            int w = Console.WindowWidth;
            int h = Console.WindowHeight;
            drawer = new Drawer(w, h);
            
            Branch branch = new Branch();
            for (int i = 0; i < 300; i++)
            {
                branch.Grow();
                Draw(branch);
                //Thread.Sleep(50);
            }

            

            Console.ReadKey();
        }


        static void Draw(Branch branch)
        {
            //Console.Clear();
            Console.SetCursorPosition(0, 0);
            List<StringBuilder> matrix = drawer.Draw(branch);
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] != '_'
                        && matrix[i][j] != ']'
                        && matrix[i][j] != '[')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(matrix[i][j]);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                        Console.Write(matrix[i][j]);
                }
            }
        }



    }
}

            //int y = 5;
            //var occupatedIndices = new int[] { 1, 3 };
            //var emptyIndices = Enumerable.Range(0, y).ToArray().Where(i => !occupatedIndices.Contains(i));

            //foreach (int i in emptyIndices)
            //{
            //    Console.WriteLine(value: i);
            //}

            //int curr = 0;
            //var indeces = new int[] { 10, 40, 20, 30 };
            //var chars = new char[] { 'x', 'y', 'h', 'd' };
            //Dictionary<int, char> poss = new Dictionary<int, char>();
            //foreach (int i in Enumerable.Range(0,indeces.Length))
            //{
            //    var values = Enumerable.Range(curr, indeces[i]);
            //    foreach (int v in values)
            //        poss.Add(v, chars[i]);
            //    curr += indeces[i];
            //}

            //foreach (int i in poss.Keys)
            //{ 
            //    Console.WriteLine(i + " -> " + poss[i]);
            //}

            //Console.WriteLine("0-9 [x]; 10-49 [y]; 50-69 [h]; 70-99 [d]");
            //Console.WriteLine();
            //Console.WriteLine("Тогда случайный индекс может быть :");
            //for (int k = 0; k < 20; k++)
            //{
            //    int a = random.Next(poss.Keys.Max() + 1);
            //    Console.WriteLine(a + " -> " + poss[a]);
            //}