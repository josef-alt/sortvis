using System.Drawing;
using System.Linq;
using System.Threading;

namespace SortVisualizer
{
    class DropSortEngine : ISortEngine
    {
        public override void Sort(int[] array, Graphics g, int MaxEntry)
        {
            this.array = array;
            this.g = g;
            this.MaxEntry = MaxEntry;
            int end = array.Count() - 1;
            for(int i = 1; i < end; ++i)
            {
                ev.WaitOne();
                g.FillRectangle(GreenBrush, i, MaxEntry - array[i], 1, MaxEntry);
                if (delay > 0)
                    Thread.Sleep(delay);
                if (array[i] < array[i - 1])
                    ShiftLeft(i--, end--);
                else
                    g.FillRectangle(WhiteBrush, i, MaxEntry - array[i], 1, MaxEntry);
            }
        }

        private void ShiftLeft(int start, int end)
        {
            while (start < end - 1)
            {
                array[start] = array[start + 1];
                g.FillRectangle(BlackBrush, start, 0, 1, MaxEntry);
                g.FillRectangle(WhiteBrush, start, MaxEntry - array[start], 1, MaxEntry);
                g.FillRectangle(BlackBrush, start + 1, 0, 1, MaxEntry);

                start++;
            }
            g.FillRectangle(BlackBrush, end, 0, 1, MaxEntry);
        }
    }
}
