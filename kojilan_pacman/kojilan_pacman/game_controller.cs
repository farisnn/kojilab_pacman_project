using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kojilan_pacman
{
    class Game_controller
    {
        //インスタンスを代入する変数
        pacman_map map;
        View view;
        pacman pacman;
        enemy1 enemy1;
        enemy2 enemy2;
        enemy3 enemy3;
        enemy4 enemy4;

        //start_gameに入れたい変数
        public enum Direction { up, down, left, right };
        public Direction after_direction;
        public bool finish_game = false; 

            Game_controller(pacman_map Map,View View, pacman Pacman,enemy1 Enemy1,enemy2 Enemy2,enemy3 Enemy3,enemy4 Enemy4 )//コンストラクタの引数で他クラスのインスタンスを取得
        {
            map = Map;
            view = View;
            pacman = Pacman;
            enemy1 = Enemy1;
            enemy2 = Enemy2;
            enemy3 = Enemy3;
            enemy4 = Enemy4;
        }

        //ほかのインスタンスのメソッドを使うメソッドを切りがいいように作成していく。返り値はbooleanとかにしてmainで判定かな？
        
        public void load_initial_state()//初期画面を描画するメソッド
        {
            view.draw_map(map);
        }



        public void start_game()//初期画面描画後に、ゲームを開始するメソッド
        {

            //ゲーム終了まではwhileをループ
            while (finish_game != true) {
                List<Direction> all_direction = new List<Direction>();//キャラの動く方向を格納するリスト

                //各キャラの動きをリストに追加
                all_direction.Add(pacman.move);
                all_direction.Add(enemy1.move);
                all_direction.Add(enemy2.move);
                all_direction.Add(enemy3.move);
                all_direction.Add(enemy4.move);

                //キャラの動きをマップクラスに渡して、マップデータを更新してもらう.
                pacman_map.update_map_data(all_direction);


                //更新したマップデータに基づいて、画面上に描画
                view.draw_map(map);

                //ゲーム終了の判定（マップ更新時の戻り値でもらっといてもいい）
                finish_game = map.check_finish();//終了しなければfalseのまま
        }

            //ゲーム終了時：スコアを通知してゲーム終了
            map.announce_score();
        }


    }
}
