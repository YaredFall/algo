using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace algo2
{
    public partial class Form1 : Form
    {
        public static Form1 Instance = null; 

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                this.Close();
            }
            OpenSecondForm();

        }

        private void FillTable(int vn, int vk, int pn)
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            
            

            VectorSequence vs = new VectorSequence(vn, vk);
            time.Stop();
            time31label.Text = $"Время: {time.Elapsed}";

            time.Restart();
            Permutations ps = new Permutations(pn);
            time.Stop();
            time32label.Text = $"Время: {time.Elapsed}";

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("number", "Номер");
            dataGridView1.Columns.Add("vectors", $"Векторы ({vn}, {vk})");
            dataGridView1.Columns.Add("permutations", $"Перестановки ({pn})");

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 200;

            int n = vs.Length;
            int m = ps.Length;

            int N = n > m ? n : m;
            for (int i = 0; i < N; i++)
            {
                dataGridView1.Rows.Add(new object[] { i + 1, i < n ? vs.StringVector(i) : "-", i < m ? ps.StringPermutation(i) : "-" });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillTable(int.Parse(vnBox.Text), int.Parse(vkBox.Text), int.Parse(pnBox.Text));
        }

        void OpenSecondForm()
        {
            Form form2 = new Form2();
            form2.StartPosition = FormStartPosition.Manual;
            form2.Left = this.Location.X + this.Width;
            form2.Top = this.Location.Y;


            form2.Show();
        }

        private void задачаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSecondForm();
        }
    }
}
