using System.Drawing;
using System.Threading;

namespace SortVisualizer
{
    abstract class ISortEngine
    {
        protected int delay = 0;
        protected int MaxEntry;
        protected int[] array;

        protected Graphics g;
        protected ManualResetEvent ev = new ManualResetEvent(true);

        protected Brush RedBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
        protected Brush GreenBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
        protected Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        protected Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        public void Toggle(bool flag)
        {
            if (flag)
                ev.Reset();
            else
                ev.Set();
        }
        public void SetDelay(int ms)
        {
            delay = ms;
        }

        protected void Swap(int i, int j)
        {
            g.FillRectangle(BlackBrush, i, 0, 1, MaxEntry);
            g.FillRectangle(BlackBrush, j, 0, 1, MaxEntry);

            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;

            g.FillRectangle(WhiteBrush, i, MaxEntry - array[i], 1, MaxEntry);
            g.FillRectangle(WhiteBrush, j, MaxEntry - array[j], 1, MaxEntry);
        }

        public abstract void Sort(int[] array, System.Drawing.Graphics g, int MaxEntry);
    }
}
