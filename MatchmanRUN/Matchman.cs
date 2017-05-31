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
        private int ID = 1;      //主角号
        private int fpi = 0;     //当前帧数，任何状态统一一个帧数
        private int state = 1;   //状态，用这个来设置主角状态
        private int oldstate = 1;//上一个状态
        public int[,] shape;
        Bitmap brickImage = mman.ManRun;
        private EventHandler evtHandler = null;
        public Matchman()
        {
            switch (this.ID)
            {
                case 1:
                    this.Width = 3;
                    this.Height = 4;
                    this.Left = 3;
                    this.top = 25;
                    shape = new int[this.Width, this.Height];
                    shape[0, 0] = 1; shape[0, 1] = 1; shape[0, 2] = 1; shape[0, 3] = 1;//一号主角是3*4规格的矩形
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
            Image m_Image=null;
            switch (this.state)                //用switch语句确定改变当前动作图
            {
                case 1:
                    m_Image = mman.ManRun;
                    break;
                case 2:
                    m_Image = mman.ManJumpX;
                    break;
            }
            if (oldstate != state)
            {
                oldstate = state;
                fpi = 0;
            }
            FrameDimension frameDim = new FrameDimension(m_Image.FrameDimensionsList[0]);//存储gif图片的相应信息
            int count = m_Image.GetFrameCount(frameDim);                                 //获取图片总帧数
            if (fpi <= count - 2) fpi = fpi + 1;                                         //帧数递增
            else fpi = 0;
            m_Image.SelectActiveFrame(frameDim, fpi);                                    //把图片设置到目标帧
            Rectangle rect = new Rectangle((this.Left) * Game.BlockImageWidth, (this.Top) * Game.BlockImageHeight, Game.BlockImageWidth * 3, Game.BlockImageHeight * 4);
                                                                                         //得到主角图像在游戏面板中的矩形区域
            e.Graphics.DrawImage(m_Image, rect);                                         //在picturebox上画出目标


        }


    }
}

