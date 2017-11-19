using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace kojilan_pacman
{
    class View
    {
        public void DrawImage(Image prm_img, int x, int y)
        {
            // グラフィック用オブジェクトを生成
            Graphics gr = pictureBox1.CreateGraphics();

            // 画像の描画
            gr.DrawImage(prm_img, new Point(x, y));
        }

       public void ClearImage()
        {
            Graphics gr = pictureBox1.CreateGraphics();
            // ピクチャボックスのクリア
            gr.Clear(pictureBox1.BackColor);

        }

        public List<List<int>> map = new List<List<int>>();
        int x = 0;
        int y = 0;

        public static void draw_map(map)
        {
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
        }
            
           

    }
}
