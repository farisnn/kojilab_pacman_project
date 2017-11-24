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
        public enum map_direction_def { up, down, left, right, stop };


        public enum character_name { pacman,enemy1, enemy2, enemy3, enemy4};

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




        public void update_map_data(character.Direction_def pacman_direction, character.Direction_def enemy1_direction, character.Direction_def enemy2_direction, character.Direction_def enemy3_direction, character.Direction_def enemy4_direction)
        {
            //パックマンの更新
            if(check_direction(pacman_location, map_data, pacman_direction))
            {
                this.pacman_direction = convert_direction(pacman_direction);
                move_charactor(character_name.pacman, pacman_location, pacman_direction);
            }
            else
            {
                this.pacman_direction = map_direction_def.stop;

            }
            //enemy1の更新
            if (check_direction(enemy1_location, map_data, enemy1_direction))
            {
                this.enemy1_direction = convert_direction(enemy1_direction);
                move_charactor(character_name.enemy1, enemy1_location, enemy1_direction);
            }
            else
            {
                this.enemy1_direction = map_direction_def.stop;

            }
            //enemy2の更新
            if (check_direction(enemy2_location, map_data, enemy2_direction))
            {
                this.enemy2_direction = convert_direction(enemy2_direction);
                move_charactor(character_name.enemy2, enemy2_location, enemy2_direction);
            }
            else
            {
                this.enemy2_direction = map_direction_def.stop;

            }
            //enemy3の更新
            if (check_direction(enemy3_location, map_data, enemy3_direction))
            {
                this.enemy3_direction = convert_direction(enemy3_direction);
                move_charactor(character_name.enemy3, enemy3_location, enemy3_direction);
            }
            else
            {
                this.enemy3_direction = map_direction_def.stop;

            }
            //enemy4の更新
            if (check_direction(pacman_location, map_data, pacman_direction))
            {
                this.enemy4_direction = convert_direction(enemy4_direction);
                move_charactor(character_name.enemy4, enemy4_location, enemy4_direction);
            }
            else
            {
                this.enemy4_direction = map_direction_def.stop;

            }

            //TODO ここに座標の重なり判定を書く











            //残りターンを減らす。そして0になったらゲームオーバ－フラグをtrue

            rest_turn--;
            if (rest_turn == 0)
                game_over = true;
             
        }


        /// <summary>
        /// その方向に進めるかどうかを判定するメソッド
        /// </summary>
        /// <param name="point">現在の座標</param>
        /// <param name="map"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private bool check_direction(Point point, List<List<int>> map, character.Direction_def direction)
        {
            int next_expect_position_status=9;
            switch(direction) {

                case character.Direction_def.up:

                    next_expect_position_status = map[point.X][point.Y -1];

                    break;
                case character.Direction_def.down:
                    next_expect_position_status = map[point.X][point.Y + 1];
                    break;
                case character.Direction_def.left:
                    next_expect_position_status = map[point.X-1][point.Y];

                    break;
                case character.Direction_def.right:
                    next_expect_position_status = map[point.X+1][point.Y];
                    break;
                default:
                    
                    break;
            }

            if (next_expect_position_status == 2)
            {

                return false;
            }
            else if (next_expect_position_status == 9)
            {
                Console.WriteLine("エラーですよ");
                return false;
            }
            else
            {
                return true;
            }

        }

       

        /// <summary>
        /// 実際にキャラクターの座標を変更するメソッド
        /// </summary>
        /// <param name="name">キャラクター名</param>
        /// <param name="now_position">キャラクターの現在の位置</param>
        /// <param name="direction">キャラクターが指定した移動の方向</param>
        private void move_charactor(character_name name, Point now_position, character.Direction_def direction)
        {
            Point next_point=now_position;


            switch (direction)
            {
                case character.Direction_def.up:
                    next_point.Y--;
                    break;

                case character.Direction_def.down:
                    next_point.Y++;
                    break;
                case character.Direction_def.left:
                    next_point.X--;
                    break;
                case character.Direction_def.right:
                    next_point.X++;
                    break;
                    
            }

            switch (name)
            {
                case character_name.pacman:
                    pacman_location = next_point;
                    break;
                case character_name.enemy1:
                    enemy1_location = next_point;
                    break;
                case character_name.enemy2:
                    enemy2_location = next_point;
                    break;
                case character_name.enemy3:
                    enemy3_location = next_point;
                    break;
                case character_name.enemy4:
                    enemy4_location = next_point;
                    break;
            }
        }


       private map_direction_def convert_direction(character.Direction_def chara_def)
        {
            switch (chara_def)
            {
                case character.Direction_def.up:
                    return map_direction_def.up;
                case character.Direction_def.down:
                    return map_direction_def.down;
                case character.Direction_def.left:
                    return map_direction_def.left;
                case character.Direction_def.right:
                    return map_direction_def.right;
                   
            }
            return map_direction_def.stop;

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
           
            }



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



