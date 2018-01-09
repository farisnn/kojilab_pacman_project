using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace kojilan_pacman
{
    public abstract class character
    {
        //キャラクター全てに共通の要素はこっちに書いておきましょう。

        /// <summary>
        ///         方向をいちいちintで表現するのもアレなので列挙型を使って見ることにしました（仕組み的には配列と同じようなアクセスも出来るみたいです）
        /// </summary>
        public enum Direction_def {up,down,left,right};

        /// <summary>
        ///今自分自身の向いている方向を示す
        /// </summary>
        protected Direction_def current_direction;


        /// <summary>
        /// マップなどを入れて進行方向を変えす関数
        /// </summary>
        /// <returns>次の進行方向</returns>
        public abstract Direction_def move(pacman_map map);
        

            //列挙型をこんな風に使う。（一応こうやって移動方向を指定すれば良いのではないかという例）
            //本当はこのメソッドはキャラによって実装が違うはずなので抽象メソッドにして後からオーバーライドしてもらう。
            //after_direction = Direction.up;

        
        


    }


    /// <summary>
    /// //敵のみに固有な変数・メソッドを書いていく
    /// </summary>
    public abstract class enemy : character
    {
        /// <summary>
        /// 敵の状態を定義
        /// </summary>
        public enum status_def {nomal, ijike, weak}

        /// <summary>
        /// 実際に現在の状態をも辞している変数
        /// </summary>
        protected status_def status;


        
        


    }

    

    /// <summary>
    /// アカベイ的なものの実装
    /// </summary>
    public class enemy1 : enemy
    {
        /// <summary>
        /// return_short_directionで生成するノードのクラス
        /// </summary>
        public class point_info
        {
            public Point current_point;//自分ノード
            public Point parent_point;//親ノード
            private Point enemy1_location;
            private object p;

            public point_info(Point Current_point, Point Parent_point)
            {
                current_point = Current_point;
                parent_point = Parent_point;
            }
        }

        /// <summary>
        /// 自分の座標と目的地とマップを引数として入れると、目的地までの最短経路から次に進むべき方向を返すメソッド
        /// </summary>
        /// <param name="own_point"></param>
        /// <param name="final_destination"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public Direction_def return_short_direction(Point own_point,Point final_destination,pacman_map map)
        {
            //foreachでノードのリストを見ながら追加していく。パックマンの座標と同じところだった場合そこで終了。そこから親ノードをたどってパスを生成。最終的に方向を決める
            Direction_def return_direction = Direction_def.up;//返す方向
            List<point_info> checked_list = new List<point_info>();//行ったことあるノードを保持
            Queue<point_info> search_task = new Queue<point_info>();//次に見る座標リスト            
            

            point_info first_info = new point_info(own_point, own_point);//この親ノードはNULLが入らないからとりあえず入れた                                                                                   
            checked_list.Add(first_info);
            Point check_point = first_info.current_point;//次からデキューしたやつをここに入れて使う感じ

            bool point_exist = false;//goneリストに入っていたらtrueにする。falseであれば、子ノードとして追加。
            while (final_destination != check_point)//上下左右の４通りの座標を子ノードとして追加。パックマンの座標が見つかったら探索終了
            {
                if (map.get_map_data()[check_point.Y + 1][check_point.X] != 2)//壁じゃなければ子ノードとして追加
                {
                    point_exist = false;
                    Point current_point = new Point(check_point.X, check_point.Y+1);//子ノードの自分の座標
                    foreach (point_info i in checked_list)//がぶっているかをチェック。
                    {
                        if (i.current_point == current_point)
                        {
                            point_exist = true;
                        }
                    }
                    if (point_exist == false)
                    {
                        point_info children_point = new point_info(current_point, check_point);
                        search_task.Enqueue(children_point);
                    }

                }
                if (map.get_map_data()[check_point.Y - 1][check_point.X] != 2)
                {
                    point_exist = false;
                    Point current_point = new Point(check_point.X, check_point.Y-1);//子ノードの自分の座標
                    foreach (point_info i in checked_list)//がぶっているかをチェック。
                    {
                        if (i.current_point == current_point)
                        {
                            point_exist = true;
                        }
                    }
                    if (point_exist == false)
                    {
                        point_info children_point = new point_info(current_point, check_point);
                        search_task.Enqueue(children_point);
                    }
                }
                if (map.get_map_data()[check_point.Y][check_point.X + 1] != 2)
                {
                    point_exist = false;
                    Point current_point = new Point(check_point.X+1, check_point.Y );//子ノードの自分の座標
                    foreach (point_info i in checked_list)//がぶっているかをチェック。
                    {
                        if (i.current_point == current_point)
                        {
                            point_exist = true;
                        }
                    }
                    if (point_exist == false)
                    {
                        point_info children_point = new point_info(current_point, check_point);
                        search_task.Enqueue(children_point);
                    }
                }
                if (map.get_map_data()[check_point.Y][check_point.X - 1] != 2)
                {
                    point_exist = false;
                    Point current_point = new Point(check_point.X-1, check_point.Y );//子ノードの自分の座標
                    foreach (point_info i in checked_list)//がぶっているかをチェック。
                    {
                        if (i.current_point == current_point)
                        {
                            point_exist = true;
                        }
                    }
                    if (point_exist == false)
                    {
                        point_info children_point = new point_info(current_point, check_point);
                        search_task.Enqueue(children_point);
                    }
                }
                checked_list.Add(search_task.Dequeue());
                check_point = checked_list[checked_list.Count - 1].current_point;
            }

            //親ノードをたどっていき目的座標までの最短パスを作成
            List<Point> short_path = new List<Point>();//最短パス
            short_path.Add(final_destination);
            check_point = final_destination;//たどっていく座標をここに代入していく
            while (check_point != own_point)
            {
                foreach (point_info i in checked_list)
                {
                    if (i.current_point == check_point)
                    {
                        short_path.Add(i.parent_point);//親ノードを追加
                        check_point = i.parent_point;//次にたどるノードをセット
                    }
                }
            }

            //次に進む座標から、進むべき方向を取得
            Point next_direction = new Point(own_point.X - short_path[short_path.Count - 2].X, own_point.Y - short_path[short_path.Count - 2].Y);//short_pathの末尾が次に進むべき座標
            if (next_direction.Y == 1)
            {
                return_direction = Direction_def.up;
            }
            else if (next_direction.Y < 0)
            {
                return_direction = Direction_def.down;
            }
            else if (next_direction.X == 1)
            {
                return_direction = Direction_def.left;
            }
            else
            {
                return_direction = Direction_def.right;
            }

            return return_direction;
        }




        /// <summary>
        /// enemy1(あかべえ)の移動方向
        /// </summary>
        /// <returns>次の方向</returns>
        public override Direction_def move(pacman_map map)
        {
            Point infront_location = map.Enemy1_location;//一マス前の座標
            Direction_def return_direction = Direction_def.up;//現時点での返す方向
            Direction_def reverse_direction = Direction_def.up;//進行方向と逆向き
            Point own_left=new Point(0,0);//自分から見て左
            Point own_right=new Point(0,0);//自分から見て右
            List<Direction_def> direction_list = new List<Direction_def>();//進行方向の優先順位を決める

            
            switch (map.Enemy1_direction)//目の前の座標の取得と、進行方向と逆向きの取得
            {
                case pacman_map.map_direction_def.stop:
                case pacman_map.map_direction_def.up:
                    infront_location.Y++;
                    reverse_direction = Direction_def.down;
                    own_left = new Point(map.Enemy1_location.X-1,map.Enemy1_location.Y);
                    own_right = new Point(map.Enemy1_location.X+1,map.Enemy1_location.Y);
                    break;
                case pacman_map.map_direction_def.down:
                    infront_location.Y--;
                    reverse_direction = Direction_def.up;
                    own_left = new Point(map.Enemy1_location.X +1, map.Enemy1_location.Y);
                    own_right = new Point(map.Enemy1_location.X - 1, map.Enemy1_location.Y);
                    break;
                case pacman_map.map_direction_def.right:
                    infront_location.X++;
                    reverse_direction = Direction_def.left;
                    own_left = new Point(map.Enemy1_location.X, map.Enemy1_location.Y-1);
                    own_right = new Point(map.Enemy1_location.X, map.Enemy1_location.Y+1);
                    break;
                case pacman_map.map_direction_def.left:
                    infront_location.X--;
                    reverse_direction = Direction_def.right;
                    own_left = new Point(map.Enemy1_location.X , map.Enemy1_location.Y+1);
                    own_right = new Point(map.Enemy1_location.X, map.Enemy1_location.Y-1);
                    break;
            }

            //分岐点にいる場合
            if (map.get_map_data()[own_left.Y][own_left.X] != 2 || map.get_map_data()[own_right.Y][own_right.X] != 2)
            {
                if (this.status == status_def.nomal)//通常時(パックマンの座標を目指す)
                {
                    return return_short_direction(map.Enemy1_location, map.Pacman_location, map);//最短距離を探索
                }



                else if (this.status == status_def.ijike)//いじけ状態のとき
                {
                    if (map.Enemy1_location.Y == 4 && map.Enemy1_location.X == 14)//右上の真ん中を通るとき、あえて直進させて大きめに回らせる
                    {
                        return_direction = current_direction;
                    }
                    else//基本的に右上を徘徊する。決まった目的地はないため、４方向に優先順位を与えるアルゴリズムをとる。
                    {
                        Point destination_distance = new Point(17 - map.Enemy1_location.X, 2 - map.Enemy1_location.Y);//右上の座標（右上を徘徊するために設定した目的地）と自分の距離
                        if (System.Math.Abs(destination_distance.X) > System.Math.Abs(destination_distance.Y))//目的地にｙ軸のほうが近い場合(このときｙ、ｘ、ｙ、ｘ、の順番が優先順位になる)
                        {
                            if (destination_distance.Y > 0)
                            {
                                direction_list.Add(Direction_def.down);
                                if (destination_distance.X > 0)
                                {
                                    direction_list.Add(Direction_def.right);
                                    direction_list.Add(Direction_def.up);
                                    direction_list.Add(Direction_def.left);
                                }
                                else
                                {
                                    direction_list.Add(Direction_def.left);
                                    direction_list.Add(Direction_def.up);
                                    direction_list.Add(Direction_def.right);
                                }
                            }
                            else
                            {
                                direction_list.Add(Direction_def.up);
                                if (destination_distance.X > 0)
                                {
                                    direction_list.Add(Direction_def.right);
                                    direction_list.Add(Direction_def.down);
                                    direction_list.Add(Direction_def.left);
                                }
                                else
                                {
                                    direction_list.Add(Direction_def.left);
                                    direction_list.Add(Direction_def.down);
                                    direction_list.Add(Direction_def.right);
                                }
                            }
                        }
                        else//パックマンにx軸のほうが近い場合
                        {
                            if (destination_distance.X > 0)
                            {
                                direction_list.Add(Direction_def.right);
                                if (destination_distance.Y > 0)
                                {
                                    direction_list.Add(Direction_def.down);
                                    direction_list.Add(Direction_def.left);
                                    direction_list.Add(Direction_def.up);
                                }
                                else
                                {
                                    direction_list.Add(Direction_def.up);
                                    direction_list.Add(Direction_def.left);
                                    direction_list.Add(Direction_def.down);
                                }
                            }
                            else
                            {
                                direction_list.Add(Direction_def.left);
                                if (destination_distance.Y > 0)
                                {
                                    direction_list.Add(Direction_def.up);
                                    direction_list.Add(Direction_def.right);
                                    direction_list.Add(Direction_def.down);
                                }
                                else
                                {
                                    direction_list.Add(Direction_def.down);
                                    direction_list.Add(Direction_def.right);
                                    direction_list.Add(Direction_def.up);
                                }
                            }
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            if (direction_list[i] == reverse_direction)//進行方向と逆向きの候補は削除
                            {
                                direction_list.RemoveAt(i);
                            }
                        }

                        //この時点で、進行方向の逆向き以外の3つの候補の優先順位を保持
                        for (int i = 0; i < 3; i++)//壁ないか優先順位順にみていく、なかったらそれで進行方向を決定
                        {
                            if (direction_list[i] == Direction_def.down)
                            {
                                if (map.get_map_data()[map.Enemy1_location.Y + 1][map.Enemy1_location.X] != 2)
                                {
                                    return_direction = Direction_def.down;
                                    break;
                                }
                            }
                            else if (direction_list[i] == Direction_def.up)
                            {
                                if (map.get_map_data()[map.Enemy1_location.Y - 1][map.Enemy1_location.X] != 2)
                                {
                                    return_direction = Direction_def.up;
                                    break;
                                }
                            }
                            else if (direction_list[i] == Direction_def.right)
                            {
                                if (map.get_map_data()[map.Enemy1_location.Y][map.Enemy1_location.X + 1] != 2)
                                {
                                    return_direction = Direction_def.right;
                                    break;
                                }
                            }
                            else if (direction_list[i] == Direction_def.left)
                            {
                                if (map.get_map_data()[map.Enemy1_location.Y][map.Enemy1_location.X - 1] != 2)
                                {
                                    return_direction = Direction_def.left;
                                    break;
                                }
                            }

                        }
                    }
                }



                else//弱体化中
                {
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
                        else if (direction_number == 1)
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
                                    if (map.get_map_data()[map.Enemy1_location.Y - 1][map.Enemy1_location.X] != 2)
                                    {
                                        finish_roop = true;
                                    }
                                    break;
                                case Direction_def.down:
                                    if (map.get_map_data()[map.Enemy1_location.Y + 1][map.Enemy1_location.X] != 2)
                                    {
                                        finish_roop = true;
                                    }
                                    break;
                                case Direction_def.right:
                                    if (map.get_map_data()[map.Enemy1_location.Y][map.Enemy1_location.X + 1] != 2)
                                    {
                                        finish_roop = true;
                                    }
                                    break;
                                case Direction_def.left:
                                    if (map.get_map_data()[map.Enemy1_location.Y][map.Enemy1_location.X - 1] != 2)
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

                }
            }
            else//曲がれるとこがない場合
            {

                switch (map.Enemy1_direction)//目の前の座標の取得と、進行方向と逆向きの取得
                {
                    case pacman_map.map_direction_def.stop:
                    case pacman_map.map_direction_def.up:
                        return_direction = Direction_def.up;
                        break;
                    case pacman_map.map_direction_def.down:
                        return_direction = Direction_def.down;
                        break;
                    case pacman_map.map_direction_def.right:
                        return_direction = Direction_def.right;
                        break;
                    case pacman_map.map_direction_def.left:
                        return_direction = Direction_def.left;
                        break;
                }
            }
        
            return return_direction;//返り値
        }
    }

    public class enemy2 : enemy
    {


        /// <summary>
        /// return_short_directionで生成するノードのクラス
        /// </summary>
        public class point_info
        {
            public Point current_point;//自分ノード
            public Point parent_point;//親ノード
            private Point enemy1_location;
            private object p;

            public point_info(Point Current_point, Point Parent_point)
            {
                current_point = Current_point;
                parent_point = Parent_point;
            }
        }


        /// <summary>
        /// 自分の座標と目的地とマップを引数として入れると、目的地までの最短経路から次に進むべき方向を返すメソッド
        /// </summary>
        /// <param name="own_point"></param>
        /// <param name="final_destination"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public Direction_def return_short_direction(Point own_point, Point final_destination, pacman_map map)
        {
            //foreachでノードのリストを見ながら追加していく。パックマンの座標と同じところだった場合そこで終了。そこから親ノードをたどってパスを生成。最終的に方向を決める
            Direction_def return_direction = Direction_def.up;//返す方向
            List<point_info> checked_list = new List<point_info>();//行ったことあるノードを保持
            Queue<point_info> search_task = new Queue<point_info>();//次に見る座標リスト            


            point_info first_info = new point_info(own_point, own_point);//この親ノードはNULLが入らないからとりあえず入れた                                                                                   
            checked_list.Add(first_info);
            Point check_point = first_info.current_point;//次からデキューしたやつをここに入れて使う感じ

            bool point_exist = false;//goneリストに入っていたらtrueにする。falseであれば、子ノードとして追加。
            while (final_destination != check_point)//上下左右の４通りの座標を子ノードとして追加。パックマンの座標が見つかったら探索終了
            {
                if (map.get_map_data()[check_point.Y + 1][check_point.X] != 2)//壁じゃなければ子ノードとして追加
                {
                    point_exist = false;
                    Point current_point = new Point(check_point.X, check_point.Y + 1);//子ノードの自分の座標
                    foreach (point_info i in checked_list)//がぶっているかをチェック。
                    {
                        if (i.current_point == current_point)
                        {
                            point_exist = true;
                        }
                    }
                    if (point_exist == false)
                    {
                        point_info children_point = new point_info(current_point, check_point);
                        search_task.Enqueue(children_point);
                    }

                }
                if (map.get_map_data()[check_point.Y - 1][check_point.X] != 2)
                {
                    point_exist = false;
                    Point current_point = new Point(check_point.X, check_point.Y - 1);//子ノードの自分の座標
                    foreach (point_info i in checked_list)//がぶっているかをチェック。
                    {
                        if (i.current_point == current_point)
                        {
                            point_exist = true;
                        }
                    }
                    if (point_exist == false)
                    {
                        point_info children_point = new point_info(current_point, check_point);
                        search_task.Enqueue(children_point);
                    }
                }
                if (map.get_map_data()[check_point.Y][check_point.X + 1] != 2)
                {
                    point_exist = false;
                    Point current_point = new Point(check_point.X + 1, check_point.Y);//子ノードの自分の座標
                    foreach (point_info i in checked_list)//がぶっているかをチェック。
                    {
                        if (i.current_point == current_point)
                        {
                            point_exist = true;
                        }
                    }
                    if (point_exist == false)
                    {
                        point_info children_point = new point_info(current_point, check_point);
                        search_task.Enqueue(children_point);
                    }
                }
                if (map.get_map_data()[check_point.Y][check_point.X - 1] != 2)
                {
                    point_exist = false;
                    Point current_point = new Point(check_point.X - 1, check_point.Y);//子ノードの自分の座標
                    foreach (point_info i in checked_list)//がぶっているかをチェック。
                    {
                        if (i.current_point == current_point)
                        {
                            point_exist = true;
                        }
                    }
                    if (point_exist == false)
                    {
                        point_info children_point = new point_info(current_point, check_point);
                        search_task.Enqueue(children_point);
                    }
                }
                checked_list.Add(search_task.Dequeue());
                check_point = checked_list[checked_list.Count - 1].current_point;
            }

            //親ノードをたどっていき目的座標までの最短パスを作成
            List<Point> short_path = new List<Point>();//最短パス
            short_path.Add(final_destination);
            check_point = final_destination;//たどっていく座標をここに代入していく
            while (check_point != own_point)
            {
                foreach (point_info i in checked_list)
                {
                    if (i.current_point == check_point)
                    {
                        short_path.Add(i.parent_point);//親ノードを追加
                        check_point = i.parent_point;//次にたどるノードをセット
                    }
                }
            }

            //次に進む座標から、進むべき方向を取得
            Point next_direction = new Point(own_point.X - short_path[short_path.Count - 2].X, own_point.Y - short_path[short_path.Count - 2].Y);//short_pathの末尾が次に進むべき座標
            if (next_direction.Y == 1)
            {
                return_direction = Direction_def.up;
            }
            else if (next_direction.Y < 0)
            {
                return_direction = Direction_def.down;
            }
            else if (next_direction.X == 1)
            {
                return_direction = Direction_def.left;
            }
            else
            {
                return_direction = Direction_def.right;
            }

            return return_direction;
        }



        /// <summary>
        /// enemy2の移動方向
        ///探索してみる？
        /// </summary>
        /// <returns>次の方向</returns>
        public override Direction_def move(pacman_map map)
        {
            Point next_pos = new Point(0, 0);
            bool loop =true;
            Direction_def result = Direction_def.up;

            Point infront_location = map.Enemy2_location;//一マス前の座標
            Direction_def return_direction = Direction_def.up;//現時点での返す方向
            Direction_def reverse_direction = Direction_def.up;//進行方向と逆向き
            Point own_left = new Point(0, 0);//自分から見て左
            Point own_right = new Point(0, 0);//自分から見て右
            List<Direction_def> direction_list = new List<Direction_def>();//進行方向の優先順位を決める


            switch (map.Enemy2_direction)//目の前の座標の取得と、進行方向と逆向きの取得
            {
                case pacman_map.map_direction_def.stop:
                case pacman_map.map_direction_def.up:
                    infront_location.Y++;
                    reverse_direction = Direction_def.down;
                    own_left = new Point(map.Enemy2_location.X - 1, map.Enemy2_location.Y);
                    own_right = new Point(map.Enemy2_location.X + 1, map.Enemy2_location.Y);
                    break;
                case pacman_map.map_direction_def.down:
                    infront_location.Y--;
                    reverse_direction = Direction_def.up;
                    own_left = new Point(map.Enemy2_location.X + 1, map.Enemy2_location.Y);
                    own_right = new Point(map.Enemy2_location.X - 1, map.Enemy2_location.Y);
                    break;
                case pacman_map.map_direction_def.right:
                    infront_location.X++;
                    reverse_direction = Direction_def.left;
                    own_left = new Point(map.Enemy2_location.X, map.Enemy2_location.Y - 1);
                    own_right = new Point(map.Enemy2_location.X, map.Enemy2_location.Y + 1);
                    break;
                case pacman_map.map_direction_def.left:
                    infront_location.X--;
                    reverse_direction = Direction_def.right;
                    own_left = new Point(map.Enemy2_location.X, map.Enemy2_location.Y + 1);
                    own_right = new Point(map.Enemy2_location.X, map.Enemy2_location.Y - 1);
                    break;
            }

            //分岐点にいる場合
            if (map.get_map_data()[own_left.Y][own_left.X] != 2 || map.get_map_data()[own_right.Y][own_right.X] != 2)
            {
                if (this.status == status_def.nomal)//通常時(パックマンの座標を目指す)
                {


                    //本来目指すべき座標（enemy1のパックマンに対する点対称）
                    next_pos.X = 2 * map.Pacman_location.X - map.Enemy1_location.X;
                    next_pos.Y = 2 * map.Pacman_location.Y - map.Enemy1_location.Y;
                    Point origin_next = next_pos;
                    int loop_count = 1;

                    while (loop)
                    {
                        if (next_pos.X > map.get_map_data()[0].Count - 1)
                        {

                            double rate = next_pos.X / (map.get_map_data()[0].Count - 1);
                            next_pos.X = (int)(next_pos.X/rate);
                            next_pos.Y = (int)(next_pos.Y / rate);
                            
                            //next_pos.X = map.get_map_data()[0].Count - 2;

                        }
                        if (next_pos.Y > map.get_map_data().Count - 1)
                        {
                            double rate = next_pos.Y / (map.get_map_data().Count - 1);
                            next_pos.X = (int)(next_pos.X / rate);
                            next_pos.Y = (int)(next_pos.Y / rate);

                        }
                        if (next_pos.Y < 1)
                        {
                            next_pos.Y = 1;

                        }

                        if (next_pos.X < 1)
                        {
                            next_pos.X = 1;

                        }


                        try
                        {
                            loop = false;
                            result = return_short_direction(map.Enemy2_location, next_pos, map);

                        }
                        catch
                        {
                            //if(loop_count == 1)
                            //{
                            //    next_pos.X--;
                            //    next_pos.Y--;

                            //}
                            //else if(loop_count % 3 == 0)
                            //{
                            //    next_pos.X = next_pos.X - 2;
                            //    next_pos.Y++;
                                

                            //}
                            //else
                            //{
                            //    next_pos.X++;

                            //}




                            

                            loop_count++;




                            //Random r = new Random();
                            //int t = r.Next(4);
                            //if (t == 0)
                            //{
                            //    next_pos.X = 1;
                            //    next_pos.Y = 1;
                            //}
                            //else if (t == 1)
                            //{
                            //    next_pos.X = 1;
                            //    next_pos.Y = 20;
                            //}

                            //else if (t == 2)
                            //{
                            //    next_pos.X = 17;
                            //    next_pos.Y = 1;
                            //}
                            //else
                            //{
                            //    next_pos.X = 17;
                            //    next_pos.Y = 20;
                            //}



                            if (map.Enemy2_location.X == 17 && map.Enemy2_location.Y == 20)
                            {
                                next_pos.X = 10;
                                next_pos.Y = 14;
                            }
                            else
                            {
                                next_pos.X = 17;
                                next_pos.Y = 20;
                            }
                            loop = true;

                        }
                    }
                    return result;
                    

                }



                else if (this.status == status_def.ijike)//いじけ状態のとき
                {
                    //if (map.Enemy2_location.Y == 4 && map.Enemy1_location.X == 14)//右上の真ん中を通るとき、あえて直進させて大きめに回らせる
                    //{
                    //    return_direction = current_direction;
                    //}
                    //else//基本的に右上を徘徊する。決まった目的地はないため、４方向に優先順位を与えるアルゴリズムをとる。
                    {
                        Point destination_distance = new Point(17 - map.Enemy1_location.X, 20 - map.Enemy1_location.Y);//右上の座標（右上を徘徊するために設定した目的地）と自分の距離
                        if (System.Math.Abs(destination_distance.X) > System.Math.Abs(destination_distance.Y))//目的地にｙ軸のほうが近い場合(このときｙ、ｘ、ｙ、ｘ、の順番が優先順位になる)
                        {
                            if (destination_distance.Y > 0)
                            {
                                direction_list.Add(Direction_def.down);
                                if (destination_distance.X > 0)
                                {
                                    direction_list.Add(Direction_def.right);
                                    direction_list.Add(Direction_def.up);
                                    direction_list.Add(Direction_def.left);
                                }
                                else
                                {
                                    direction_list.Add(Direction_def.left);
                                    direction_list.Add(Direction_def.up);
                                    direction_list.Add(Direction_def.right);
                                }
                            }
                            else
                            {
                                direction_list.Add(Direction_def.up);
                                if (destination_distance.X > 0)
                                {
                                    direction_list.Add(Direction_def.right);
                                    direction_list.Add(Direction_def.down);
                                    direction_list.Add(Direction_def.left);
                                }
                                else
                                {
                                    direction_list.Add(Direction_def.left);
                                    direction_list.Add(Direction_def.down);
                                    direction_list.Add(Direction_def.right);
                                }
                            }
                        }
                        else//パックマンにx軸のほうが近い場合
                        {
                            if (destination_distance.X > 0)
                            {
                                direction_list.Add(Direction_def.right);
                                if (destination_distance.Y > 0)
                                {
                                    direction_list.Add(Direction_def.down);
                                    direction_list.Add(Direction_def.left);
                                    direction_list.Add(Direction_def.up);
                                }
                                else
                                {
                                    direction_list.Add(Direction_def.up);
                                    direction_list.Add(Direction_def.left);
                                    direction_list.Add(Direction_def.down);
                                }
                            }
                            else
                            {
                                direction_list.Add(Direction_def.left);
                                if (destination_distance.Y > 0)
                                {
                                    direction_list.Add(Direction_def.up);
                                    direction_list.Add(Direction_def.right);
                                    direction_list.Add(Direction_def.down);
                                }
                                else
                                {
                                    direction_list.Add(Direction_def.down);
                                    direction_list.Add(Direction_def.right);
                                    direction_list.Add(Direction_def.up);
                                }
                            }
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            if (direction_list[i] == reverse_direction)//進行方向と逆向きの候補は削除
                            {
                                direction_list.RemoveAt(i);
                            }
                        }

                        //この時点で、進行方向の逆向き以外の3つの候補の優先順位を保持
                        for (int i = 0; i < 3; i++)//壁ないか優先順位順にみていく、なかったらそれで進行方向を決定
                        {
                            if (direction_list[i] == Direction_def.down)
                            {
                                if (map.get_map_data()[map.Enemy2_location.Y + 1][map.Enemy2_location.X] != 2)
                                {
                                    return_direction = Direction_def.down;
                                    break;
                                }
                            }
                            else if (direction_list[i] == Direction_def.up)
                            {
                                if (map.get_map_data()[map.Enemy2_location.Y - 1][map.Enemy2_location.X] != 2)
                                {
                                    return_direction = Direction_def.up;
                                    break;
                                }
                            }
                            else if (direction_list[i] == Direction_def.right)
                            {
                                if (map.get_map_data()[map.Enemy2_location.Y][map.Enemy2_location.X + 1] != 2)
                                {
                                    return_direction = Direction_def.right;
                                    break;
                                }
                            }
                            else if (direction_list[i] == Direction_def.left)
                            {
                                if (map.get_map_data()[map.Enemy2_location.Y][map.Enemy2_location.X - 1] != 2)
                                {
                                    return_direction = Direction_def.left;
                                    break;
                                }
                            }

                        }
                    }
                }



                else//弱体化中
                {
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
                        else if (direction_number == 1)
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
                                    if (map.get_map_data()[map.Enemy2_location.Y - 1][map.Enemy2_location.X] != 2)
                                    {
                                        finish_roop = true;
                                    }
                                    break;
                                case Direction_def.down:
                                    if (map.get_map_data()[map.Enemy2_location.Y + 1][map.Enemy2_location.X] != 2)
                                    {
                                        finish_roop = true;
                                    }
                                    break;
                                case Direction_def.right:
                                    if (map.get_map_data()[map.Enemy2_location.Y][map.Enemy2_location.X + 1] != 2)
                                    {
                                        finish_roop = true;
                                    }
                                    break;
                                case Direction_def.left:
                                    if (map.get_map_data()[map.Enemy2_location.Y][map.Enemy2_location.X - 1] != 2)
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

                }
            }
            else//曲がれるとこがない場合
            {

                switch (map.Enemy2_direction)//目の前の座標の取得と、進行方向と逆向きの取得
                {
                    case pacman_map.map_direction_def.stop:
                    case pacman_map.map_direction_def.up:
                        return_direction = Direction_def.up;
                        break;
                    case pacman_map.map_direction_def.down:
                        return_direction = Direction_def.down;
                        break;
                    case pacman_map.map_direction_def.right:
                        return_direction = Direction_def.right;
                        break;
                    case pacman_map.map_direction_def.left:
                        return_direction = Direction_def.left;
                        break;
                }
            }
            return return_direction;
        }

    }


    public class enemy3 : enemy
    {
        /// <summary>
        /// return_short_directionで生成するノードのクラス
        /// </summary>
        public class point_info
        {
            public Point current_point;//自分ノード
            public Point parent_point;//親ノード
            private Point enemy1_location;
            private object p;

            public point_info(Point Current_point, Point Parent_point)
            {
                current_point = Current_point;
                parent_point = Parent_point;
            }
        }

        /// <summary>
        /// 自分の座標と目的地とマップを引数として入れると、目的地までの最短経路から次に進むべき方向を返すメソッド
        /// </summary>
        /// <param name="own_point"></param>
        /// <param name="final_destination"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public Direction_def return_short_direction(Point own_point, Point final_destination, pacman_map map)
        {
            //foreachでノードのリストを見ながら追加していく。パックマンの座標と同じところだった場合そこで終了。そこから親ノードをたどってパスを生成。最終的に方向を決める
            Direction_def return_direction = Direction_def.up;//返す方向
            List<point_info> checked_list = new List<point_info>();//行ったことあるノードを保持
            Queue<point_info> search_task = new Queue<point_info>();//次に見る座標リスト            


            point_info first_info = new point_info(own_point, own_point);//この親ノードはNULLが入らないからとりあえず入れた                                                                                   
            checked_list.Add(first_info);
            Point check_point = first_info.current_point;//次からデキューしたやつをここに入れて使う感じ

            bool point_exist = false;//goneリストに入っていたらtrueにする。falseであれば、子ノードとして追加。
            while (final_destination != check_point)//上下左右の４通りの座標を子ノードとして追加。パックマンの座標が見つかったら探索終了
            {
                if (map.get_map_data()[check_point.Y + 1][check_point.X] != 2)//壁じゃなければ子ノードとして追加
                {
                    point_exist = false;
                    Point current_point = new Point(check_point.X, check_point.Y + 1);//子ノードの自分の座標
                    foreach (point_info i in checked_list)//がぶっているかをチェック。
                    {
                        if (i.current_point == current_point)
                        {
                            point_exist = true;
                        }
                    }
                    if (point_exist == false)
                    {
                        point_info children_point = new point_info(current_point, check_point);
                        search_task.Enqueue(children_point);
                    }

                }
                if (map.get_map_data()[check_point.Y - 1][check_point.X] != 2)
                {
                    point_exist = false;
                    Point current_point = new Point(check_point.X, check_point.Y - 1);//子ノードの自分の座標
                    foreach (point_info i in checked_list)//がぶっているかをチェック。
                    {
                        if (i.current_point == current_point)
                        {
                            point_exist = true;
                        }
                    }
                    if (point_exist == false)
                    {
                        point_info children_point = new point_info(current_point, check_point);
                        search_task.Enqueue(children_point);
                    }
                }
                if (map.get_map_data()[check_point.Y][check_point.X + 1] != 2)
                {
                    point_exist = false;
                    Point current_point = new Point(check_point.X + 1, check_point.Y);//子ノードの自分の座標
                    foreach (point_info i in checked_list)//がぶっているかをチェック。
                    {
                        if (i.current_point == current_point)
                        {
                            point_exist = true;
                        }
                    }
                    if (point_exist == false)
                    {
                        point_info children_point = new point_info(current_point, check_point);
                        search_task.Enqueue(children_point);
                    }
                }
                if (map.get_map_data()[check_point.Y][check_point.X - 1] != 2)
                {
                    point_exist = false;
                    Point current_point = new Point(check_point.X - 1, check_point.Y);//子ノードの自分の座標
                    foreach (point_info i in checked_list)//がぶっているかをチェック。
                    {
                        if (i.current_point == current_point)
                        {
                            point_exist = true;
                        }
                    }
                    if (point_exist == false)
                    {
                        point_info children_point = new point_info(current_point, check_point);
                        search_task.Enqueue(children_point);
                    }
                }
                checked_list.Add(search_task.Dequeue());
                check_point = checked_list[checked_list.Count - 1].current_point;
            }

            //親ノードをたどっていき目的座標までの最短パスを作成
            List<Point> short_path = new List<Point>();//最短パス
            short_path.Add(final_destination);
            check_point = final_destination;//たどっていく座標をここに代入していく
            while (check_point != own_point)
            {
                foreach (point_info i in checked_list)
                {
                    if (i.current_point == check_point)
                    {
                        short_path.Add(i.parent_point);//親ノードを追加
                        check_point = i.parent_point;//次にたどるノードをセット
                    }
                }
            }

            //次に進む座標から、進むべき方向を取得
            Point next_direction = new Point(own_point.X - short_path[short_path.Count - 2].X, own_point.Y - short_path[short_path.Count - 2].Y);//short_pathの末尾が次に進むべき座標
            if (next_direction.Y == 1)
            {
                return_direction = Direction_def.up;
            }
            else if (next_direction.Y < 0)
            {
                return_direction = Direction_def.down;
            }
            else if (next_direction.X == 1)
            {
                return_direction = Direction_def.left;
            }
            else
            {
                return_direction = Direction_def.right;
            }

            return return_direction;
        }




        /// <summary>
        /// enemy3の移動方向
        /// </summary>
        /// <returns>次の方向</returns>
        public override Direction_def move(pacman_map map)
        {
            Point next_pos3 = new Point(0, 0);
            bool loop = true;
            Direction_def result = Direction_def.up;

            Point infront_location = map.Enemy3_location;//一マス前の座標
            Direction_def return_direction = Direction_def.up;//現時点での返す方向
            Direction_def reverse_direction = Direction_def.up;//進行方向と逆向き
            Point own_left = new Point(0, 0);//自分から見て左
            Point own_right = new Point(0, 0);//自分から見て右
            List<Direction_def> direction_list = new List<Direction_def>();//進行方向の優先順位を決める


            switch (map.Enemy3_direction)//目の前の座標の取得と、進行方向と逆向きの取得
            {
                case pacman_map.map_direction_def.stop:
                case pacman_map.map_direction_def.up:
                    infront_location.Y++;
                    reverse_direction = Direction_def.down;
                    own_left = new Point(map.Enemy3_location.X - 1, map.Enemy3_location.Y);
                    own_right = new Point(map.Enemy3_location.X + 1, map.Enemy3_location.Y);
                    break;
                case pacman_map.map_direction_def.down:
                    infront_location.Y--;
                    reverse_direction = Direction_def.up;
                    own_left = new Point(map.Enemy3_location.X + 1, map.Enemy3_location.Y);
                    own_right = new Point(map.Enemy3_location.X - 1, map.Enemy3_location.Y);
                    break;
                case pacman_map.map_direction_def.right:
                    infront_location.X++;
                    reverse_direction = Direction_def.left;
                    own_left = new Point(map.Enemy3_location.X, map.Enemy3_location.Y - 1);
                    own_right = new Point(map.Enemy3_location.X, map.Enemy3_location.Y + 1);
                    break;
                case pacman_map.map_direction_def.left:
                    infront_location.X--;
                    reverse_direction = Direction_def.right;
                    own_left = new Point(map.Enemy3_location.X, map.Enemy3_location.Y + 1);
                    own_right = new Point(map.Enemy3_location.X, map.Enemy3_location.Y - 1);
                    break;
            }

            //分岐点にいる場合
            if (map.get_map_data()[own_left.Y][own_left.X] != 2 || map.get_map_data()[own_right.Y][own_right.X] != 2)
            {
                if (this.status == status_def.nomal)//通常時(パックマンの座標を目指す)
                {
                    next_pos3.X = map.Pacman_location.X;//現在のパックマンの座標
                    next_pos3.Y = map.Pacman_location.Y;//現在のパックマンの座標
                    
                    //ここから↓　パックマンの三手先の座標予想
                    if (map.Pacman_direction== pacman_map.map_direction_def.down)
                    {
                        for (int t = 0; t < 3; t++)
                        {
                            if (map.get_map_data()[next_pos3.Y][next_pos3.X - 1] != 2 )//分岐点の時
                            {
                                if (map.get_map_data()[next_pos3.Y][next_pos3.X + 1] != 2)//左右空
                                {
                                    Random r = new Random();
                                    int t1 = r.Next(1);
                                    if (t1==0)
                                    {
                                        next_pos3.X-=1;//左へ
                                    }
                                    else
                                    {
                                        next_pos3.X += 1;//右へ
                                    }
                                }
                                else//左空
                                {
                                    next_pos3.X -= 1;//左へ
                                }
                            }
                            else if (map.get_map_data()[next_pos3.Y][next_pos3.X + 1] != 2)//右空
                            {
                                next_pos3.X += 1;//右へ
                            }
                            else//直進時
                            {
                                next_pos3.Y += 1;
                            }
                        }
                    }

                    else if (map.Pacman_direction == pacman_map.map_direction_def.up)
                    {
                        for (int t = 0; t < 3; t++)
                        {
                            if (map.get_map_data()[next_pos3.Y][next_pos3.X - 1] != 2)//分岐点の時
                            {
                                if (map.get_map_data()[next_pos3.Y][next_pos3.X + 1] != 2)//左右空
                                {
                                    Random r = new Random();
                                    int t1 = r.Next(1);
                                    if (t1 == 0)
                                    {
                                        next_pos3.X -= 1;//左へ
                                    }
                                    else
                                    {
                                        next_pos3.X += 1;//右へ
                                    }
                                }
                                else//左空
                                {
                                    next_pos3.X -= 1;//左へ
                                }
                            }
                            else if (map.get_map_data()[next_pos3.Y][next_pos3.X + 1] != 2)//右空
                            {
                                next_pos3.X += 1;//右へ
                            }
                            else//直進時
                            {
                                next_pos3.Y -= 1;
                            }
                        }
                     
                    }

                    else if (map.Pacman_direction == pacman_map.map_direction_def.right)
                    {
                        for (int t = 0; t < 3; t++)
                        {
                            if (map.get_map_data()[next_pos3.Y + 1][next_pos3.X] != 2)//分岐点の時
                            {
                                if (map.get_map_data()[next_pos3.Y - 1][next_pos3.X] != 2)//上下空
                                {
                                    Random r = new Random();
                                    int t1 = r.Next(1);
                                    if (t1 == 0)
                                    {
                                        next_pos3.Y -= 1;//上へ
                                    }
                                    else
                                    {
                                        next_pos3.Y += 1;//下へ
                                    }
                                }
                                else//下空
                                {
                                    next_pos3.Y += 1;//下へ
                                }
                            }
                            else if (map.get_map_data()[next_pos3.Y - 1][next_pos3.X ] != 2)//上空
                            {
                                next_pos3.Y -= 1;//上へ
                            }
                            else//直進時
                            {
                                next_pos3.X+= 1;
                            }
                        }
                    }

         
                    else if (map.Pacman_direction == pacman_map.map_direction_def.left)
                    {
                        for (int t = 0; t < 3; t++)
                        {
                            if (map.get_map_data()[next_pos3.Y + 1][next_pos3.X] != 2)//分岐点の時
                            {
                                if (map.get_map_data()[next_pos3.Y - 1][next_pos3.X] != 2)//上下空
                                {
                                    Random r = new Random();
                                    int t1 = r.Next(1);
                                    if (t1 == 0)
                                    {
                                        next_pos3.Y -= 1;//上へ
                                    }
                                    else
                                    {
                                        next_pos3.Y += 1;//下へ
                                    }
                                }
                                else//上空
                                {
                                    next_pos3.Y += 1;//下へ
                                }
                            }
                            else if (map.get_map_data()[next_pos3.Y - 1][next_pos3.X] != 2)//上空
                            {
                                next_pos3.Y -= 1;//へ
                            }
                            else//直進時
                            {
                                next_pos3.X -= 1;
                            }
                        }
                    }

                    else
                    {

                    }


                    try
                    {
                        loop = false;
                        result = return_short_direction(map.Enemy3_location, next_pos3, map);

                    }
                    catch
                    {
                        Random r = new Random();
                        int t = r.Next(4);
                        if (t == 0)
                            next_pos3.Y--;
                        else if (t == 1)
                            next_pos3.Y++;
                        else if (t == 2)
                            next_pos3.X--;
                        else
                            next_pos3.X++;

                        loop = true;

                    }
                    return result;
                  //  return result = return_short_direction(map.Enemy3_location, next_pos3, map);


                }



                else if (this.status == status_def.ijike)//いじけ状態のとき
                {
                    if (map.Enemy3_location.Y == 4 && map.Enemy3_location.X == 14)//右上の真ん中を通るとき、あえて直進させて大きめに回らせる
                    {
                        return_direction = current_direction;
                    }
                    else//基本的に右上を徘徊する。決まった目的地はないため、４方向に優先順位を与えるアルゴリズムをとる。
                    {
                        Point destination_distance = new Point(17 - map.Enemy3_location.X, 2 - map.Enemy3_location.Y);//右上の座標（右上を徘徊するために設定した目的地）と自分の距離
                        if (System.Math.Abs(destination_distance.X) > System.Math.Abs(destination_distance.Y))//目的地にｙ軸のほうが近い場合(このときｙ、ｘ、ｙ、ｘ、の順番が優先順位になる)
                        {
                            if (destination_distance.Y > 0)
                            {
                                direction_list.Add(Direction_def.down);
                                if (destination_distance.X > 0)
                                {
                                    direction_list.Add(Direction_def.right);
                                    direction_list.Add(Direction_def.up);
                                    direction_list.Add(Direction_def.left);
                                }
                                else
                                {
                                    direction_list.Add(Direction_def.left);
                                    direction_list.Add(Direction_def.up);
                                    direction_list.Add(Direction_def.right);
                                }
                            }
                            else
                            {
                                direction_list.Add(Direction_def.up);
                                if (destination_distance.X > 0)
                                {
                                    direction_list.Add(Direction_def.right);
                                    direction_list.Add(Direction_def.down);
                                    direction_list.Add(Direction_def.left);
                                }
                                else
                                {
                                    direction_list.Add(Direction_def.left);
                                    direction_list.Add(Direction_def.down);
                                    direction_list.Add(Direction_def.right);
                                }
                            }
                        }
                        else//パックマンにx軸のほうが近い場合
                        {
                            if (destination_distance.X > 0)
                            {
                                direction_list.Add(Direction_def.right);
                                if (destination_distance.Y > 0)
                                {
                                    direction_list.Add(Direction_def.down);
                                    direction_list.Add(Direction_def.left);
                                    direction_list.Add(Direction_def.up);
                                }
                                else
                                {
                                    direction_list.Add(Direction_def.up);
                                    direction_list.Add(Direction_def.left);
                                    direction_list.Add(Direction_def.down);
                                }
                            }
                            else
                            {
                                direction_list.Add(Direction_def.left);
                                if (destination_distance.Y > 0)
                                {
                                    direction_list.Add(Direction_def.up);
                                    direction_list.Add(Direction_def.right);
                                    direction_list.Add(Direction_def.down);
                                }
                                else
                                {
                                    direction_list.Add(Direction_def.down);
                                    direction_list.Add(Direction_def.right);
                                    direction_list.Add(Direction_def.up);
                                }
                            }
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            if (direction_list[i] == reverse_direction)//進行方向と逆向きの候補は削除
                            {
                                direction_list.RemoveAt(i);
                            }
                        }

                        //この時点で、進行方向の逆向き以外の3つの候補の優先順位を保持
                        for (int i = 0; i < 3; i++)//壁ないか優先順位順にみていく、なかったらそれで進行方向を決定
                        {
                            if (direction_list[i] == Direction_def.down)
                            {
                                if (map.get_map_data()[map.Enemy3_location.Y + 1][map.Enemy3_location.X] != 2)
                                {
                                    return_direction = Direction_def.down;
                                    break;
                                }
                            }
                            else if (direction_list[i] == Direction_def.up)
                            {
                                if (map.get_map_data()[map.Enemy3_location.Y - 1][map.Enemy3_location.X] != 2)
                                {
                                    return_direction = Direction_def.up;
                                    break;
                                }
                            }
                            else if (direction_list[i] == Direction_def.right)
                            {
                                if (map.get_map_data()[map.Enemy3_location.Y][map.Enemy3_location.X + 1] != 2)
                                {
                                    return_direction = Direction_def.right;
                                    break;
                                }
                            }
                            else if (direction_list[i] == Direction_def.left)
                            {
                                if (map.get_map_data()[map.Enemy3_location.Y][map.Enemy3_location.X - 1] != 2)
                                {
                                    return_direction = Direction_def.left;
                                    break;
                                }
                            }

                        }
                    }
                }



                else//弱体化中
                {
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
                        else if (direction_number == 1)
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
                                    if (map.get_map_data()[map.Enemy3_location.Y - 1][map.Enemy3_location.X] != 2)
                                    {
                                        finish_roop = true;
                                    }
                                    break;
                                case Direction_def.down:
                                    if (map.get_map_data()[map.Enemy3_location.Y + 1][map.Enemy3_location.X] != 2)
                                    {
                                        finish_roop = true;
                                    }
                                    break;
                                case Direction_def.right:
                                    if (map.get_map_data()[map.Enemy3_location.Y][map.Enemy3_location.X + 1] != 2)
                                    {
                                        finish_roop = true;
                                    }
                                    break;
                                case Direction_def.left:
                                    if (map.get_map_data()[map.Enemy3_location.Y][map.Enemy3_location.X - 1] != 2)
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

                }
            }
            else//曲がれるとこがない場合
            {

                switch (map.Enemy1_direction)//目の前の座標の取得と、進行方向と逆向きの取得
                {
                    case pacman_map.map_direction_def.stop:
                    case pacman_map.map_direction_def.up:
                        return_direction = Direction_def.up;
                        break;
                    case pacman_map.map_direction_def.down:
                        return_direction = Direction_def.down;
                        break;
                    case pacman_map.map_direction_def.right:
                        return_direction = Direction_def.right;
                        break;
                    case pacman_map.map_direction_def.left:
                        return_direction = Direction_def.left;
                        break;
                }
            }

            return return_direction;//返り値
        }
    }




    public class enemy4 : enemy
    {
        /// <summary>
        /// return_short_directionで生成するノードのクラス
        /// </summary>
        public class point_info
        {
            public Point current_point;//自分ノード
            public Point parent_point;//親ノード
            private Point enemy1_location;
            private object p;

            public point_info(Point Current_point, Point Parent_point)
            {
                current_point = Current_point;
                parent_point = Parent_point;
            }
        }


        /// <summary>
        /// 自分の座標と目的地とマップを引数として入れると、目的地までの最短経路から次に進むべき方向を返すメソッド
        /// </summary>
        /// <param name="own_point"></param>
        /// <param name="final_destination"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public Direction_def return_short_direction(Point own_point, Point final_destination, pacman_map map)
        {
            //foreachでノードのリストを見ながら追加していく。パックマンの座標と同じところだった場合そこで終了。そこから親ノードをたどってパスを生成。最終的に方向を決める
            Direction_def return_direction = Direction_def.up;//返す方向
            List<point_info> checked_list = new List<point_info>();//行ったことあるノードを保持
            Queue<point_info> search_task = new Queue<point_info>();//次に見る座標リスト            


            point_info first_info = new point_info(own_point, own_point);//この親ノードはNULLが入らないからとりあえず入れた                                                                                   
            checked_list.Add(first_info);
            Point check_point = first_info.current_point;//次からデキューしたやつをここに入れて使う感じ

            bool point_exist = false;//goneリストに入っていたらtrueにする。falseであれば、子ノードとして追加。
            while (final_destination != check_point)//上下左右の４通りの座標を子ノードとして追加。パックマンの座標が見つかったら探索終了
            {
                if (map.get_map_data()[check_point.Y + 1][check_point.X] != 2)//壁じゃなければ子ノードとして追加
                {
                    point_exist = false;
                    Point current_point = new Point(check_point.X, check_point.Y + 1);//子ノードの自分の座標
                    foreach (point_info i in checked_list)//がぶっているかをチェック。
                    {
                        if (i.current_point == current_point)
                        {
                            point_exist = true;
                        }
                    }
                    if (point_exist == false)
                    {
                        point_info children_point = new point_info(current_point, check_point);
                        search_task.Enqueue(children_point);
                    }

                }
                if (map.get_map_data()[check_point.Y - 1][check_point.X] != 2)
                {
                    point_exist = false;
                    Point current_point = new Point(check_point.X, check_point.Y - 1);//子ノードの自分の座標
                    foreach (point_info i in checked_list)//がぶっているかをチェック。
                    {
                        if (i.current_point == current_point)
                        {
                            point_exist = true;
                        }
                    }
                    if (point_exist == false)
                    {
                        point_info children_point = new point_info(current_point, check_point);
                        search_task.Enqueue(children_point);
                    }
                }
                if (map.get_map_data()[check_point.Y][check_point.X + 1] != 2)
                {
                    point_exist = false;
                    Point current_point = new Point(check_point.X + 1, check_point.Y);//子ノードの自分の座標
                    foreach (point_info i in checked_list)//がぶっているかをチェック。
                    {
                        if (i.current_point == current_point)
                        {
                            point_exist = true;
                        }
                    }
                    if (point_exist == false)
                    {
                        point_info children_point = new point_info(current_point, check_point);
                        search_task.Enqueue(children_point);
                    }
                }
                if (map.get_map_data()[check_point.Y][check_point.X - 1] != 2)
                {
                    point_exist = false;
                    Point current_point = new Point(check_point.X - 1, check_point.Y);//子ノードの自分の座標
                    foreach (point_info i in checked_list)//がぶっているかをチェック。
                    {
                        if (i.current_point == current_point)
                        {
                            point_exist = true;
                        }
                    }
                    if (point_exist == false)
                    {
                        point_info children_point = new point_info(current_point, check_point);
                        search_task.Enqueue(children_point);
                    }
                }
                checked_list.Add(search_task.Dequeue());
                check_point = checked_list[checked_list.Count - 1].current_point;
            }

            //親ノードをたどっていき目的座標までの最短パスを作成
            List<Point> short_path = new List<Point>();//最短パス
            short_path.Add(final_destination);
            check_point = final_destination;//たどっていく座標をここに代入していく
            while (check_point != own_point)
            {
                foreach (point_info i in checked_list)
                {
                    if (i.current_point == check_point)
                    {
                        short_path.Add(i.parent_point);//親ノードを追加
                        check_point = i.parent_point;//次にたどるノードをセット
                    }
                }
            }

            //次に進む座標から、進むべき方向を取得
            Point next_direction = new Point(own_point.X - short_path[short_path.Count - 2].X, own_point.Y - short_path[short_path.Count - 2].Y);//short_pathの末尾が次に進むべき座標
            if (next_direction.Y == 1)
            {
                return_direction = Direction_def.up;
            }
            else if (next_direction.Y < 0)
            {
                return_direction = Direction_def.down;
            }
            else if (next_direction.X == 1)
            {
                return_direction = Direction_def.left;
            }
            else
            {
                return_direction = Direction_def.right;
            }

            return return_direction;
        }

        /// <summary>
        /// enemy4(グズタ)の移動方向
        /// </summary>
        /// <returns>次の方向</returns>
        public override Direction_def move(pacman_map map)
        {
            Point infront_location = map.Enemy4_location;//一マス前の座標
            Direction_def return_direction = Direction_def.up;//現時点での返す方向
            Direction_def reverse_direction = Direction_def.up;//進行方向と逆向き
            Point own_left = new Point(0, 0);//自分から見て左
            Point own_right = new Point(0, 0);//自分から見て右
            List<Direction_def> direction_list = new List<Direction_def>();//進行方向の優先順位を決める


            switch (map.Enemy4_direction)//目の前の座標の取得と、進行方向と逆向きの取得
            {
                case pacman_map.map_direction_def.stop:
                case pacman_map.map_direction_def.up:
                    infront_location.Y++;
                    reverse_direction = Direction_def.down;
                    own_left = new Point(map.Enemy4_location.X - 1, map.Enemy4_location.Y);
                    own_right = new Point(map.Enemy4_location.X + 1, map.Enemy4_location.Y);
                    break;
                case pacman_map.map_direction_def.down:
                    infront_location.Y--;
                    reverse_direction = Direction_def.up;
                    own_left = new Point(map.Enemy4_location.X + 1, map.Enemy4_location.Y);
                    own_right = new Point(map.Enemy4_location.X - 1, map.Enemy4_location.Y);
                    break;
                case pacman_map.map_direction_def.right:
                    infront_location.X++;
                    reverse_direction = Direction_def.left;
                    own_left = new Point(map.Enemy4_location.X, map.Enemy4_location.Y - 1);
                    own_right = new Point(map.Enemy4_location.X, map.Enemy4_location.Y + 1);
                    break;
                case pacman_map.map_direction_def.left:
                    infront_location.X--;
                    reverse_direction = Direction_def.right;
                    own_left = new Point(map.Enemy4_location.X, map.Enemy4_location.Y + 1);
                    own_right = new Point(map.Enemy4_location.X, map.Enemy4_location.Y - 1);
                    break;
            }

            if (map.Enemy4_location == new Point(9, 9) || map.Enemy4_location == new Point(9, 10))//真ん中の座標にいるとき
            {
                return_direction = Direction_def.up;
            }
            else
            {

                //分岐点にいる場合
                if (map.get_map_data()[own_left.Y][own_left.X] != 2 || map.get_map_data()[own_right.Y][own_right.X] != 2)
                {

                    if (this.status == status_def.nomal)//通常時(パックマンとの相対距離を計算し、直線距離が８以上なら追跡。８未満ならランダム)
                    {
                        //パックマンとの直線距離
                        double packman_distance = Math.Sqrt((map.Pacman_location.X - map.Enemy1_location.X) * (map.Pacman_location.X - map.Enemy1_location.X) + (map.Pacman_location.Y - map.Enemy1_location.Y) * (map.Pacman_location.Y - map.Enemy1_location.Y));

                        if (packman_distance >= 8)//距離が８以上
                        {
                            return return_short_direction(map.Enemy4_location, map.Pacman_location, map);//最短距離を探索
                        }
                        else//距離が８未満
                        {
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
                                            if (map.get_map_data()[map.Enemy4_location.Y - 1][map.Enemy4_location.X] != 2)
                                            {
                                                finish_roop = true;
                                            }
                                            break;
                                        case Direction_def.down:
                                            if (map.get_map_data()[map.Enemy4_location.Y + 1][map.Enemy4_location.X] != 2)
                                            {
                                                finish_roop = true;
                                            }
                                            break;
                                        case Direction_def.right:
                                            if (map.get_map_data()[map.Enemy4_location.Y][map.Enemy4_location.X+1] != 2)
                                            {
                                                finish_roop = true;
                                            }
                                            break;
                                        case Direction_def.left:
                                            if (map.get_map_data()[map.Enemy4_location.Y][map.Enemy4_location.X - 1] != 2)
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
                        }
                    }



                    else if (this.status == status_def.ijike)//いじけ状態のとき
                    {
                        if (map.Enemy4_location.Y == 4 && map.Enemy4_location.X == 14)//左下の真ん中（設定した目的地）を通るとき、ランダムに移動
                        {
                            return_direction = current_direction;
                        }
                        else//基本的に左下を徘徊する。決まった目的地はないため、４方向に優先順位を与えるアルゴリズムをとる。
                        {
                            Point destination_distance = new Point(4 - map.Enemy4_location.X, 16 - map.Enemy4_location.Y);//左下の座標（左下を徘徊するために設定した目的地）と自分の距離
                            if (System.Math.Abs(destination_distance.X) > System.Math.Abs(destination_distance.Y))//目的地にｙ軸のほうが近い場合(このときｙ、ｘ、ｙ、ｘ、の順番が優先順位になる)
                            {
                                if (destination_distance.Y > 0)
                                {
                                    direction_list.Add(Direction_def.down);
                                    if (destination_distance.X > 0)
                                    {
                                        direction_list.Add(Direction_def.right);
                                        direction_list.Add(Direction_def.up);
                                        direction_list.Add(Direction_def.left);
                                    }
                                    else
                                    {
                                        direction_list.Add(Direction_def.left);
                                        direction_list.Add(Direction_def.up);
                                        direction_list.Add(Direction_def.right);
                                    }
                                }
                                else
                                {
                                    direction_list.Add(Direction_def.up);
                                    if (destination_distance.X > 0)
                                    {
                                        direction_list.Add(Direction_def.right);
                                        direction_list.Add(Direction_def.down);
                                        direction_list.Add(Direction_def.left);
                                    }
                                    else
                                    {
                                        direction_list.Add(Direction_def.left);
                                        direction_list.Add(Direction_def.down);
                                        direction_list.Add(Direction_def.right);
                                    }
                                }
                            }
                            else//パックマンにx軸のほうが近い場合
                            {
                                if (destination_distance.X > 0)
                                {
                                    direction_list.Add(Direction_def.right);
                                    if (destination_distance.Y > 0)
                                    {
                                        direction_list.Add(Direction_def.down);
                                        direction_list.Add(Direction_def.left);
                                        direction_list.Add(Direction_def.up);
                                    }
                                    else
                                    {
                                        direction_list.Add(Direction_def.up);
                                        direction_list.Add(Direction_def.left);
                                        direction_list.Add(Direction_def.down);
                                    }
                                }
                                else
                                {
                                    direction_list.Add(Direction_def.left);
                                    if (destination_distance.Y > 0)
                                    {
                                        direction_list.Add(Direction_def.up);
                                        direction_list.Add(Direction_def.right);
                                        direction_list.Add(Direction_def.down);
                                    }
                                    else
                                    {
                                        direction_list.Add(Direction_def.down);
                                        direction_list.Add(Direction_def.right);
                                        direction_list.Add(Direction_def.up);
                                    }
                                }
                            }

                            for (int i = 0; i < 4; i++)
                            {
                                if (direction_list[i] == reverse_direction)//進行方向と逆向きの候補は削除
                                {
                                    direction_list.RemoveAt(i);
                                }
                            }

                            //この時点で、進行方向の逆向き以外の3つの候補の優先順位を保持
                            for (int i = 0; i < 3; i++)//壁ないか優先順位順にみていく、なかったらそれで進行方向を決定
                            {
                                if (direction_list[i] == Direction_def.down)
                                {
                                    if (map.get_map_data()[map.Enemy4_location.Y + 1][map.Enemy4_location.X] != 2)
                                    {
                                        return_direction = Direction_def.down;
                                        break;
                                    }
                                }
                                else if (direction_list[i] == Direction_def.up)
                                {
                                    if (map.get_map_data()[map.Enemy4_location.Y - 1][map.Enemy4_location.X] != 2)
                                    {
                                        return_direction = Direction_def.up;
                                        break;
                                    }
                                }
                                else if (direction_list[i] == Direction_def.right)
                                {
                                    if (map.get_map_data()[map.Enemy4_location.Y][map.Enemy4_location.X + 1] != 2)
                                    {
                                        return_direction = Direction_def.right;
                                        break;
                                    }
                                }
                                else if (direction_list[i] == Direction_def.left)
                                {
                                    if (map.get_map_data()[map.Enemy4_location.Y][map.Enemy4_location.X - 1] != 2)
                                    {
                                        return_direction = Direction_def.left;
                                        break;
                                    }
                                }

                            }
                        }
                    }



                    else//弱体化中
                    {
                        //ランダムに移動
                        Random random_number = new System.Random();
                        bool finish_roop = false;
                        while (true)
                        {
                            // 0 以上 512 未満の乱数を取得する
                            int direction_number = random_number.Next(3);
                            if (direction_number == 0)
                            {
                                return_direction = Direction_def.up;
                            }
                            else if (direction_number == 1)
                            {
                                return_direction = Direction_def.down;
                            }
                            else if (direction_number == 1)
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
                                        if (map.get_map_data()[map.Enemy4_location.Y - 1][map.Enemy4_location.X] != 2)
                                        {
                                            finish_roop = true;
                                        }
                                        break;
                                    case Direction_def.down:
                                        if (map.get_map_data()[map.Enemy4_location.Y + 1][map.Enemy4_location.X] != 2)
                                        {
                                            finish_roop = true;
                                        }
                                        break;
                                    case Direction_def.right:
                                        if (map.get_map_data()[map.Enemy4_location.Y][map.Enemy4_location.X + 1] != 2)
                                        {
                                            finish_roop = true;
                                        }
                                        break;
                                    case Direction_def.left:
                                        if (map.get_map_data()[map.Enemy4_location.Y][map.Enemy4_location.X - 1] != 2)
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

                    }
                }
                else//曲がれるとこがない場合
                {

                    switch (map.Enemy4_direction)//目の前の座標の取得と、進行方向と逆向きの取得
                    {
                        case pacman_map.map_direction_def.stop:
                        case pacman_map.map_direction_def.up:
                            return_direction = Direction_def.up;
                            break;
                        case pacman_map.map_direction_def.down:
                            return_direction = Direction_def.down;
                            break;
                        case pacman_map.map_direction_def.right:
                            return_direction = Direction_def.right;
                            break;
                        case pacman_map.map_direction_def.left:
                            return_direction = Direction_def.left;
                            break;
                    }
                }
            }
            return return_direction;//返り値
        }
    }

}