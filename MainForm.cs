using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortVisualizer
{
    public partial class MainForm : Form
    {
        int[] array;
        int delay;
        Graphics g;
        Thread worker;
        ISortEngine engine;

        public MainForm()
        {
            InitializeComponent();
            algorithmComboBox.SelectedIndex = 0;
            SetSort();
        }

        private void SetSort()
        {
            switch(algorithmComboBox.SelectedIndex)
            {
                case 0:
                    engine = new BubbleSortEngine();
                    break;
                case 1:
                    engine = new SelectionSortEngine();
                    break;
                case 2:
                    engine = new InsertionSortEngine();
                    break;
                case 3:
                    engine = new RadixSortEngine();
                    break;
                case 4:
                    engine = new BogoSortEngine();
                    break;
                case 5:
                    engine = new SlowSortEngine();
                    break;
                case 6:
                    engine = new DropSortEngine();
                    break;
            }
            engine.SetDelay(delay);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (worker != null)
                worker.Abort();
            base.OnClosing(e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
            this.Close();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            if(worker != null)
            Console.WriteLine(worker.IsAlive);
            if (worker != null && worker.IsAlive)
            {
                worker.Abort();
                Console.WriteLine("aborting");
            }
            sortButton.Text = "Sort";

            SetSort();
            g = visualizationPanel.CreateGraphics();
            int NumEntries = visualizationPanel.Width;
            int MaxEntry = visualizationPanel.Height;
            array = new int[NumEntries];
            g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), 0, 0, NumEntries, MaxEntry);
            Random random = new Random();
            for (int i = 0; i < NumEntries; ++i)
                array[i] = random.Next(0, MaxEntry);

            for (int i = 0; i < NumEntries; ++i)
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, MaxEntry - array[i], 1, MaxEntry);
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Clicked", sortButton.Text);
            switch(sortButton.Text)
            {
                case "Sort":
                    if (worker != null && worker.ThreadState == ThreadState.Running)
                        worker.Abort();
                    if (array == null)
                        return;

                    sortButton.Text = "Pause";
                    worker = new Thread(() => engine.Sort(array, g, visualizationPanel.Height));
                    worker.IsBackground = true;
                    worker.Start();
                    break;
                case "Pause":
                    if (worker == null || !worker.IsAlive)
                        return;

                    engine.Toggle(true);
                    sortButton.Text = "Resume";
                    break;
                case "Resume":
                    if (worker == null)
                        return;

                    engine.Toggle(false);
                    sortButton.Text = "Pause";
                    break;
            }
            
        }

        private void algorithmComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Selected", algorithmComboBox.SelectedIndex);
            SetSort();
        }

        private void delayTextBox_TextChanged(object sender, EventArgs e)
        {
            int delayValue;
            if(delayTextBox.Text != null && Int32.TryParse(delayTextBox.Text, out delayValue))
            {
                delay = delayValue;
                engine.SetDelay(delayValue);
            }
            else
            {
                delayTextBox.Text = "";
            }
        }
    }
}
