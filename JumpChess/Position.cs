using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpChess
{
    class Position
    {
        public Position(int pieceID = 0, int size = 10)
        {
            ID = currentID;
            currentID++;
            Size = size;
            this.PieceID = pieceID;
        }
        public int ID { get; set; }
        private static int currentID = 0;
        public int Size { get; set; }
        private Point _point;
        public Point Point
        {
            get
            {
                return _point;
            }
            set
            {
                _point = value;
                Rectangle = new Rectangle(_point.X - Size, _point.Y - Size, 2 * Size, 2 * Size);
            }
        }
        public Rectangle Rectangle { get; set; }
        public Position[] Neighbours { get; set; } = new Position[6];
        public int PieceID { get; set; }
        public List<Position> GetAvailablePositions()
        {
            List<Position> positions = new List<Position>();
            AddAvailablePositionsToList(positions);
            foreach (Position neighbour in Neighbours)
                if (neighbour != null && !positions.Contains(neighbour) && neighbour.PieceID == 0)
                    positions.Add(neighbour);
            return positions;
        }
        public void AddAvailablePositionsToList(List<Position> visitedPositions)
        {
            for(int i = 0; i < 6; i++)
            {
                int count = 0;
                Position obstacle = null;
                if((obstacle = GetNearestObstable(i, out count)) != null)
                {
                    Position destination = obstacle.GetJumpDestination(i, count);
                    if(destination != null && !visitedPositions.Contains(destination))
                    {
                        visitedPositions.Add(destination);
                        destination.AddAvailablePositionsToList(visitedPositions);
                    }
                }
            }
        }

        public Position GetNearestObstable(int direction, out int count)
        {
            Position current = Neighbours[direction];
            count = 1;
            while(current != null && current.PieceID == 0)
            {
                count++;
                current = current.Neighbours[direction];
                if(current == null)
                {
                    return null;
                }
            }
            return current;
        }
        public Position GetJumpDestination(int direction, int count)
        {
            Position current = this;
            while(count > 0)
            {
                current = current.Neighbours[direction];
                if(current == null || current.PieceID != 0)
                {
                    return null;
                }
                count--;
            }
            return current.PieceID == 0 ? current : null;
        }
    }
}
