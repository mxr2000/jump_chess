using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorShower
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
            this.Resize += Form1_Resize;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int width = ClientSize.Width / column;
            int height = ClientSize.Height / row;
            Font font = new Font(FontFamily.GenericMonospace, 10);
            Brush penBrush = new SolidBrush(Color.Black);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            for(int i = 0; i < row; i++)
            {
                for(int j = 0; j < column; j++)
                {
                    int index = i * column + j;
                    if (index == count)
                        break;
                    Color color = Colors[index];
                    String name = Names[index];
                    Brush brush = new SolidBrush(color);
                    Graphics graphics = e.Graphics;
                    graphics.FillRectangle(brush, j * width, i * height, width, height / 2);
                    RectangleF rectangleF = new RectangleF() { X = j * width, Y = i * height + height / 2, Width = width, Height = height / 2 };
                    graphics.DrawString(name, font, penBrush, rectangleF, stringFormat);
                }
            }
        }

        List<string> Names = new List<string>();
        List<Color> Colors = new List<Color>();
        int count;
        int column = 10;
        int row;
        private void Form1_Load(object sender, EventArgs e)
        {
            Type type = typeof(Color);
            PropertyInfo[] props = type.GetProperties();
            count = 0;
            foreach (PropertyInfo property in props)
            {
                if (property.PropertyType.Name == "Color")
                {
                    
                    Colors.Add((Color)(property.GetValue(null, null)));
                    Names.Add(property.Name);
                    count++;
                }

            }
            row = count / column + 1;
        }
    }
}
