using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MatchmanRUN
{
    public class Matchman
    {
        private short width;   //主角规格
        private short height;
        private short top;
        private short left;
        private int ID;
        public int[,] shape;
        public Matchman()
        {
            this.ID = 1;
            switch (this.ID)
            {
                case 1:
                    this.Width = 2;
                    this.Height = 4;
                    this.Left = 3;
                    this.top = 25;
                    shape = new int[this.Width, this.Height];
                    shape[0, 0] = 1; shape[0, 1] = 1;
                    shape[1, 0] = 1; shape[1, 1] = 1;
                    shape[0, 2] = 1; shape[0, 3] = 1;
                    shape[1, 2] = 1; shape[1, 3] = 1;
                    break;

            }
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
        public short Left//Left属性
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

        public void Draw(Graphics g)
        {
            Image brickImage = Image.FromFile("image/block0.gif");
           
            for (int i = 0; i < this.Width; i++)
            {
                for (int j = 0; j < this.Height; j++)
                {
                    if (this.shape[i, j] == 1)
                    {
                        //得到绘制这个格子的在游戏面板中的矩形区域
                        Rectangle rect = new Rectangle((this.Left + i) * Game.BlockImageWidth, (this.Top + j) * Game.BlockImageHeight, Game.BlockImageWidth, Game.BlockImageHeight);
                        g.DrawImage(brickImage, rect);
                    }
                }
            }
        }
    

    }
}
