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
        public abstract Direction_def move(List<List<int>> map, Point position);
        

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

    public class pacman : character
    {
        //パックマンに固有の情報はこっちに
        public enum status_def {nomal, strong}
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
        public override Direction_def move(List<List<int>> map, Point position)
        {

            

            return Direction_def.left;
        }




    }

    /// <summary>
    /// アカベイ的なものの実装
    /// </summary>
    public class enemy1 : enemy 
    {


        /// <summary>
        /// enemy1の移動方向
        /// </summary>
        /// <returns>次の方向</returns>
        public override Direction_def move(List<List<int>> map, Point position)
        {
            

           /*なんとなくアルゴリズム考える
            *上下の座標を引いて、一番差分の大きい方へ移動
            * 
            * 
            *
           */

            return Direction_def.left;
        }


    }

    public class enemy2 : enemy
    {

        /// <summary>
        /// enemy1の移動方向
        ///探索してみる？
        /// </summary>
        /// <returns>次の方向</returns>
        public override Direction_def move(List<List<int>> map, Point position)
        {



            return Direction_def.left;
        }

    }


    public class enemy3 : enemy
    {

        /// <summary>
        /// enemy1の移動方向
        /// </summary>
        /// <returns>次の方向</returns>
        public override Direction_def move(List<List<int>> map, Point position)
        {


            return Direction_def.left;
        }

    }

    public class enemy4 : enemy
    {

        /// <summary>
        /// enemy1の移動方向
        /// </summary>
        /// <returns>次の方向</returns>
        public override Direction_def move(List<List<int>> map, Point position)
        {


            return Direction_def.left;
        }

    }

}