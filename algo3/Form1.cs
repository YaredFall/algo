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

        

        private void button1_Click(object sender, EventArgs e)
        {
            
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
