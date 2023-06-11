using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Project3_MemoryGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Easy")
            {
                textBox1.Text = "5";
                textBox1.ReadOnly = true;
                textBox2.Text = "3";
                textBox2.ReadOnly = true;
                textBox3.Text = "2";
                textBox3.ReadOnly = true;
                textBox4.Text = "4";
                textBox4.ReadOnly = true;

            }

            else if (comboBox1.SelectedItem.ToString() == "Medium")
            {
                textBox1.Text = "6";
                textBox1.ReadOnly = true;
                textBox2.Text = "2";
                textBox2.ReadOnly = true;
                textBox3.Text = "3";
                textBox3.ReadOnly = true;
                textBox4.Text = "5";
                textBox4.ReadOnly = true;
            }

            else if (comboBox1.SelectedItem.ToString() == "Hard")
            {
                textBox1.Text = "7";
                textBox1.ReadOnly = true;
                textBox2.Text = "2";
                textBox2.ReadOnly = true;
                textBox3.Text = "4";
                textBox3.ReadOnly = true;
                textBox4.Text = "7";
                textBox4.ReadOnly = true;
            }

            else if (comboBox1.SelectedItem.ToString() == "Custom")
            {
                textBox1.Text = string.Empty;
                textBox1.ReadOnly = false;
                textBox2.Text = string.Empty;
                textBox2.ReadOnly = false;
                textBox3.Text = string.Empty;
                textBox3.ReadOnly = false;
                textBox4.Text = string.Empty;
                textBox4.ReadOnly = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            if (PlayerNameTextBox.Text == "")
            {
                MessageBox.Show("Please enter the user Name.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (comboBox1.Text == "")
            {
                MessageBox.Show("Please choose the Difficulty.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter the Board Size.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Please enter the Number of Lives.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Please enter the amount of waiting time before the game starts.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else if (textBox3.Text == "0")
            {
                MessageBox.Show("The amount of waiting time have to be more than 0.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else if (textBox4.Text == "")
            {
                MessageBox.Show("Please enter the Number of Tiles.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else if (Math.Pow(Convert.ToInt32(textBox1.Text), 2) < Convert.ToInt32(textBox4.Text))
            {
                MessageBox.Show("The number of the answer Tiles cannot be more than number of all Tiles.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else
            {
                Player.name = PlayerNameTextBox.Text;
                Player.BoardSize = Convert.ToInt32(textBox1.Text);
                Player.NumberofLives = Convert.ToInt32(textBox2.Text);
                Player.time = Convert.ToInt32(textBox3.Text);
                Player.NumberofTiles = Convert.ToInt32(textBox4.Text);

            //OPEN Form2 & CLOSE Form1
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog(); ;
            this.Close();
            }
            
        }
    }
}
