using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace SortVisualizer
{
    class RadixSortEngine : ISortEngine
    {
        public override void Sort(int[] array, Graphics g, int MaxEntry)
        {
            int max_radix = (int)Math.Log10(MaxEntry) + 1;
            for(int radix = 0; radix < max_radix; ++radix)
            {
                ev.WaitOne();
                List<List<int>> buckets = new List<List<int>>(10);
                for (int m = 0; m < 10; ++m)
                    buckets.Add(new List<int>());
                foreach(int number in array)
                    buckets[(int)(number / Math.Pow(10, radix)) % 10].Add(number);

                int index = 0;
                foreach (List<int> bucket in buckets)
                {
                    foreach (int number in bucket)
                    {
                        array[index] = number;
                        g.FillRectangle(BlackBrush, index, 0, 1, MaxEntry);
                        g.FillRectangle(WhiteBrush, index, MaxEntry - array[index], 1, MaxEntry);
                        index++;
                    }
                    if (delay > 0)
                        Thread.Sleep(delay);
                }
            }
        }
    }
}
