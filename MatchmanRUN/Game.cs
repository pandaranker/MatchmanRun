using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MatchmanRUN
{
    class Game
    {
        public const int BlockImageWidth = 12;       //每个格子的大小
        public const int BlockImageHeight = 12;
        public const int PlayingFieldWidth = 75;     //游戏面板大小
        public const int PlayingFieldHeight = 50;
        private int[,] pile;                         //存储方块位置
        private Matchman role;
        private List<map> maps;
        public int score = 0;
        public bool over = false;                    //游戏是否结束
        public Game()                                //Game类构造函数
        {
           // pile = new int[PlayingFieldWidth, PlayingFieldHeight];
           // ClearPile();
            CreateNewMan();
            CreateNewMaps();
            CreateNewMap();

        }
        private void CreateNewMan()
        {
            role = new Matchman();
        }
        private void CreateNewMaps()
        {
            maps = new List<map>();
        }
        private void CreateNewMap()
        {
            Random r = new Random();
            maps.Add(new map(r.Next(1,7)));
        }
        public void DrawMatchman(PaintEventArgs e)
        {
            if(role != null)   //检查当前角色是否为空
            {
                role.Draw(e.Graphics);
                if((role.state==1 || role.state == 3) && freeDown() && role.Top!=25)
                {
                    if (role.Top + 2 < 25)
                    {
                        role.Top += 2;
                        role.state = 3;
                        role.Movex = 0;
                    }
                    else
                    {
                        role.Top = 25;
                        role.state = 1;
                    }
                }
            }
        }
        public void DrawMap(PaintEventArgs e)
        {
            if (maps != null && maps.Count != 0)          //判断地图组是否有图
            {
                if (maps.First().DeleteMaps())
                {
                    maps.RemoveAt(0);                     //图若到尽头进行删除
                }
            }
            if(/*maps.Count <3 && */maps.ElementAt(maps.Count-1).Create())
            {
                 CreateNewMap();
            }
               foreach (map Map in maps)
            {
                int a, b, c;
                role.DeadLine(out a, out b, out c);
                //MessageBox.Show(a.ToString());
                foreach (Block block in Map.blocks)
                {
                    if (((role.state==2&&role.Movex>6)||role.state==3) && block.underBlock(b, c, a))                   //跳上台
                    {
                        //over = true;
                        
                        role.state = 1;
                        role.Movex = 0;
                        role.Top = block.Top-4;
                        //MessageBox.Show(role.Top.ToString());
                    }

                }
                role.DeadLine(out a, out b, out c);
                if (Map.gameOver(a, b)) over  = true;
                Map.Draw(e.Graphics);
                
            }
            
            
        }
        public bool freeDown()
        {
            foreach (map Map in maps)
            {
                int a, b, c;
                role.DeadLine(out a, out b, out c);
                //MessageBox.Show(a.ToString());
                foreach (Block block in Map.blocks)
                {
                    if (block.underAir(b, c, a)||block.underBlock(b,c,a))
                    { return false; }

                }
            }
            return true;
        }
        private void ClearPile()//清空游戏面板中的所有方砖
        {
            for (int i = 0; i < PlayingFieldWidth; i++)
            {
                for (int j = 0; j < PlayingFieldHeight; j++)
                {
                    pile[i, j] = 0;
                }
            }
        }
        /*public bool NothingUnder(map Map)
        {
            foreach (Block block in Map.blocks)
            {
                if()
            }
        }*/
        
        /*
        public void gameOver()
        {
            foreach(map Map in maps)
            {
                int a, b;                                 // 备用的结束方案
                role.DeadLine(out a, out b);
                
            }

        }*/
        public void Jump()
        {
            role.jump();
        }
        public void DrawPile(Graphics g)
        {
            Image brickImage = mman.block;
            for (int i = 0; i < PlayingFieldWidth; i++)
            {
                for (int j = 0; j < PlayingFieldHeight; j++)
                {
                    if (pile[i, j] == 1)
                    {
                        Rectangle rect = new Rectangle(i * BlockImageWidth, j * BlockImageHeight, BlockImageWidth, BlockImageHeight);   //(j - 1)
                        g.DrawImage(brickImage, rect);
                    }
                }
            }
        }

    }
}
