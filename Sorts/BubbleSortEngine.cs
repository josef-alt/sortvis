using System.Drawing;
using System.Linq;
using System.Threading;

namespace SortVisualizer
{
    class BubbleSortEngine : ISortEngine
    {
        private bool IsSorted = false;

        public override void Sort(int[] array, Graphics g, int MaxEntry)
        {
            this.array = array;
            this.g = g;
            this.MaxEntry = MaxEntry;

            int stop = array.Count() - 1;
            while (!IsSorted)
            {
                ev.WaitOne();
                for (int i = 0; i < stop; ++i)
                {
                    if (array[i] > array[i + 1])
                        Swap(i, i + 1);
                    if (delay > 0)
                        Thread.Sleep(delay);
                }
                stop--;
                IsSorted = CheckIsSorted();
            }
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
