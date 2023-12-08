using System.Drawing;
using System.Linq;
using System.Threading;

namespace SortVisualizer
{
    class SlowSortEngine : ISortEngine
    {
        public override void Sort(int[] array, Graphics g, int MaxEntry)
        {
            this.array = array;
            this.g = g;
            this.MaxEntry = MaxEntry;

            SlowSort(0, array.Count() - 1);
        }

        private void SlowSort(int low, int high)
        {
            if (low >= high)
                return;

            ev.WaitOne();
            int mid = low + (high - low) / 2;
            SlowSort(low, mid);
            SlowSort(mid + 1, high);
            if (array[high] < array[mid])
            {
                if (delay > 0)
                    Thread.Sleep(delay);
                Swap(mid, high);
            }
            SlowSort(low, high - 1);
        }
    }
}
