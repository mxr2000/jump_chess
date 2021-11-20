using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpChess
{
    class Grid
    {
        public List<Position> Positions { get; set; } = new List<Position>();
        public Position CreateInverseTriangle(int size, int id = 0)
        {
            Position root = new Position(id);
            Positions.Add(root);
            Position lastOneInLastLine = null;
            Position nextOneInLastLine = null;
            Position saved = root;
            for (int i = 1; i < size; i++)
            {
                Position lastOneInNewLine = null;
                lastOneInLastLine = null;
                nextOneInLastLine = saved;
                for(int j = 0; j < i + 1; j++)
                {
                    Position position = new Position(id);
                    Positions.Add(position);
                    if (j == 0)
                        saved = position;
                    ConnectTwoPositions(lastOneInNewLine, position, 4);
                    ConnectTwoPositions(lastOneInLastLine, position, 3);
                    ConnectTwoPositions(nextOneInLastLine, position, 2);
                    lastOneInLastLine = nextOneInLastLine;
                    lastOneInNewLine = position;
                    if(nextOneInLastLine != null)
                        nextOneInLastLine = nextOneInLastLine.Neighbours[4];
                }
            }
            return root;
        }

        public Position CreateTriangle(int size, int id = 0)
        {
            Position root = new Position(id);
            Positions.Add(root);
            Position lastOneInLastLine = null;
            Position nextOneInLastLine = null;
            Position saved = root;
            for (int i = 1; i < size; i++)
            {
                Position lastOneInNewLine = null;
                lastOneInLastLine = null;
                nextOneInLastLine = saved;
                if(i == 2)
                    Console.WriteLine("Dick");
                for (int j = 0; j < i + 1; j++)
                {
                    Position position = new Position(id);
                    Positions.Add(position);
                    if (j == 0)
                        saved = position;
                    ConnectTwoPositions(lastOneInNewLine, position, 3);
                    ConnectTwoPositions(lastOneInLastLine, position, 2);
                    ConnectTwoPositions(nextOneInLastLine, position, 1);
                    lastOneInLastLine = nextOneInLastLine;
                    lastOneInNewLine = position;
                    if (nextOneInLastLine != null)
                        nextOneInLastLine = nextOneInLastLine.Neighbours[3];
                }
            }
            return root;
        }

        public void CreateHexagon(int size, out Position p0, out Position p1, out Position p2, out Position p3, out Position p4, out Position p5, out Position pCenter)
        {
            p0 = p1 = p2 = p3 = p4 = p5 = pCenter = null;
            Position forwardInLastLine = null;
            Position backwardInLastLine = null;
            Position last = null;
            Position saved = null;
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size + i; j++)
                {
                    Position position = new Position();
                    if (i == 0 && j == 0)
                        p0 = position;
                    else if (i == 0 && j == size - 1)
                        p1 = position;
                    else if (i == size - 1 && j == 0)
                        p5 = position;
                    else if (i == size - 1 && j == 2 * size - 2)
                        p2 = position;
                    else if (i == size - 1 && j == size - 1)
                        pCenter = position;
                    Positions.Add(position);
                    if (j == 0)
                        saved = position;
                    ConnectTwoPositions(last, position, 2);
                    ConnectTwoPositions(backwardInLastLine, position, 3);
                    ConnectTwoPositions(forwardInLastLine, position, 4);
                    last = position;
                    backwardInLastLine = forwardInLastLine;
                    if (forwardInLastLine != null)
                        forwardInLastLine = forwardInLastLine.Neighbours[2];
                }
                last = null;
                forwardInLastLine = saved;
            }
            backwardInLastLine = saved;
            forwardInLastLine = saved.Neighbours[2];
            for(int i = 0; i < size - 1; i++)
            {
                for(int j = 0; j < 2 * size - i - 2; j++)
                {
                    Position position = new Position();
                    Positions.Add(position);
                    if (i == size - 2 && j == 0)
                        p4 = position;
                    else if (i == size - 2 && j == size - 1)
                        p3 = position;

                    if (j == 0)
                        saved = position;
                    ConnectTwoPositions(last, position, 2);
                    ConnectTwoPositions(backwardInLastLine, position, 3);
                    ConnectTwoPositions(forwardInLastLine, position, 4);
                    last = position;
                    backwardInLastLine = forwardInLastLine;
                    if (forwardInLastLine != null)
                        forwardInLastLine = forwardInLastLine.Neighbours[2];
                }
                backwardInLastLine = saved;
                forwardInLastLine = backwardInLastLine.Neighbours[2];
                last = null;
            }
        }
        

        public void ConnectTwoPositions(Position p1, Position p2, int direction)
        {
            if(p1 != null)
                p1.Neighbours[direction] = p2;
            if(p2 != null)
                p2.Neighbours[(3 + direction) % 6] = p1;
        }

        public void CalculatePositionPoints(Position root, int length, int x, int y)
        {
            int horizontalLength = length / 2;
            int verticalLength = (int)(length * 0.866);
            int[] firstWeights = { -1, 1, 2, 1, -1, -2 };
            int[] secondWeights = { -1, -1, 0, 1, 1, 0 };
            List<Position> visitedPositions = new List<Position>();
            Queue<Position> currentPositions = new Queue<Position>();
            currentPositions.Enqueue(root);
            root.Point = new System.Drawing.Point(x, y);
            while(currentPositions.Count > 0)
            {
                Position current = currentPositions.Dequeue();
                visitedPositions.Add(current);
                for(int i = 0; i < 6; i++){
                    Position position = current.Neighbours[i];
                    if (position != null && !visitedPositions.Contains(position))
                    {
                        position.Point = new System.Drawing.Point(current.Point.X + firstWeights[i] * horizontalLength, 
                            current.Point.Y + secondWeights[i] * verticalLength);
                        currentPositions.Enqueue(position);
                    }
                }
            }
        }

        private void GlueTwoLines(Position p0, Position p1, int size, int direction)
        {
            for(int i = 0; i < size; i++)
            {
                ConnectTwoPositions(p1, p0, (direction + 2) % 6);
                ConnectTwoPositions(p1, p0.Neighbours[direction], (direction + 1) % 6);
                p0 = p0.Neighbours[direction];
                p1 = p1.Neighbours[direction];
            }
        }

        private Position GetUpCorner(Position root)
        {
            while (root.Neighbours[1] != null)
                root = root.Neighbours[1];
            return root;
        }
        private Position GetRightCorner(Position root)
        {
            while (root.Neighbours[2] != null)
                root = root.Neighbours[2];
            return root;
        }
        private Position GetBottomCorner(Position root)
        {
            while (root.Neighbours[3] != null)
                root = root.Neighbours[3];
            return root;
        }
        public Position CreateStandard(int size)
        {
            Position p0, p1, p2, p3, p4, p5, pCenter;
            CreateHexagon(size, out p0, out p1, out p2, out p3, out p4, out p5, out pCenter);

            Position tri0 = CreateTriangle(size - 1, 1);
            GlueTwoLines(p0, tri0, size - 1, 2);

            Position tri1 = CreateInverseTriangle(size - 1, 2);
            GlueTwoLines(p1, tri1, size - 1, 3);

            Position tri2 = CreateTriangle(size - 1, 3);
            GlueTwoLines(p2, GetUpCorner(tri2), size - 1, 4);

            Position tri3 = CreateInverseTriangle(size - 1, 4);
            GlueTwoLines(p3, GetRightCorner(tri3), size - 1, 5);

            Position tri4 = CreateTriangle(size - 1, 5);
            GlueTwoLines(p4, GetRightCorner(tri4), size - 1, 0);

            Position tri5 = CreateInverseTriangle(size - 1, 6);
            GlueTwoLines(p5, GetBottomCorner(tri5), size - 1, 1);

            return pCenter;
        }
    }
}
