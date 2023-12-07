using System;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace SortVisualizer
{
    class BogoSortEngine : ISortEngine
    {
        private bool IsSorted = false;

        public override void Sort(int[] array, Graphics g, int MaxEntry)
        {
            this.array = array;
            this.g = g;
            this.MaxEntry = MaxEntry;

            Random random = new Random();
            while (!IsSorted)
            {
                ev.WaitOne();
                Shuffle(random);
                IsSorted = CheckIsSorted();
            }
        }

        private void Shuffle(Random random)
        {
            int n = array.Count();
            while (n-- > 1)
            {
                int k = random.Next(n + 1);
                g.FillRectangle(RedBrush, n, MaxEntry - array[n], 1, MaxEntry);
                g.FillRectangle(RedBrush, k, MaxEntry - array[k], 1, MaxEntry);

                if (delay > 0)
                    Thread.Sleep(delay);
                Swap(n, k);

                g.FillRectangle(BlackBrush, n, 0, 1, MaxEntry);
                g.FillRectangle(BlackBrush, k, 0, 1, MaxEntry);
                g.FillRectangle(WhiteBrush, n, MaxEntry - array[n], 1, MaxEntry);
                g.FillRectangle(WhiteBrush, k, MaxEntry - array[k], 1, MaxEntry);
            }
        }

        public new void Swap(int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        private bool CheckIsSorted()
        {
            for (int i = 0; i < array.Count() - 1; ++i)
                if (array[i] > array[i + 1])
                    return false;
            return true;
        }
    }
}
