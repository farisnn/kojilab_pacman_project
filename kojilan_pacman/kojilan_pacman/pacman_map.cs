using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace kojilan_pacman
{
    class pacman_map
    {
        private List<List<int>> map_data;

        //実際の移動を示す型の宣言
        public enum map_direction_def { up, down, left, light, stop }


        //初期座標の設定
        public Point pacman_location = new Point(0, 0);
        public Point enemy1_location = new Point(0, 0);
        public Point enemy2_location = new Point(0, 0);
        public Point enemy3_location = new Point(0, 0);
        public Point enemy4_location = new Point(0, 0);



        /// <summary>
        /// キャラクターの移動した方向を示す変数とその初期値（取りあえずstop）
        /// </summary>
        public map_direction_def pacman_direction = map_direction_def.stop;
        public map_direction_def enemy1_direction = map_direction_def.stop;
        public map_direction_def enemy2_direction = map_direction_def.stop;
        public map_direction_def enemy3_direction = map_direction_def.stop;
        public map_direction_def enemy4_direction = map_direction_def.stop;
        
        public int player_score = 0;
        public int rest_turn = 100;
        public int enemy1_state = 0;
        public int enemy2_state = 0;
        public int enemy3_state = 0;
        public int enemy4_state = 0;

        bool game_over = false;



        /// <summary>
        /// コンストラクタ。一応マップの定義はここでやってる
        /// </summary>
        pacman_map()
        {
            map_data = new List<List<int>>();
            map_data.Add(new List<int>() { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 });
            map_data.Add(new List<int>() { 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2 });
            map_data.Add(new List<int>() { 2, 1, 2, 2, 1, 2, 2, 2, 1, 2, 1, 2, 2, 2, 1, 2, 2, 1, 2 });
            map_data.Add(new List<int>() { 2, 1, 2, 2, 1, 2, 2, 2, 1, 2, 1, 2, 2, 2, 1, 2, 2, 1, 2 });
            map_data.Add(new List<int>() { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 });
            map_data.Add(new List<int>() { 2, 1, 2, 2, 1, 2, 1, 2, 2, 2, 2, 2, 1, 2, 1, 2, 2, 1, 2 });
            map_data.Add(new List<int>() { 2, 1, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 1, 2 });
            map_data.Add(new List<int>() { 2, 2, 2, 2, 1, 2, 2, 2, 0, 2, 0, 2, 2, 2, 1, 2, 2, 2, 2 });
            map_data.Add(new List<int>() { 2, 0, 0, 2, 1, 2, 0, 0, 0, 0, 0, 0, 0, 2, 1, 2, 0, 0, 2 });
            map_data.Add(new List<int>() { 2, 2, 2, 2, 1, 2, 0, 2, 2, 0, 2, 2, 0, 2, 1, 2, 2, 2, 2 });
            map_data.Add(new List<int>() { 2, 0, 0, 2, 1, 0, 0, 2, 0, 0, 0, 2, 0, 0, 1, 2, 0, 0, 2 });
            map_data.Add(new List<int>() { 2, 2, 2, 2, 1, 2, 0, 2, 2, 2, 2, 2, 0, 2, 1, 2, 2, 2, 2 });
            map_data.Add(new List<int>() { 2, 0, 0, 2, 1, 2, 0, 0, 0, 0, 0, 0, 0, 2, 1, 2, 0, 0, 2 });
            map_data.Add(new List<int>() { 2, 2, 2, 2, 1, 2, 0, 2, 2, 2, 2, 2, 0, 2, 1, 2, 2, 2, 2 });
            map_data.Add(new List<int>() { 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2 });
            map_data.Add(new List<int>() { 2, 1, 2, 2, 1, 2, 2, 2, 1, 2, 1, 2, 2, 2, 1, 2, 2, 1, 2 });
            map_data.Add(new List<int>() { 2, 1, 1, 2, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 2, 1, 1, 2 });
            map_data.Add(new List<int>() { 2, 2, 1, 2, 1, 2, 1, 2, 2, 2, 2, 2, 1, 2, 1, 2, 1, 2, 2 });
            map_data.Add(new List<int>() { 2, 1, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 1, 2 });
            map_data.Add(new List<int>() { 2, 1, 2, 2, 2, 2, 2, 2, 1, 2, 1, 2, 2, 2, 2, 2, 2, 1, 2 });
            map_data.Add(new List<int>() { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 });
            map_data.Add(new List<int>() { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 });




        }

        /// <summary>
        /// 現在のマップのデータを返す関数
        /// </summary>
        /// <returns>マップデータ（リストのリスト）</returns>
        public List<List<int>> get_map_data()
        {
            return map_data;
        }




        public void update_map_data(character.Direction_def pacman_direction, character.Direction_def enemy1_direction, character.Direction_def enemy2_direction, character.Direction_def enemy3_direction, character.Direction_def emeny4_direction)
        {

            switch (pacman_direction) {

                case character.Direction_def.up:
                    //TODO ここにはupの時のロジックを書く
                    break;
                case character.Direction_def.down:
                    //TODO ここにdownの時のロジックを書く
                    break;
                case character.Direction_def.left:
                    //TODO ここにleftの時のロジックを書く
                    break;
                case character.Direction_def.right:
                    //TODO ここにrightの時のロジックを書く
                    break;
                default:
                    //ここに来るときはエラー以外無し
                    break;
            }


            //TODO ここに座標の重なり判定を書く
            



            //残りターンを減らす。そして0になったらゲームオーバ－フラグをtrue

            rest_turn--;
            if (rest_turn == 0)
                game_over = true;
             
        }





        /// <summary>
        /// ゲームオーバーの判定を返す。
        /// </summary>
        /// <returns>game_overの値(bool)</returns>
        public bool check_finish()
        {
            return game_over;
        }


        /// <summary>
        /// 今のところスコアだけ返す関数
        /// </summary>
        /// <returns>player_scoreの値</returns>
        public int announcre_score()
        {

            return player_score;

        }









        //マップの読み込み
        public void loadMap()
        {
            rawmap = new List<List<int>>();
           // int map[][]
        //マップデータ　使うかどうかは不明 
        int [,] map = new int [,]{
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2 },
                { 2, 1, 2, 2, 1, 2, 2, 2, 1, 2, 1, 2, 2, 2, 1, 2, 2, 1, 2 },
                { 2, 1, 2, 2, 1, 2, 2, 2, 1, 2, 1, 2, 2, 2, 1, 2, 2, 1, 2 },
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 },
                { 2, 1, 2, 2, 1, 2, 1, 2, 2, 2, 2, 2, 1, 2, 1, 2, 2, 1, 2 },
                { 2, 1, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 1, 2 },
                { 2, 2, 2, 2, 1, 2, 2, 2, 0, 2, 0, 2, 2, 2, 1, 2, 2, 2, 2 },
                { 0, 0, 0, 2, 1, 2, 0, 0, 0, 0, 0, 0, 0, 2, 1, 2, 0, 0, 0 },
                { 2, 2, 2, 2, 1, 2, 0, 2, 2, 0, 2, 2, 0, 2, 1, 2, 2, 2, 2 },
                { 0, 0, 0, 0, 1, 0, 0, 2, 0, 0, 0, 2, 0, 0, 1, 0, 0, 0, 0 },
                { 2, 2, 2, 2, 1, 2, 0, 2, 2, 2, 2, 2, 0, 2, 1, 2, 2, 2, 2 },
                { 0, 0, 0, 2, 1, 2, 0, 0, 0, 0, 0, 0, 0, 2, 1, 2, 0, 0, 0 },
                { 2, 2, 2, 2, 1, 2, 0, 2, 2, 2, 2, 2, 0, 2, 1, 2, 2, 2, 2 },
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2 },
                { 2, 1, 2, 2, 1, 2, 2, 2, 1, 2, 1, 2, 2, 2, 1, 2, 2, 1, 2 },
                { 2, 1, 1, 2, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 2, 1, 1, 2 },
                { 2, 2, 1, 2, 1, 2, 1, 2, 2, 2, 2, 2, 1, 2, 1, 2, 1, 2, 2 },
                { 2, 1, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 1, 2 },
                { 2, 1, 2, 2, 2, 2, 2, 2, 1, 2, 1, 2, 2, 2, 2, 2, 2, 1, 2 },
                { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 },
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            };



            //マップを実際に描画している部分です。縦方向に 15 回、横方向に 20 回ループしています。
            //map[y][x] は現在の位置の要素の値を調べ、それに応じて chip.bmp の中から範囲を決めて描画しています。
            //map[y][x] が 0 の場合は(0, 0) - (32, 32) つまり通路を描画します。
            //map[y][x] が 1 の場合は(32, 0) - (64, 32) つまり壁を描画します。
            //map[y][x] が 2 の場合は(64, 0) - (96, 32) つまりエサを描画します。


            //for (int y = 0; y < 15; y++)
            //{
            //    for (int x = 0; x < 20; x++)
            //    {
            //        elDraw::Layer(x * 32, y * 32, chip, map[y][x] * 32, 0, map[y][x] * 32 + 32, 32);
            //    }
            //}
        }
    }
}



