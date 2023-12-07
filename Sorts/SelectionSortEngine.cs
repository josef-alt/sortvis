using System.Drawing;
using System.Linq;
using System.Threading;

namespace SortVisualizer
{
    class SelectionSortEngine : ISortEngine
    {
        public override void Sort(int[] array, Graphics g, int MaxEntry)
        {
            this.array = array;
            this.g = g;
            this.MaxEntry = MaxEntry;

            for(int i = 0; i < array.Count() - 1; ++i)
            {
                MarkStart(i);

                int min = i + 1;
                for (int j = i + 1; j < array.Count(); ++j)
                {
                    ev.WaitOne();
                    if (array[j] < array[min])
                    {
                        MarkNewMin(min, j);
                        min = j;
                    }
                }
                if(delay > 0)
                    Thread.Sleep(delay);
                Swap(i, min);

            }
        }

        private void MarkNewMin(int old_min, int new_min)
        {
            g.FillRectangle(WhiteBrush, old_min, MaxEntry - array[old_min], 1, MaxEntry);
            g.FillRectangle(RedBrush, new_min, MaxEntry - array[new_min], 1, MaxEntry);
        }

        private void MarkStart(int i)
        {
            g.FillRectangle(GreenBrush, i, MaxEntry - array[i], 1, MaxEntry);
        }
    }
}
