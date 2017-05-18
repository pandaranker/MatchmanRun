using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchmanRUN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 250;
        }
        Game game=null;
        

        private void button1_Click(object sender, EventArgs e)
        {
            game = new Game();
            pictureBox1.Height = Game.BlockImageHeight * Game.PlayingFieldHeight + 3;
            pictureBox1.Width = Game.BlockImageWidth * Game.PlayingFieldWidth + 3;
            pictureBox1.Invalidate();
            timer1.Enabled = true;
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //重画游戏面板
            
            if (game != null)
            {
                game.DrawMatchman(e.Graphics);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (game.over == false)
            {
                pictureBox1.Invalidate();
            }
            else if (game.over == true)
            {
                timer1.Enabled = false;
                MessageBox.Show("游戏结束,", "提示");
            }
        }
    }
}
