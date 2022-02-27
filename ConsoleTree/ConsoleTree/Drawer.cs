using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTree
{
    internal class Drawer
    {
        private List<StringBuilder> matrix;

        public Drawer(int w, int h)
        {
            ClearMatrix(w, h);
        }
        public List<StringBuilder> Draw(Branch b)
        {
            if (matrix.Count <= 0 || matrix[0].Length <= 0)
                return matrix;

            ClearMatrix(matrix[0].Length, matrix.Count);
            DrawBranch(b, matrix[0].Length / 2, matrix.Count - 1);

            return matrix;
        }
        private void DrawBranch(Branch b, int x, int y)
        {
            if (y >= 0 && y < matrix.Count && x >= 0 && x < matrix[0].Length)
                matrix[y][x] = '░';
            
            if (b.D == 0)
                y -= 1;
            else
                x += b.X>=0 ? 1 : -1;

            if (y >= 0 && y < matrix.Count && x >= 0 && x < matrix[0].Length)
                matrix[y][x] = b.X >= 0 ? '>' : '<';

            int x2 = x;
            for (int i = 0; i < Math.Abs(b.X); i++)
            {
                x2 += Math.Sign(b.X) * 1;
                if (y >= 0 && y < matrix.Count && x >= 0 && x < matrix[0].Length)
                    matrix[y][x2] = '─';
            }
            matrix[y][x2] = '?';//'┼';
            //int y2 = y;
            for (int i = 1; i < b.Y; i++)
            {
                //y2 += 1;
                if (y >= 0 && y < matrix.Count && x >= 0 && x < matrix[0].Length)
                    matrix[y-i][x2] = '│';
            }
            if (y >= 0 && y < matrix.Count && x >= 0 && x < matrix[0].Length)
                matrix[y - b.Y][x2] = '*';

            foreach (Branch b2 in b.Childrens)
            {
                DrawBranch(b2, x2, y - b2.H);
            }
            
        }

        private void ClearMatrix(int w, int h)
        {
            matrix = new List<StringBuilder>(h);
            for (int i = 0; i < h; i++)
            {
                StringBuilder s = new StringBuilder("[" + new String('_', w - 2) + "]");
                matrix.Add(s);
                //matrix[i].PadRight(w - 1, '_');
            }
        }
    }
}
