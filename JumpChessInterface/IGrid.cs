using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpChessInterface
{
    interface IGrid
    {
        /**
         * 创建边长是size的标准棋盘
         */
        IPosition CreateStandard(int size);

        /**
         * 获取所有的IPosition
         */
        List<IPosition> Positions { get; set; }

        /**
         * 设置所有IPosition的区域信息，即Point属性，棋盘需要位于游戏区域中间
         * width: 游戏区域宽度
         * height: 游戏区域高度
         * length: 两个一条直线上的相邻Position圆心之间的距离
         */
        void SetAllPositionsPoints(int width, int height, int length);
    }
}
