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

namespace JumpChess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            


            grid = new Grid();

            root = grid.CreateStandard(5);

            grid.CalculatePositionPoints(root, 50, Width / 2, Height / 2);

            currentPlayerID = 1;

            brushes = new Brush[] {
                new SolidBrush(Color.LightGreen),
                new SolidBrush(Color.Turquoise),
                new SolidBrush(Color.DeepPink),
                new SolidBrush(Color.Khaki),
                new SolidBrush(Color.Tan),
                new SolidBrush(Color.Plum)
            };
        }
        Position root;
        Grid grid;
        int currentPlayerID;
        Position currentSelectedPosition;
        List<Position> hintePositions;
        Brush[] brushes;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(brushes[currentPlayerID - 1], 0, 0, 30, 30);
            int[] firstWeights = { -1, 1, 2, 1, -1, -2 };
            int[] secondWeights = { -1, -1, 0, 1, 1, 0 };
            

            int horizontalLength = 25;
            int verticalLength = (int)(50 * 0.866);
            Graphics graphics = e.Graphics;
            Brush brush = new SolidBrush(Color.Black);
            Pen pen = new Pen(brush, 2);
            Pen outlinePen = new Pen(Color.Blue, 2);
            foreach(Position position in grid.Positions)
            {
                graphics.FillEllipse(brush, position.Point.X - 10, position.Point.Y - 10, 20, 20);
                for(int i = 0; i < 6; i++)
                {
                    if(position.Neighbours[i] != null)
                        graphics.DrawLine(pen, position.Point, new Point(position.Point.X + horizontalLength * firstWeights[i]
                        , position.Point.Y + verticalLength * secondWeights[i]));
                }
            }
            foreach (Position position in grid.Positions)
            {
                if (position.PieceID != 0)
                    graphics.FillEllipse(brushes[position.PieceID - 1], position.Point.X - 15, position.Point.Y - 15, 30, 30);
            }
            if (currentSelectedPosition != null)
                graphics.DrawEllipse(outlinePen, currentSelectedPosition.Point.X - 15, currentSelectedPosition.Point.Y - 15, 30, 30);
            if(hintePositions != null)
                foreach (Position position in hintePositions)
                    graphics.DrawEllipse(outlinePen, position.Point.X - 15, position.Point.Y - 15, 30, 30);
        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            foreach (Position position in grid.Positions)
                if (position.Rectangle.Contains(x, y))
                {
                    if(currentSelectedPosition != null)
                    {
                        if(position.PieceID != 0)
                        {
                            if(position.PieceID == currentPlayerID)
                            {
                                currentSelectedPosition = position;
                                hintePositions = position.GetAvailablePositions();
                                Invalidate();
                            }
                            else
                            {
                                currentSelectedPosition = null;
                                hintePositions = null;
                                Invalidate();
                            }
                        }
                        else if (hintePositions.Contains(position))
                        {
                            position.PieceID = currentSelectedPosition.PieceID;
                            currentSelectedPosition.PieceID = 0;
                            currentSelectedPosition = null;
                            hintePositions = null;
                            Invalidate();
                            currentPlayerID = currentPlayerID == 6 ? 1 : currentPlayerID + 1;
                        }
                    }
                    else
                    {
                        if (position.PieceID == currentPlayerID)
                        {
                            currentSelectedPosition = position;
                            hintePositions = position.GetAvailablePositions();
                            Invalidate();
                        }
                    }
                }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if(grid != null)
            {
                grid.CalculatePositionPoints(root, 50, Width / 2, Height / 2);
                Invalidate();
            }
        }
    }
}
