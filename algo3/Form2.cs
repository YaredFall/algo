using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace algo2
{
    public partial class Form2 : Form
    {
        Form1 f1 = Form1.Instance;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            if (f1 != null)
            {
                алгоритмыToolStripMenuItem.Enabled = false;
                f1.задачаToolStripMenuItem.Enabled = false;
            }
            else
            {
                алгоритмыToolStripMenuItem.Enabled = !false;
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            f1.задачаToolStripMenuItem.Enabled = true;
        }

        private void алгоритмыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f1 = new Form1();
            f1.StartPosition = this.StartPosition;
            f1.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }



        private void выбратьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fn = openFileDialog1.FileName;
                toolStripTextBox1.Text = fn.Substring(fn.LastIndexOf('\\') + 1, fn.Length - fn.LastIndexOf('\\') - 1);
                ReadFile();
            }
            else
            {
                toolStripTextBox1.Text = "Файл не выбран";
            }
        }

        int glWidth = 20;
        int glHeight = 20;

        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            OpenGL gl = this.openGLControl1.OpenGL;

            int wScale = openGLControl1.Width / 2 / glWidth;
            int hScale = openGLControl1.Height / 2 / glHeight;

            gl.ClearColor(0.9f, 0.9f, 0.9f, 1f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Enable(OpenGL.GL_PROGRAM_POINT_SIZE);
            gl.Enable(OpenGL.GL_POINT_SMOOTH);
            gl.Enable(OpenGL.GL_BLEND);

            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Projection);
            gl.LoadIdentity();
            gl.Ortho(-glWidth, glWidth, -glHeight, glHeight, 0, 10);

            gl.Color(0.5, 0.5, 0.5);
            gl.PointSize(1);
            gl.Begin(OpenGL.GL_POINTS);

            for (int i = 0; i < glWidth; i++)
            {
                for (int j = 0; j < glHeight; j++)
                {
                    gl.Vertex(i, j);
                    gl.Vertex(-i, j);
                    gl.Vertex(i, -j);
                    gl.Vertex(-i, -j);
                }
                
            }
            gl.End();

            


            //gl.DrawText(wScale * glWidth, hScale * glHeight, 0, 0, 0, "", 12, "");
            //if (points != null)
            //    for (int i = 0; i < points.Count; i++)
            //    {
                    
            //        gl.DrawText((glWidth +points[i].X) * wScale + 15, (glHeight + points[i].Y) * hScale + 20, 0, 0, 0, "", 12, ConversString($"{i+1}"));
            //    }
        }

        private string ConversString(string s)
        {
            string res = "";
            byte[] asci = Encoding.Default.GetBytes(s);

            foreach (byte c in asci)
                res += Convert.ToChar(c+1).ToString();
            return res;
        }



        private void ReadFile()
        {
            if (File.Exists(openFileDialog1.FileName))
            {

                string[] f = File.ReadAllLines(openFileDialog1.FileName);

                foreach (var line in f)
                {
                    string[] s = line.Split(' ');

                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
