using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpChessInterface
{
    interface IGame
    {
        /**
         * 绘制游戏(根据每一个Position的Point得到坐标，有棋子则用相应的画刷和棋子半径画圆，没有则用blackBrush根据positionRadius画圆)
         * graphics: 绘图上下文
         * grid: 棋盘数据
         * length: 两个一条直线上相邻的Position圆心之间的距离
         * positionRadius: 每一个位置点上没有棋子时的半径
         * pieceRadius: 每一个棋子的半径
         * linePen: 绘制线条的画笔
         * pieceBrushes: 用于绘制6种棋子的6个画刷
         * blackBrush: 黑点的画刷
         */
        void PaintGame(Graphics graphics, IGrid grid, int length, int positionRadius, int pieceRadius, Pen linePen, Brush[] pieceBrushes, Brush blackBrush);

        /**
         * 根据鼠标点击位置得到点击的Position
         * x: 点击的水平坐标
         * y: 点击的纵向坐标
         * grid: 棋盘数据
         * radius: 用于判断点击是否在这个Position的Point为圆心的圆内，radius为该圆的半径
         */
        IPosition GetSelectedPosition(int x, int y, IGrid grid, int radius);
    }
}
