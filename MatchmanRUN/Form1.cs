﻿using System;
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
        Game game = null;


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
                button3.Visible = false;//让按钮隐藏
                timer1.Enabled = true;
            }
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
        protected override bool ProcessCmdKey(ref Message msg, Keys e)
        //重写ProcessCmdKey方法
        {
            if (button2.Text == "继续游戏") return true;//暂停时不响应键盘
            if (e == Keys.Up || e == Keys.Down || e == Keys.Space ||
                     e == Keys.Left || e == Keys.Right)
            {
                MyKeyPress(this, new KeyPressEventArgs((char)e));
            }
            return true;
        }

        private void MyKeyPress(Form1 form1, KeyPressEventArgs keyPressEventArgs)
        {
            throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "继续")
            {
                //timer1.Enabled = true;
                button2.Text = "暂停";
                button1.Visible = false;
                button3.Visible = false;
            }
            else
            {
                //timer1.Enabled = false;
                button2.Text = "继续";
                button3.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Text = "暂停";
            this.Dispose();
        }
    }
    }