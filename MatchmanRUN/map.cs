using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MatchmanRUN
{
    class map
    {
        private short num;    //地图中障碍物数量
        private short left;   //最左坐标
        private short right;  //最右坐标
        private int[] WaitTime; //间隔出现时间
        private int ID;       //地图号
        public List<Block> blocks;   //存障碍物的集合
        public map(int I)
        {
            ID = I;
            blocks = new List<Block>();
            switch (ID)
            {
                case 1:
                    this.num = 4;
                    this.WaitTime = new int[num];
                    WaitTime[0] = 0; 
                    WaitTime[1] = 14;                            //障碍物之间间隔的格子数
                    WaitTime[2] = 28;
                    WaitTime[3] = 28;
                    this.left = 75;
                    this.right = 120;
                    blocks.Add(new Block(1));
                    blocks.Add(new Block(2));
                    blocks.Add(new Block(3));
                    blocks.Add(new Block(3));
                    blocks.ElementAt(3).Top -= 6;
                    break;
            }
            int count = 0;
            foreach (Block block in this.blocks)
            {
                block.addWaitTime(this.WaitTime[count]);
                count++;
            }
        }
        public bool Create()
        {
            if (right <= 75)
                return true; 
            else
                return false;
        }
        public void Move()                                    //地图左移
        {
            foreach (Block block in this.blocks)
            {
                block.Move();
            }
            
        }
        public void reallyMove()
        {
            if(blocks.First().reallyMove())
            {
                this.left--;
                this.right--;
            }
        }
        public bool DeleteMaps()            //满足条件删除地图
        {
            if (this.right <= 0)
                return true;
            else
                return false;
        }
        public bool gameOver(int a,int b)
        {
            foreach (Block block in this.blocks)
            {
                if (block.gameOver(a, b))
                    return true;
            }
            return false;
        }

        public void Draw(Graphics e)
        {
            //MessageBox.Show("  1  ");
            foreach (Block block in this.blocks)
            {
                if(block.ShouldDraw())
                block.Draw(e);
                

            }
            this.Move();     //每次绘图结束，进行移动与地图删除检测
            this.reallyMove();
            DeleteMaps();
        }
    }
}
