using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTree
{
    internal class Branch
    {
        private readonly int[] randomChances = { 25, 30, 5, 45 };
        private readonly char[] randomChars = { 'x', 'y', 'h', 'd' };
        private const int xMax = 35;
        private const int yMax = 20;
        private const int dMax = 4;

        private Random random = new Random();
        private int x = 0;
        private int y = 1;
        private int h = 0;
        private int d = 0;
        private List<Branch> children;

        /// <summary>
        /// horizontal offset of branch
        /// </summary>
        public int X { get { return x; } }

        /// <summary>
        /// vertical offset of branch
        /// </summary>
        public int Y { get { return y; } }

        /// <summary>
        /// height of this branch on the parent branch
        /// </summary>
        public int H { get { return h; } }

        /// <summary>
        /// deepness of this branch in the tree
        /// </summary>
        public int D { get { return d; } }

        public List<Branch> Childrens { get { return new List<Branch>(children); } }

        public Branch()
        {
            random = new Random();
            children = new List<Branch>();
        }

        public Branch(int x, int y, int h, int d)
        { 
            random = new Random();
            children = new List<Branch>();
            this.x = x;
            this.y = y;
            this.h = h;
            this.d = d;
        }

        public void Grow()
        {
            // Нужно получить два массива: массив событий и массив правых границ интервалов вероятностей этих событий
            // после рандомить число от 0 до самой правой границы

            char? randEvent = GetRandomGrow();
            if (randEvent == null)
                return;

            switch (randEvent)
            {
                case 'x':
                    GrowX();
                    break;
                case 'y':
                    GrowY();
                    break;
                case 'h':
                    AddChild();
                    break;
                case 'd':
                    GrowChildren();
                    break;
            }


        }

        private char? GetRandomGrow()
        {
            int curr = 0;
            Dictionary<int, char> possibilities = new Dictionary<int, char>();
            if (CanGrowX())
                possibilities.Add(curr += randomChances[0], randomChars[0]);
            if (CanGrowY())
                possibilities.Add(curr += randomChances[1], randomChars[1]);
            if (CanAddChild())
                possibilities.Add(curr += randomChances[2], randomChars[2]);
            if (CanGrowChildren())
                possibilities.Add(curr += randomChances[3], randomChars[3]);

            if (curr == 0)
                return null;

            int a = random.Next(possibilities.Keys.Max() + 1);

            foreach (int k in possibilities.Keys)
            { 
                if (a < k)
                    return possibilities[k];
            }

            return null;
        }

        private bool CanGrow()
        {
            return CanGrowX() || CanGrowY() || CanAddChild() || CanGrowChildren();
        }
        //Вырасти по X
        private bool CanGrowX()
        {
            return Math.Abs(X) < xMax && d > 0;
        }
        private void GrowX()
        {
            if (X == 0)
                x += random.Next(10) >= 4 ? 1 : -1;
            else if (X > 0)
                x += 1;
            else
                x -= 1;
        }
        // Вырасти по Y
        private bool CanGrowY()
        {
            return Y < yMax;
        }
        private void GrowY()
        {
            y += 1;
        }
        // Вырастить нового потомка
        private bool CanAddChild()
        {
            if (children.Count >= Y || d >= dMax)
                return false;
            return true;
        }
        private void AddChild()
        {
            if (children.Count >= Y)
                return;

            var occupatedIndices = children.Select(c => c.H).ToArray();
            var emptyIndices = Enumerable.Range(0, y).ToArray().Where(i => !occupatedIndices.Contains(i)).ToArray();
            int index = random.Next(emptyIndices.Length);
            Branch newChild = new Branch(random.Next(10) <= 8 ? Math.Sign(X) : -Math.Sign(X), 0, emptyIndices[index], d + 1);
            newChild.GrowX();
            children.Add(newChild);
        }
        // Подрастить потомка
        private bool CanGrowChildren()
        {
            foreach (Branch b in children)
            {
                if (b.CanGrow())
                    return true;
            }
            return false;
        }
        private void GrowChildren()
        {
            if (children.Count <= 0)
                return;
            children[random.Next(children.Count)].Grow();
        }

        
    }
}
