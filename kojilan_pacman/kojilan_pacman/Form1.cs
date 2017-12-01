using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kojilan_pacman
{
    public partial class Form1 : Form
    {
        private game_controller game_controller;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private System.Windows.Forms.PictureBox[] testPicureBox;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.testPicureBox = new System.Windows.Forms.PictureBox[418];
            int i = 0;
            for (int y = 0; y < 23; y++)
            {
                
                for(int x=0;x<20;x++)
                {
                    i++;
                    this.testPicureBox[i] = new System.Windows.Forms.PictureBox();
                    this.testPicureBox[i].Size = new Size(100, 100);
                    this.testPicureBox[i].Location = new Point( x * 100, y * 100);
                    if (Map[x][y] == 0)
                    {
                        this.testPicureBox[i].Image = Image.FromFile(@"C:\Documents and Settings\hogehoge\My Documents\My Pictures\00.gif");
                    }
                    if (Map[x][y] == 1)
                    {
                        this.testPicureBox[i].Image = Image.FromFile(@"C:\Documents and Settings\hogehoge\My Documents\My Pictures\01.gif");
                    }
                    if (Map[x][y] == 2)
                    {
                        this.testPicureBox[i].Image = Image.FromFile(@"C:\Documents and Settings\hogehoge\My Documents\My Pictures\02.gif");
                    }
                       

                    this.Controls.Add(testPicureBox[i]);
                }
               
            }

        }
    }
  