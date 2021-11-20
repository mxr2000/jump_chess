using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpChessInterface
{
    interface IPosition
    {
        /**
         * 获取6个方向上的6个邻居
         */
        IPosition[] Neighbours { get; set; }

        /**
         * 获取当前位置所能到达的IPosition
         */
        List<IPosition> GetAvailablePositions();

        /**
         * 获取这个位置的在Grid的Position列表中的索引
         */
        int ID { get; set; }

        /**
         * 获取这个位置上的棋子id, 1-6，0表示没有棋子
         */
        int PieceID { get; set; }

        /**
         * 获取这个区域的圆心坐标
         */
        Point Point { get; set; }
    }
}
