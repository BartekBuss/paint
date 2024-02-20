using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paintt
{
    public partial class Form1 : Form
    {
        Bitmap bitmapa;
        Graphics grafika;
        bool rysowanie = false;
        Point px, py;
        Pen pen = new Pen(Color.Black, 2);
        int wybor;
        int x, y, x1, y1, x2, y2;
        int width = 694;
        int height = 422;
        SolidBrush pedzel = new SolidBrush(Color.Black);
        Font czcionka = new Font("Arial", 12);

        public Form1()
        {
            InitializeComponent();
            bitmapa = new Bitmap(width, height);
            grafika = Graphics.FromImage(bitmapa);
            grafika.Clear(Color.White);
            pictureBox1.Image = bitmapa;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            wybor = 5;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics grafika = e.Graphics;
            if (rysowanie)
            {
                if (wybor == 2)
                {
                    grafika.DrawEllipse(pen, x2, y2, x1, y1);
                }
                if (wybor == 3)
                {
                    grafika.DrawRectangle(pen, x2, y2, x1, y1);
                }
                if (wybor == 4)
                {
                    grafika.DrawLine(pen, x2, y2, x, y);
                }
                if (wybor == 5)
                {
                    string napis = textBox1.Text;
                    grafika.DrawString(napis, czcionka, pedzel, x1, y1);
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            rysowanie = true;
            py = e.Location;

            x2 = e.X;
            y2 = e.Y;
            if (wybor == 5)
            {
                string napis = textBox1.Text;
                grafika.DrawString(napis, czcionka, pedzel, x2, y2);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (rysowanie)
            {
                if (wybor == 1)
                {
                    px = e.Location;
                    grafika.DrawLine(pen, px, py);
                    py = px;
                }
            }
            pictureBox1.Refresh();

            x = e.X;
            y = e.Y;
            x1 = e.X - x2;
            y1 = e.Y - y2;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            rysowanie = false;

            x1 = x - x2;
            y1 = y - y2;
            if (wybor == 2)
            {
                grafika.DrawEllipse(pen, x2, y2, x1, y1);
            }
            if (wybor == 3)
            {
                grafika.DrawRectangle(pen, x2, y2, x1, y1);
            }
            if (wybor == 4)
            {
                grafika.DrawLine(pen, x2, y2, x, y);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wybor = 4;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            wybor = 3;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            wybor = 2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            pen.Color = colorDialog1.Color;
            pedzel.Color = colorDialog1.Color;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            Image otw = Image.FromFile(openFileDialog1.FileName);
            grafika.DrawImage(otw, 20.0F, 20.0F);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var zapis = new SaveFileDialog();
            zapis.Filter = "Image(*.jpg)|*.jpg|(*.*)|*.*";
            if (zapis.ShowDialog() == DialogResult.OK)
            {
                Bitmap bm = bitmapa.Clone(new Rectangle(0, 0, width, height), bitmapa.PixelFormat);
                bm.Save(zapis.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}
