using System.Drawing;
using System.Linq;
using System.Threading;

namespace SortVisualizer
{
    class QuickSortEngine : ISortEngine
    {
        public override void Sort(int[] array, Graphics g, int MaxEntry)
        {
            this.array = array;
            this.g = g;
            this.MaxEntry = MaxEntry;
            QuickSort(0, array.Count() - 1);
        }

        private void QuickSort(int low, int high)
        {
            if (low >= high)
                return;

            ev.WaitOne();
            int pivot = Partition(low, high);
            QuickSort(low, pivot - 1);
            QuickSort(pivot + 1, high);
        }

        // uses last element as pivot
        private int Partition(int low, int high)
        {
            int pivot = array[high];
            g.FillRectangle(GreenBrush, high, MaxEntry - pivot, 1, MaxEntry);

            int i = low - 1;
            for (int j = low; j < high; ++j)
            {
                if (array[j] < pivot)
                {
                    g.FillRectangle(RedBrush, i + 1, MaxEntry - array[i + 1], 1, MaxEntry);
                    g.FillRectangle(RedBrush, j, MaxEntry - array[j], 1, MaxEntry);
                    if (delay > 0)
                        Thread.Sleep(delay);
                    Swap(++i, j);
                }
            }

            g.FillRectangle(WhiteBrush, high, MaxEntry - pivot, 1, MaxEntry);

            Swap(++i, high);
            return i;
        }
    }
}
