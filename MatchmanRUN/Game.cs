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
        public const int BlockImageWidth = 11;      //每个格子的大小
        public const int BlockImageHeight = 11;
        public const int PlayingFieldWidth = 75;     //游戏面板大小
        public const int PlayingFieldHeight = 50;
        private int[,] pile;                         //存储方块位置
        private Matchman role;
        public int score = 0;
        public bool over = false;                    //游戏是否结束
        public Game()//Game类构造函数
        {
            pile = new int[PlayingFieldWidth, PlayingFieldHeight];
            ClearPile();
            CreateNewMan();

        }
        private void CreateNewMan()
        {
            role = new Matchman();
        }
        public void DrawMatchman(PaintEventArgs e)
        {
            if(role != null)   //检查当前角色是否为空
            {
                role.Draw(e);
            }
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
        public void DrawPile(Graphics g)
        {
            Image brickImage = Image.FromFile("image/block1.gif");
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
