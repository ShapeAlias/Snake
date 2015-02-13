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

        Bitmap map;
        Graphics g;

        bool newGame = false;
        bool addLength = false;
        int lengthcount = 0;
        Point snakeHead;
        Color c = new Color();
        Pen pen;
        Rectangle bug;
        Color bc;
        Random r;
        SolidBrush brush = new SolidBrush(Color.Green);
        int score = 0;

        enum direction
        {
            Up,
            Down,
            Left,
            Right
        };

        direction dir;

        List<Point> snakeP = new List<Point>();



        public Snake()
        {
            InitializeComponent();
            map = new Bitmap(paint.Width, paint.Height);
            g = Graphics.FromImage(map);
            snakeHead = new Point(map.Width / 2, map.Height / 2);
            c = Color.Black;
            pen = new Pen(Color.Black);
            dir = direction.Up;
            r = new Random();
            
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (newGame)
            {
                // snake head movement
                if (e.KeyData == Keys.W && dir != direction.Down)
                {

                    dir = direction.Up;

                }

                else if (e.KeyData == Keys.S && dir != direction.Up)
                {

                    dir = direction.Down;

                }
                else if (e.KeyData == Keys.A && dir != direction.Right)
                {

                    dir = direction.Left;

                }
                else if (e.KeyData == Keys.D && dir != direction.Left)
                {

                    dir = direction.Right;

                } //end direction movement

            }

        }

        private void onPaint()
        {

            g.Clear(Color.Transparent);

            if (newGame)
            {
                for (int i = 0; i < snakeP.Count; i++)
                {

                    g.DrawLines(pen, snakeP.ToArray());

                }


                g.FillRectangle(brush, bug);
                

                paint.Image = map;
            }

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

            newGame = true;
            if (snakeP.Count == 1)
            {
                snakeP.Clear();
            }
            g.Clear(Color.Transparent);
            snakeP.Add(snakeHead);

            for (int i = 1; i <= 60; i++)
            {
                snakeP.Add(new Point(snakeHead.X, snakeHead.Y + i));
            }

            dir = direction.Up;

            bug = new Rectangle(5 + r.Next(map.Width - 10), 5 + r.Next(map.Height - 10), 5, 5);

            createBug();

            tickEvent.Enabled = true;
            tickEvent.Start();

            paint.Refresh();

        }

        private void createBug()
        {
            bool isSnake = true;
            
            while(isSnake)
            {

                bug = new Rectangle(5 + r.Next(map.Width - 10), 5 + r.Next(map.Height - 10), 5, 5);

                for (int i = 1; i < snakeP.Count - 1; i++)
                {
                    
                    if(snakeP[i].X >= bug.X && snakeP[i].X <= (bug.X) + bug.Width && snakeP[i].Y >= bug.Y && snakeP[i].Y <= (bug.Y) + bug.Height)
                    {
                        bug = new Rectangle(5 + r.Next(map.Width - 10), 5 + r.Next(map.Height - 10), 5, 5);
                    }
                    else
                    {
                        isSnake = false;
                    }

                }



            }


        }

        private void highScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Application.Exit();

        }

        private void gameOver()
        {
            newGame = false;
            MessageBox.Show(this, "You Lost!\nYour Score: " + score.ToString(), "Game Over", MessageBoxButtons.OK);
            g.Clear(paint.BackColor);
            paint.Image = map;
            snakeP.Clear();

        }

        private void tickEvent_Tick(object sender,  EventArgs e)
        {


            

            if (newGame)
            {
               // paint.BackColor = Color.Black;
                for (int i = snakeP.Count - 1; i >= 1; --i)
                {


                    if (snakeP[i] == snakeP[snakeP.Count - 1] && lengthcount >= 0)
                    {
                        snakeP.Add(snakeP[snakeP.Count - 1]);
                        lengthcount--;
                    }

                    snakeP[i] = snakeP[i - 1];

                }



                // snake head movement
                if (dir == direction.Up)
                {
                    if (snakeP[0].Y == 0)
                    {
                        snakeP[0] = new Point(snakeP[0].X, 0);

                    }
                    else
                    {
                        snakeP[0] = new Point(snakeP[0].X, snakeP[0].Y - 1);
                    }
                }

                 if (dir == direction.Down)
                {

                    if (snakeP[0].Y == map.Height - 3)
                    {


                    }
                    else
                    {
                        snakeP[0] = new Point(snakeP[0].X, snakeP[0].Y + 1);
                    }

                }
                else if (dir == direction.Left)
                {

                    if (snakeP[0].X == 0)
                    {


                    }
                    else
                    {
                        snakeP[0] = new Point(snakeP[0].X - 1, snakeP[0].Y);
                    }

                }
                else if (dir == direction.Right)
                {

                    if (snakeP[0].X == map.Width - 3)
                    {


                    }
                    else
                    {
                        snakeP[0] = new Point(snakeP[0].X + 1, snakeP[0].Y);
                    }

                    

                } //end snake head movement


                 //bug eaten check
                if(snakeP[0].X >= bug.X && snakeP[0].X <= (bug.X) + bug.Width && snakeP[0].Y >= bug.Y && snakeP[0].Y <= (bug.Y) + bug.Height)
                {
                    addLength = true;
                    score++;
                    lengthcount = 25;
                    lblScore.Text = "Score:" + score;
                    createBug();
                }


                

                onPaint(); // paint

                //Lose conditions checked below
                if (snakeP[0].X == 0 || snakeP[0].Y == 0 || snakeP[0].X == map.Width - 3 || snakeP[0].Y == map.Height - 3)
                {

                    gameOver();

                }

                for(int i = snakeP.Count - 1; i >= 1; --i)
                {
                    if(snakeP[0] == snakeP[i])
                    {
                        gameOver();
                        break;
                    }

                }

            }
        }
    }
            
    }



