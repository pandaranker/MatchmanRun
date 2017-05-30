using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
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
        private int fpi=0;   //帧
        public int[,] shape;
        Bitmap brickImage = mman.ManRun;
        private EventHandler evtHandler = null;
        public Matchman()
        {
            this.ID = 1;
            switch (this.ID)
            {
                case 1:
                    this.Width = 3;
                    this.Height = 4;
                    this.Left = 3;
                    this.top = 25;
                    shape = new int[this.Width, this.Height];
                    shape[0, 0] = 1; shape[0, 1] = 1; shape[0, 2] = 1; shape[0, 3] = 1;
                    shape[1, 0] = 1; shape[1, 1] = 1; shape[1, 2] = 1; shape[1, 3] = 1;
                    shape[2, 0] = 1; shape[2, 1] = 1; shape[2, 2] = 1; shape[2, 3] = 1;



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
        public void Draw(PaintEventArgs e)
        {
            
            Image m_Image = mman.ManRun;
            FrameDimension frameDim = new FrameDimension(m_Image.FrameDimensionsList[0]);
            int count = m_Image.GetFrameCount(frameDim);
            if (fpi <= count-2) fpi=fpi+1;
            else fpi = 0;
            m_Image.SelectActiveFrame(frameDim, fpi);
            //得到主角图像在游戏面板中的矩形区域
            Rectangle rect = new Rectangle((this.Left) * Game.BlockImageWidth, (this.Top) * Game.BlockImageHeight, Game.BlockImageWidth * 4, Game.BlockImageHeight * 5);
            e.Graphics.DrawImage(m_Image, rect);
            
            
        }
    

    }
}
