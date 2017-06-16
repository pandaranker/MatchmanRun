using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace MatchmanRUN
{
    class Block
    {
        private short width;   //障碍物规格
        private short height;
        private short top;
        private int oldleft;
        private int left;
        private int move=0;
        private int ID = 1;    //障碍物号
        public int[,] shape;
        public Block(int I)
        {
            this.ID = I;
            switch(ID)
            {
                case 1:
                    this.width = 6;
                    this.height = 4;
                    this.top = 25 ;
                    this.left = 75;
                    shape = new int[this.width, this.height];
                    for (int i = 0; i < width; i++)
                        for (int j = 0; j < height; j++)
                            shape[i, j] = 1;
                    /*shape[0, 0] = 1; shape[0, 1] = 1; shape[0, 2] = 1; shape[0, 3] = 1;
                    shape[1, 0] = 1; shape[1, 1] = 1; shape[1, 2] = 1; shape[1, 3] = 1;
                    shape[2, 0] = 1; shape[2, 1] = 1; shape[2, 2] = 1; shape[2, 3] = 1;
                    shape[3, 0] = 1; shape[3, 1] = 1; shape[3, 2] = 1; shape[3, 3] = 1;               //备用 改变形状
                    shape[4, 0] = 1; shape[4, 1] = 1; shape[4, 2] = 1; shape[4, 3] = 1;
                    shape[5, 0] = 1; shape[5, 1] = 1; shape[5, 2] = 1; shape[5, 3] = 1;*/
                    break;
                case 2:
                    this.width = 2;
                    this.height = 4;
                    this.top = 21;
                    this.left = 75;
                    shape = new int[this.width, this.height];
                    for (int i = 0; i < width; i++)
                        for (int j = 0; j < height; j++)
                            shape[i, j] = 1;
                    /*shape[0, 0] = 1; shape[0, 1] = 1; shape[0, 2] = 1; shape[0, 3] = 1;
                    shape[1, 0] = 1; shape[1, 1] = 1; shape[1, 2] = 1; shape[1, 3] = 1;*/
                    break;
                case 3:
                    this.width = 8;
                    this.height = 1;
                    this.top = 19;
                    this.left = 75;
                    shape = new int[this.width, this.height];
                    for (int i = 0; i < width; i++)
                        for (int j = 0; j < height; j++)
                            shape[i, j] = 1;
                    break;
                case 4:
                    this.width = 2;
                    this.height = 12;
                    this.top = 17;
                    this.left = 75;
                    shape = new int[this.width, this.height];
                    for (int i = 0; i < width; i++)
                        for (int j = 0; j < height; j++)
                            shape[i, j] = 1;
                    break;

            }
            oldleft = left;
        }
        public short Width//Width属性
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }
        public short Height //Height属性
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }
        public short Top//Top属性
        {
            get
            {
                return top;
            }
            set
            {
                top = value;
            }
        }
        public int Left//Left属性
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }
        public bool ShouldDraw()   //是否处于可画范围
        {
            if (this.left < 75 && this.left + this.width > 0)
                return true;
            else
                return false;
        }

        /*public bool Delete()
        {
            if (this.left + this.width < 0)              删除对象的留用（弃用）方案：先删除障碍物再删除图。
                return true;
            else
                return false;
        }*/
        public void Move()
        {
            move+=12;
            if (move >= 12)
            {
                left--;
                move-=12;
            }
        }
        public bool reallyMove()    //地图移动判定
        {
            if (left != oldleft)
            {
                oldleft = left;
                return true;
            }
            else
                return false;
        }
        public bool underBlock(int topp,int topn,int leftt)
        {
            if (topp <= top*Game.BlockImageHeight && topn > top*Game.BlockImageHeight && leftt>left*Game.BlockImageWidth && leftt-4*Game.BlockImageWidth<(left+width)*Game.BlockImageWidth)
                return true;
            else
                return false;
        }
        public bool underAir(int topp, int topn, int leftt)
        {
            if (topp == top * Game.BlockImageHeight && (leftt-4*Game.BlockImageWidth) < (left + width) * Game.BlockImageWidth && leftt > left * Game.BlockImageWidth  )
                return true;
            else
                return false;
        }
        public void addWaitTime(int time)  //给障碍物增加间隔（改变left属性）
        {
            this.left = left + time;
        }
        public bool gameOver(int LEFT, int TOP)            //最基础的碰撞死亡
        {
            if (LEFT > left * Game.BlockImageWidth - move && (top + height) * Game.BlockImageHeight <= (TOP - 4 * Game.BlockImageHeight))  //   头在障碍物底之上
                return false;
            else if (LEFT > left * Game.BlockImageWidth - move && TOP <= top * Game.BlockImageHeight && LEFT < (left + width) * Game.BlockImageWidth)
                return false;
            else if (LEFT > left * Game.BlockImageWidth - move && TOP > top * Game.BlockImageHeight && LEFT < (left + width) * Game.BlockImageWidth)    // 人右下 与 障碍物左上对比
              return true; 
            else if (LEFT > left * Game.BlockImageWidth - move && TOP > (top + height) * Game.BlockImageHeight && (left + width) * Game.BlockImageWidth - move > (LEFT - 4 * Game.BlockImageWidth) && (top + height) * Game.BlockImageHeight > (TOP - 4 * Game.BlockImageHeight))
              return true;//人左上 与 障碍物右下对比
            else
                return false;
            
        }
        public void Draw(Graphics g)
        {
            //MessageBox.Show("  1  ");
            Image brickImage = mman.block;
            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    if (this.shape[i, j] == 1)//黑色格子
                    {
                        //得到绘制这个格子的在游戏面板中的矩形区域
                        Rectangle rect = new Rectangle((this.left + i) * Game.BlockImageWidth-move, (this.top + j) * Game.BlockImageHeight, Game.BlockImageWidth, Game.BlockImageHeight);
                        g.DrawImage(brickImage, rect);
                    }
                }
            }
        }
    }
}
