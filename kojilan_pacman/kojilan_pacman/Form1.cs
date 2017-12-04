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
        public Form1()
        {
            InitializeComponent();
        }
        private List<List<int>> map = new List<List<int>>();

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

        int time = 0;
        private List<List<int>> map1 = new List<List<int>>();
        private Point pacman_location = new Point(0, 0);
        private Point enemy1_location = new Point(0, 0);
        private Point enemy2_location = new Point(0, 0);
        private Point enemy3_location = new Point(0, 0);
        private Point enemy4_location = new Point(0, 0);
        private int state1;
        private int state2;
        private int state3;
        private int state4;
        private int score;
        private bool cheak;
        public void draw_map(pacman_map map_data)
        {

            this.map = map_data.get_map_data(); //mapデータ
            this.pacman_location = map_data.Pacman_location; //パックマン座標
            this.enemy1_location = map_data.Enemy1_location; //敵1座標
            this.enemy2_location = map_data.Enemy2_location;//敵2座標
            this.enemy3_location = map_data.Enemy3_location;//敵3座標
            this.enemy4_location = map_data.Enemy4_location;//敵4座標
            this.state1 = map_data.Enemy1_state;
            this.state2 = map_data.Enemy2_state;
            this.state3 = map_data.Enemy3_state;
            this.state4 = map_data.Enemy4_state;
            this.time = map_data.Rest_turn;
            this.cheak = map_data.check_finish();
            this.score = map_data.announce_score();
            Bitmap img1 = new Bitmap(@"../../image/test0.png");    //道
            Bitmap img2 = new Bitmap(@"../../image\test1.png");    //餌ありの道
            Bitmap img3 = new Bitmap(@"../../image\test2.png");    //壁
            Bitmap img4 = new Bitmap(@"../../image\test4.png");    //パワー餌
            Bitmap pacman = new Bitmap(@"../../image\test5.png");    //pacman
            Bitmap bule = new Bitmap(@"../../image\test3.jpg");    //アオスケ
            Bitmap red = new Bitmap(@"../../image\test4.jpg");    //アカべイ
            Bitmap pink = new Bitmap(@"../../image\test5.jpg");    //ピンキー
            Bitmap orange = new Bitmap(@"../../image\test6.jpg");//グズタ
            Bitmap weak = new Bitmap(@"../../image\test7.jpg");//弱体中の敵
            pacman.SetResolution(96.0F, 96.0F);
            red.SetResolution(96.0F, 96.0F);
            bule.SetResolution(96.0F, 96.0F);
            pink.SetResolution(96.0F, 96.0F);
            orange.SetResolution(96.0F, 96.0F);
            weak.SetResolution(96.0F, 96.0F);


            //描画判断（map）
            for (int y = 0; y <= 21; y++)
            {
                for (int x = 0; x <= 18; x++)
                {
                    if (map[y][x] == 0)
                    {
                        DrawImage(img1, 20 * x, 20 * y);
                    }
                    if (map[y][x] == 1)
                    {
                        DrawImage(img2, 20 * x, 20 * y);
                    }
                    if (map[y][x] == 2)
                    {
                        DrawImage(img3, 20 * x, 20 * y);
                    }
                    if (map[y][x] == 3)
                    {
                        DrawImage(img4, 20 * x, 20 * y);
                    }
                }
            }
            label2.Text =score.ToString();
            label4.Text = time.ToString();
            DrawImage(pacman, 20 * pacman_location.X, 20 * pacman_location.Y);
            DrawImage(bule, 20 * enemy1_location.X, 20 * enemy1_location.Y);
            DrawImage(red, 20 * enemy2_location.X, 20 * enemy2_location.Y);
            DrawImage(pink, 20 * enemy3_location.X, 20 * enemy3_location.Y);
            DrawImage(orange, 20 * enemy4_location.X, 20 * enemy4_location.Y);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Game_controller game = new Game_controller(new pacman_map(),this , new pacman(), new enemy1(), new enemy2(), new enemy3 (),new enemy4 ());

            game.start_game();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
 
}


//   private Game_controller game_controlle;







