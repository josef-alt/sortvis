using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SortVisualizer
{
    class MergeSortEngine : ISortEngine
    {
        public override void Sort(int[] array, Graphics g, int MaxEntry)
        {
            this.array = array;
            this.g = g;
            this.MaxEntry = MaxEntry;
            this.array = MergeSort(0, array.Count());
        }

        private int[] MergeSort(int low, int high)
        {
            if (low >= high)
                return new int[0];
            if (low == high - 1)
                return new int[] { array[low] };
            ev.WaitOne();

            int mid = low + (high - low) / 2;
            int[] left = MergeSort(low, mid);
            int[] right = MergeSort(mid, high);
            int lN = left.Count();
            int rN = right.Count();
            int[] result = new int[lN + rN];
            for(int l = 0, r = 0, i = 0; i < result.Count(); ++i)
            {
                if (l < lN && r < rN && left[l] < right[r] || r == rN)
                {                   
                    result[i] = left[l++];
                }
                else
                {                    
                    result[i] = right[r++];
                }
                if(delay > 0)
                    Thread.Sleep(delay);
                
                g.FillRectangle(BlackBrush, low + i, 0, 1, MaxEntry);
                g.FillRectangle(WhiteBrush, low + i, MaxEntry - result[i], 1, MaxEntry);
            }
            return result;
        }
    }
}
