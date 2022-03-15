using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AlgoritmienVisualisointi
{
    public partial class Form1 : Form
    {
        int[] taulu;
        Graphics grafiikka;
        BackgroundWorker b = null;
        bool paussi = false;

        public Form1()
        {
            InitializeComponent();
            TaydennaDroppi();
        }

        private void TaydennaDroppi()
        {
            List<string> luokat = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(ISort).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.Name).ToList();
            luokat.Sort();
            foreach(string a in luokat)
            {
                comboBox1.Items.Add(a);
            }
            comboBox1.SelectedIndex = 0;

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSort_Click(object sender, EventArgs e)
        {
            b = new BackgroundWorker();
            b.WorkerSupportsCancellation = true;
            b.DoWork += new DoWorkEventHandler(b_doWork);
            b.RunWorkerAsync(argument: comboBox1.SelectedItem);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!paussi)
            {
                b.CancelAsync();
                paussi = true;
            }
            else
            {
                int leveys = panel1.Width;
                int korkeus = panel1.Height;
                paussi = false;
                for(int i = 0; i < korkeus; i++)
                {
                    grafiikka.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), i, 0, 1, korkeus);
                    grafiikka.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), i, korkeus - taulu[i], 1, korkeus);
                }
                b.RunWorkerAsync(argument: comboBox1.SelectedItem);
            }
        }


        private void btnShuffle_Click(object sender, EventArgs e)
        {
            grafiikka = panel1.CreateGraphics();
            int leveys = panel1.Width;
            int korkeus = panel1.Height;
            taulu = new int[leveys];
            grafiikka.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Red), 0, 0, leveys, korkeus);
            Random r = new Random();

            for(int i = 0; i < leveys; i++)
            {
                taulu[i] = r.Next(0, korkeus);
            }

            for (int i = 0; i < leveys; i++)
            {
                grafiikka.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), i, korkeus - taulu[i], 1, korkeus);
            }

        }

        public void b_doWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            string sortName = (string)e.Argument;
            Type type = Type.GetType("AlgoritmienVisualisointi." + sortName);
            var ctors = type.GetConstructors();
            try
            {
                ISort se = (ISort)ctors[0].Invoke(new object[] { taulu, grafiikka, panel1.Height });
                while(!se.onJarjestetty() && (!b.CancellationPending))
                {
                    se.Seuraava();
                }
            }
            catch(Exception ex)
            {

            }
        }

        
    }
}
