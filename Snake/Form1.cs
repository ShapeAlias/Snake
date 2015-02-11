using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Snake : Form
    {

        bool newGame = false;


        public Snake()
        {
            InitializeComponent();
        }

        private void keyDown(object sender, KeyEventArgs e)
        {




        }

        private void onPaint(object sender, PaintEventArgs e)
        {



        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

            newGame = true;

        }

        private void highScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Application.Exit();

        }
    }
}
