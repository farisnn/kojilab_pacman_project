using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace kojilan_pacman
{
    public class pacman : character
    {
        //パックマンに固有の情報はこっちに
        public enum status_def { nomal, strong }
        status_def status;



        public pacman()
        {



        }
        /// <summary>
        /// ここにパックマンの移動を定義します。ここをオーバーライドして自前の探索を行ってください
        /// </summary>
        /// <param name="map">マップの二次元配列</param>
        /// <param name="position">パックマンとかの位置？</param>
        /// <returns></returns>
        public override Direction_def move(pacman_map map)
        {

            Point infront_location = map.Pacman_location;//一マス前の座標
            Direction_def return_direction = Direction_def.up;//現時点での返す方向
            Direction_def reverse_direction = Direction_def.up;//進行方向と逆向き


            switch (map.Pacman_direction)//目の前の座標の取得と、進行方向と逆向きの取得
            {
                case pacman_map.map_direction_def.stop:
                case pacman_map.map_direction_def.up:
                    infront_location.Y++;
                    reverse_direction = Direction_def.down;
                    break;
                case pacman_map.map_direction_def.down:
                    infront_location.Y--;
                    reverse_direction = Direction_def.up;
                    break;
                case pacman_map.map_direction_def.right:
                    infront_location.X++;
                    reverse_direction = Direction_def.left;
                    break;
                case pacman_map.map_direction_def.left:
                    infront_location.X--;
                    reverse_direction = Direction_def.right;
                    break;
            }
            //ランダムに移動
            Random random_number = new System.Random();
            bool finish_roop = false;
            while (true)
            {
                // 0 以上 512 未満の乱数を取得する
                int direction_number = random_number.Next(4);
                if (direction_number == 0)
                {
                    return_direction = Direction_def.up;
                }
                else if (direction_number == 1)
                {
                    return_direction = Direction_def.down;
                }
                else if (direction_number == 2)
                {
                    return_direction = Direction_def.right;
                }
                else
                {
                    return_direction = Direction_def.left;
                }

                if (reverse_direction != return_direction)//後退は禁止
                {
                    switch (return_direction)//目の前の座標の取得と、進行方向と逆向きの取得
                    {
                        case Direction_def.up:
                            if (map.get_map_data()[map.Pacman_location.Y - 1][map.Pacman_location.X] != 2)
                            {
                                finish_roop = true;
                            }
                            break;
                        case Direction_def.down:
                            if (map.get_map_data()[map.Pacman_location.Y + 1][map.Pacman_location.X] != 2)
                            {
                                finish_roop = true;
                            }
                            break;
                        case Direction_def.right:
                            if (map.get_map_data()[map.Pacman_location.Y][map.Pacman_location.X + 1] != 2)
                            {
                                finish_roop = true;
                            }
                            break;
                        case Direction_def.left:
                            if (map.get_map_data()[map.Pacman_location.Y][map.Pacman_location.X - 1] != 2)
                            {
                                finish_roop = true;
                            }
                            break;
                    }
                    if (finish_roop == true)
                    {
                        break;
                    }
                }
            }




            return return_direction;
        }




    }
}
