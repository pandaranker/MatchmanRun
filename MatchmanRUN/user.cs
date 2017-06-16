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
    public partial class user : Form
    {
        public String player=null;
        public Form1 form2=null;
        public user(String play)
        {
            InitializeComponent();
            player = play;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            player = textBox1.Text;
            this.Hide();
        }
    }
}
