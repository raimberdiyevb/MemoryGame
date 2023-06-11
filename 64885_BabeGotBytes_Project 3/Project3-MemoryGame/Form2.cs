using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace Project3_MemoryGame
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        Random random = new Random();
        List<int> numlist = new List<int>();
        int box = Player.BoardSize;
        private void randomnum()//Random number of all tiles (depend on board size [Width * Height])
        {
            for (int i = 0; i < 1; i++)
            {
                int randomNumber;
                do
                {
                    randomNumber = random.Next(1, (box * box) + 1); 
                } while (numlist.Contains(randomNumber));

                numlist.Add(randomNumber);
            }
        }

        double second = Convert.ToDouble(Player.time);
        List<System.Windows.Forms.Button> Blist;
        private void HideTiles1()//Build Tiles, Random number of all tiles, Hide number of Tiles
        {
            int b = box;
            Blist = new List<System.Windows.Forms.Button>();

            for (int j = 1; j <= b; j++)
            {
                for (int i = 1; i <= b; i++)
                {
                    System.Windows.Forms.Button B = new System.Windows.Forms.Button();
                    B.Location = new System.Drawing.Point(140 + i * 60, -50 + j * 60);
                    B.Size = new System.Drawing.Size(50, 50);
                    B.BackColor = System.Drawing.Color.Black;
                    B.ForeColor = System.Drawing.Color.Black;
                    B.Font = new System.Drawing.Font("BoonTook Mon Ultra", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    B.TabIndex = i;

                    randomnum();

                    foreach (int num in numlist)
                    {
                        B.Text = num.ToString();
                    }

                    B.Name = "B" + Convert.ToInt32(B.Text);
                    B.Click += B_Click;
                    B.Tag = Convert.ToInt32(B.Text);
                    B.Enabled = false;
                    Controls.Add(B);
                    Blist.Add(B);
                }
            }
        }

        private void HideTiles2()//Hide Number of Tiles and User can Click the Tiles
        {
            foreach (System.Windows.Forms.Button B in Blist)
            {
                B.BackColor = System.Drawing.Color.Black;
                B.Enabled = true;
            }
        }

        int numtiles = Player.NumberofTiles;
        List<int> validNumbers = new List<int>();
        private void numtilesonly()//Pick only Number of tiles that specified by User
        //(if user enter number of tiles = 4; The program will pick number between 1 to 4)
        {
            for (int w = 1; w <= numtiles; w++)
            {
                validNumbers.Add(w);
            }
        }

        private void ShowTiles()//Show Number of tiles that specified by User, User can't Click the Tiles
        {
            foreach (System.Windows.Forms.Button B in Blist)
            {
                numtilesonly();
                if (!validNumbers.Contains(Convert.ToInt32(B.Text)))
                {
                    B.Text = "";//Delete all numbers not specified by the user.
                }
                else
                {
                    B.BackColor = System.Drawing.Color.White;
                }
                B.Enabled = false;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label6.Text = Player.name;
            label7.Text = Player.NumberofTiles.ToString();
            label8.Text = Player.NumberofLives.ToString();
            this.Width = 170 + box * 70;
            this.Height = box * 70;
            button1.Location = new System.Drawing.Point(40, this.Height - 100);
            button2.Location = new System.Drawing.Point(40, this.Height - 60);
            HideTiles1();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "RESTART") 
            {
                //Restart form2 , All Information from Form1 still available
                this.Hide();
                Form2 f2 = new Form2();
                f2.ShowDialog(); ;
                this.Close();
            }
            else 
            {
                //Start Game
                ShowTiles();
                timer1.Start();
                label2.Text = second.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Go Back to Form1
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
            this.Close();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (second > 0)
            {
                second -= 1; //Countdown time (1000 interval)
                label2.Text = second.ToString("0");

            }
            else if (second == 0)
            {
                timer1.Stop();
                HideTiles2();
                timer2.Start();
            }
        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            label2.Text = second.ToString("0.0");
            second += 0.1; //Countup time (100 interval)
        }

        int lives = Player.NumberofLives;
        int Numc = 1; 
        DialogResult result = new DialogResult();
        private void B_Click(object sender, EventArgs e)
        {
            // creating a object of the same button class and typecasting the button object and assigning to a local object,  accessing it's properties.
            System.Windows.Forms.Button clickedButton = (System.Windows.Forms.Button)sender;
            string tileNumber = clickedButton.Text;

            if (tileNumber != "")//if tileNumber is not empty
            {
                int tt = Convert.ToInt32(tileNumber);

                if (tt == Numc)//if tileNumber = Number of tiles that specified by User
                {
                    Numc++;
                    clickedButton.BackColor = System.Drawing.Color.Lime; //Change the button color to GREEN for correct selection
                    clickedButton.ForeColor = System.Drawing.Color.Black;
                    numtiles--;
                    label7.Text = numtiles.ToString();

                    if (numtiles == 0)//All correct Clicks by the User
                    {
                        timer2.Stop();
                        
                        string timeshow = label2.Text;
                        foreach (System.Windows.Forms.Button B in Blist)
                        {
                            B.Enabled = false;
                        }
                        string message = $"You Solve the puzzle in {timeshow} seconds";
                        result = MessageBox.Show(message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button1.Text = "RESTART";//Appear RESTART Button
                    }
                }

                else if (tt != Numc) //If User Click Numbered tiles but clicked in wrong order
                {
                    //it will be changed to RED
                    clickedButton.BackColor = System.Drawing.Color.Red;
                    clickedButton.ForeColor = System.Drawing.Color.Red;
                    lives--;
                    label8.Text = lives.ToString();

                    if (lives == 0)//Decreasing Number of Lives
                    {
                        timer2.Stop();
                        foreach (System.Windows.Forms.Button B in Blist)
                        {    
                            B.ForeColor = System.Drawing.Color.White;
                        }
                        string message = $"Your Lives over and You Lost.";
                        result = MessageBox.Show(message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button1.Text = "RESTART";
                        foreach (System.Windows.Forms.Button B in Blist)
                        {
                            B.Enabled = false;
                        }
                    }
                }
            }

            else if (tileNumber == "") //If User Click Empty Tiles
            {
                clickedButton.BackColor = System.Drawing.Color.Red;
                clickedButton.ForeColor = System.Drawing.Color.Red;
                lives--;
                label8.Text = lives.ToString();
                if (lives == 0)
                {
                    timer2.Stop();
                    foreach (System.Windows.Forms.Button B in Blist)
                    {
                        B.ForeColor = System.Drawing.Color.White;
                    }
                    string message = $"Your Lives over and You Lost.";
                    result = MessageBox.Show(message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button1.Text = "RESTART";
                    foreach (System.Windows.Forms.Button B in Blist)
                    {
                        B.Enabled = false;
                    }
                }
            }
        }
        
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
