using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MatchmanRUN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);

            SetStyle(ControlStyles.ResizeRedraw, true);

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            SetStyle(ControlStyles.Selectable, true);
            //给控件们设置父背景，达成透明效果。
            label1.Parent = button4;

            button1.Parent = pictureBox1;

            button3.Parent = pictureBox1;

            button4.Parent = pictureBox1;

            label2.Parent = button4;


        }

        Game game = null;      
        public String player = "user";                                            //玩家名
        user user1 = new user("user");                                            //框架user类，直接实例化
        Font font = new Font("幼圆", 16);                                         //画笔字体
        Brush brush = new SolidBrush(Color.FromArgb(255, 255, 255));              //画笔颜色
        //String defultpath = System.IO.Directory.GetCurrentDirectory();          //备用的方案
        DalUsers strsql = new DalUsers();           //关于数据库类的一个对象
        String[] str1 = new String[11];
        String[] str2 = new String[11];

        bool drawscore = false;               //绘制排行榜的开关
        private void button1_Click(object sender, EventArgs e)
        {
            game = new Game();

            pictureBox1.Height = Game.BlockImageHeight * Game.PlayingFieldHeight + 3;
            pictureBox1.Width = Game.BlockImageWidth * Game.PlayingFieldWidth + 3;
            pictureBox1.Invalidate();

            timer1.Enabled = true;
            if (button1.Left >= 5)
            {
                button1.Visible = false;
                button2.Visible = true;
                button3.Visible = false;                   //让按钮隐藏
                button4.Visible = false;
                timer1.Enabled = true;
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //重画游戏面板

            if (game != null)
            {
                game.DrawMap(e);
                game.DrawMatchman(e);
                e.Graphics.DrawString(label2.Text + "   " + label1.Text, font, brush, 200, 0);
            }
            else if (drawscore)
            {
                for (int i = 0; i < 10 && i < str1.Length; i++)
                {
                    e.Graphics.DrawString(str1[i], font, brush, 500, i * 20);
                    e.Graphics.DrawString(str2[i], font, brush, 600, i * 20);
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (game.over == false)
            {
                game.score++;
                label1.Text = "得分:" + game.score.ToString();

                pictureBox1.Invalidate();

            }
            else if (game.over == true)
            {
                timer1.Enabled = false;
                strsql.InsertScore(user1.player,game.score);
                game = null;
                pictureBox1.Invalidate();
                if (button2.Text == "暂停")
                    MessageBox.Show("游戏结束，" + label1.Text, "提示");
                button2.Visible = false;
                button2.Text = "暂停";
                button1.Visible = true;
                button3.Text = "结束游戏";
                button3.Visible = true;
                button4.Visible = true;

            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys e)
        //重写ProcessCmdKey方法
        {
            if (timer1.Enabled == true)//暂停时不响应键盘

                if (e == Keys.Up)
                {
                    MyKeyPress(this, new KeyPressEventArgs((char)e));
                }
            if (e == Keys.Space && game != null && !game.over)
            {
                button2_Click(null, null);
            }

            return true;
        }

        private void MyKeyPress(Form1 form1, KeyPressEventArgs keyPressEventArgs)
        {

            game.Jump();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "继续")
            {
                //timer1.Enabled = true;
                timer1.Enabled = true;
                button2.Text = "暂停";
                button1.Visible = false;
                button3.Visible = false;
            }
            else
            {
                timer1.Enabled = false;
                //timer1.Enabled = false;
                button2.Text = "继续";
                button3.Text = "返回主界面";
                button3.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "返回主界面")
            {
                button2.Text = "继续";
                game.over = true;
                timer1.Enabled = true;
            }
            else if (button3.Text == "结束游戏")
                this.Dispose();
            else if (button3.Text == "修改玩家名")
            {
                user1.ShowDialog();
                label2.Text = "当前玩家:" + user1.player;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            user1.ShowDialog();
            label2.Text = "当前玩家:" + user1.player;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "返回主界面")
            {
                drawscore = false;
                button1.Visible = true;
                button3.Text = "结束游戏";
                button4.Text = "排行榜";
                pictureBox1.Invalidate();
            }
            else if(button4.Text == "排行榜")
            {
                drawscore = true; 
                DataTable score = strsql.GetAllUsers();
                for (int i = 0; i<10 && i < score.Rows.Count && (str1[i] = score.Rows[i][0].ToString()) != null; i++) { }
                for (int i = 0; i<10 && i < score.Rows.Count && (str2[i] = score.Rows[i][1].ToString()) != null; i++) { }
                pictureBox1.Invalidate();
                button1.Visible = false;
                button3.Text = "修改玩家名";
                button4.Text = "返回主界面";
            }
            //drawscore = false;
        }
        public void sortScore()
        {
            String strch;
            for (int i = 0; i < 10; i++)
            {
                for (int j = i; j < 10; j++)
                {
                    if (Convert.ToInt32(str2[i]) < Convert.ToInt32(str2[j]))
                    {
                        strch = str1[j];
                        str1[j] = str1[i];
                        str1[i] = strch;
                        strch = str2[j];
                        str2[j] = str2[i];
                        str2[i] = strch;
                    }
                }
            }
        }

    }
}
