using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGame
{
    public partial class Form1 : Form
    {

        bool goleft, goright, juming, isGameOver;
        int jumSpeed;
        int force;
        int score=0;
        int playerSpeed = 7;

        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        int enemyOneSpeed = 5;
        int enemyTwospeed = 3;
        public Form1()
        {
            InitializeComponent();
        }

        

        private void MainGameTimeEvent(object sender, EventArgs e)
        {
            txtscore.Text = "Score" + score;
            player.Top += jumSpeed;
            if (goleft==true)
            {
                player.Left -= playerSpeed;
            }
            if (goright==true)
            {
                player.Left += playerSpeed;
            }

            if (juming==true && force<0)
            {
                juming = false;
            }
            if (juming == true)
            {
                jumSpeed = -8;
                force -= 1;
            }
            else
            {
                jumSpeed = 10;
            }
            foreach (Control x in Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag=="platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;

                            if ((string)x.Name=="horinzotalPlatform" && goleft==false || (string)x.Name == "horinzotalPlatform" && goright == false)
                            {
                                player.Left -= horizontalSpeed;
                            }
                           

                        }
                        x.BringToFront();
                    }
                    if ((string)x.Tag=="coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds)&&x.Visible==true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }

                    if ((string)x.Tag=="enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            txtscore.Text = "Score" + score + Environment.NewLine +"You are dead man!";
                        }
                      
                    
                    }
                    


                }
            }

            horizontalPlatform.Left -= horizontalSpeed;

            if (horizontalPlatform.Left<0 || horizontalPlatform.Left+horizontalPlatform.Width>this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }
            verticalPlatform.Top += verticalSpeed;

            if (verticalPlatform.Top<195|| verticalPlatform.Top>526)
            {
                verticalSpeed = -verticalSpeed;
            }
            enemyOne.Left -= enemyOneSpeed;

            if (enemyOne.Left < pictureBox4.Left || enemyOne.Left + enemyOne.Width>pictureBox4.Left+pictureBox4.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }
            enemyTwo.Left += enemyTwospeed;
            if (enemyTwo.Left<pictureBox2.Left || enemyTwo.Left + enemyTwo.Width>pictureBox2.Left+pictureBox2.Width)
            {
                enemyTwospeed = -enemyTwospeed;
            }
            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtscore.Text = "Score" + score + Environment.NewLine + "You are dead man";
            }
            if (player.Bounds.IntersectsWith(door.Bounds) && score == 20)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtscore.Text = "Score" + score + Environment.NewLine + "You win man";
            }
            //else
            //{
            //    txtscore.Text = "Score" + score + Environment.NewLine + "Collect all the conits";
            //}

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode==Keys.Right)
            {
                goright = true;
            }
            if (e.KeyCode==Keys.Space && juming==false)
            {
                juming = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (juming == true)
            {
                juming = false;
            }
            if (e.KeyCode==Keys.Enter&& isGameOver==true)
            {
                RestartGame();
            }
        }

        private void RestartGame()
        {

            juming = false;
            goleft = false;
            goright = false;
            isGameOver = false;
            score = 0;
            txtscore.Text = "Score" + score;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible==false)
                {
                    x.Visible = true;
                }
            }
            //reset

            player.Left = 43;
            player.Top = 678;
            enemyTwo.Left = 473;
            enemyOne.Left = 430;

            horizontalPlatform.Left = 284;
            verticalPlatform.Top = 484;

            gameTimer.Start();

        }
    }
}
