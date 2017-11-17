using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace kojilan_pacman
{
    public partial class Form1 : Form
    {

        private void DrawImage(Image prm_img, int x, int y)
        {
            // グラフィック用オブジェクトを生成
            Graphics gr = pictureBox1.CreateGraphics();

            // 画像の描画
            gr.DrawImage(prm_img, new Point(x, y));
        }

        private void ClearImage()
        {
            Graphics gr = pictureBox1.CreateGraphics();
            // ピクチャボックスのクリア
            gr.Clear(pictureBox1.BackColor);
           
        }
        private List<List<int>> map=new List<List<int>>();

        int time = 0;
        int[,] pacman = new int[,] { { 9, 11 } };
        private void button1_Click(object sender, EventArgs e)
        {
            //マップ情報
           map.Add(new List<int>(){ 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 });
           map.Add(new List<int>(){ 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2 });
           map.Add(new List<int>(){ 2, 1, 2, 2, 1, 2, 2, 2, 1, 2, 1, 2, 2, 2, 1, 2, 2, 1, 2 });
           map.Add(new List<int>(){ 2, 1, 2, 2, 1, 2, 2, 2, 1, 2, 1, 2, 2, 2, 1, 2, 2, 1, 2 });
           map.Add(new List<int>(){ 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 });
           map.Add(new List<int>(){ 2, 1, 2, 2, 1, 2, 1, 2, 2, 2, 2, 2, 1, 2, 1, 2, 2, 1, 2 });
           map.Add(new List<int>(){ 2, 1, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 1, 2 });
           map.Add(new List<int>(){ 2, 2, 2, 2, 1, 2, 2, 2, 0, 2, 0, 2, 2, 2, 1, 2, 2, 2, 2 });
           map.Add(new List<int>(){ 2, 0, 0, 2, 1, 2, 0, 0, 0, 0, 0, 0, 0, 2, 1, 2, 0, 0, 2 });
           map.Add(new List<int>(){ 2, 2, 2, 2, 1, 2, 0, 2, 2, 0, 2, 2, 0, 2, 1, 2, 2, 2, 2 });
           map.Add(new List<int>(){ 2, 0, 0, 2, 1, 0, 0, 2, 0, 0, 0, 2, 0, 0, 1, 2, 0, 0, 2 });
           map.Add(new List<int>(){ 2, 2, 2, 2, 1, 2, 0, 2, 2, 2, 2, 2, 0, 2, 1, 2, 2, 2, 2 });
           map.Add(new List<int>(){ 2, 0, 0, 2, 1, 2, 0, 0, 0, 0, 0, 0, 0, 2, 1, 2, 0, 0, 2 });
           map.Add(new List<int>(){ 2, 2, 2, 2, 1, 2, 0, 2, 2, 2, 2, 2, 0, 2, 1, 2, 2, 2, 2 });
           map.Add(new List<int>(){ 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2 });
           map.Add(new List<int>(){ 2, 1, 2, 2, 1, 2, 2, 2, 1, 2, 1, 2, 2, 2, 1, 2, 2, 1, 2 });
           map.Add(new List<int>(){ 2, 1, 1, 2, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 2, 1, 1, 2 });
           map.Add(new List<int>(){ 2, 2, 1, 2, 1, 2, 1, 2, 2, 2, 2, 2, 1, 2, 1, 2, 1, 2, 2 });
           map.Add(new List<int>(){ 2, 1, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 1, 2 });
           map.Add(new List<int>(){ 2, 1, 2, 2, 2, 2, 2, 2, 1, 2, 1, 2, 2, 2, 2, 2, 2, 1, 2 });
           map.Add(new List<int>(){ 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 });
           map.Add(new List<int>(){ 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 });

            //更新
            //map[10][3] = 2;
            //map[10][15] = 2;
                
            //　viewの部分

            //  Bitmap関係       
            Bitmap img1 = new Bitmap(@"C:\image\test0.png");    //道
            Bitmap img2 = new Bitmap(@"C:\image\test1.png");    //餌ありの道
            Bitmap img3 = new Bitmap(@"C:\image\test2.png");    //壁
            Bitmap img4 = new Bitmap(@"C:\image\test1.jpg");    //pacman
            //描画判断
            for (int y = 0; y <= 21; y++)
            {
                for (int x = 0; x <= 18; x++)
                { 
                    if (map[y][x] == 0)
                    {
                        DrawImage(img1, 25 * x, 25 * y);
                    }
                    if (map[y][x] == 1)
                    {
                        DrawImage(img2, 25 * x, 25 * y);
                    }
                    if (map[y][x] == 2)
                    {
                        DrawImage(img3, 25 * x, 25 * y);
                    }
                }
            }


            time++;
            DrawImage(img4, 25 * 9, 25 * (11-time));
            map[11-time][9] = 0;
        }


        private game_controller game_controller;


        public Form1()
        {
            InitializeComponent();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{

        //}
    }
    
}
