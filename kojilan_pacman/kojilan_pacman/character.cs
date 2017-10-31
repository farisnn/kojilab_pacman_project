using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace kojilan_pacman
{
    public class character
    {
        //キャラクター全てに共通の要素はこっちに書いておきましょう。

        //各キャラの座標
        //移動前
        protected int beforeX;
        protected int beforeY;

        protected Point before;
        //移動後
        protected int afterX;
        protected int afterY;

        protected Point after;


        //方向をいちいちintで表現するのもアレなので列挙型を使って見ることにしました（仕組み的には配列と同じようなアクセスも出来るみたいです）
        protected enum Direction {up,down,left,right};

        protected Direction after_direction;


        void get_direction()
        {

            //列挙型をこんな風に使う。（一応こうやって移動方向を指定すれば良いのではないかという例）
            //本当はこのメソッドはキャラによって実装が違うはずなので抽象メソッドにして後からオーバーライドしてもらう。
            //after_direction = Direction.up;
        }
        


    }


    
    interface enemy
    {
        
        //敵のみに固有な情報を書いていく（状態とか）


    }

    public class pacman : character
    {
        //パックマンに固有の情報はこっちに

        public pacman()
        {


            
        }
            
        

    }

    public class enemy1 : character,enemy 
    {
        
    }

    public class enemy2 : character,enemy
    {


    }

}