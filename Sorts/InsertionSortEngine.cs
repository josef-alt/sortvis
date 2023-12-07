using System.Drawing;
using System.Linq;
using System.Threading;

namespace SortVisualizer
{
    class InsertionSortEngine : ISortEngine
    {
        public override void Sort(int[] array, Graphics g, int MaxEntry)
        {
            this.array = array;
            this.g = g;
            this.MaxEntry = MaxEntry;

            for (int i = 0; i < array.Count() - 1; ++i)
            {
                for(int j = i + 1; j > 0 && array[j] < array[j - 1]; --j)
                {
                    ev.WaitOne();
                    g.FillRectangle(GreenBrush, j, MaxEntry - array[j], 1, MaxEntry);
                    if (delay > 0)
                        Thread.Sleep(delay);
                    Swap(j, j - 1);
                }
            }
        }
    }
}
