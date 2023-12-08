using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SortVisualizer
{
    public partial class MainForm : Form
    {
        private int[] array;
        private int delay;
        private Graphics g;
        private Thread worker;
        private ISortEngine engine;

        public MainForm()
        {
            InitializeComponent();
            algorithmComboBox.SelectedIndex = 0;
            SetSort();
        }

        private void SetSort()
        {
            engine = SortFactory.LoadSort(algorithmComboBox.SelectedIndex);
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
            if (worker != null && worker.IsAlive)
                worker.Abort();
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
