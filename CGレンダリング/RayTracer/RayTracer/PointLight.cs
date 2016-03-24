using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    class PointLight
    {
        protected Vector3D pos;		//光源位置
        protected double ii;		//強度

        //コンストラクタ
        public PointLight()
        {
            pos = new Vector3D(0, 0, 0);
            ii = 0;
        }

        //データが書かれた文字列で初期化
        //点光源データフォーマット:x y z ii
        public PointLight(String data)
        {
            try
            {
                pos = new Vector3D(0, 0, 0);

                //点光源データフォーマット(X Y Z Ii)
                string[] delimiter = { " " };
                string[] s = data.Split(delimiter, StringSplitOptions.None);//スペースごとに文字列分割

                if (s.Length >= 4)
                {								//入力された数が4個以上なら読み込み
                    pos.x = Double.Parse(s[0]);    		//位置、半径
                    pos.y = Double.Parse(s[1]);
                    pos.z = Double.Parse(s[2]);
                    ii = Double.Parse(s[3]);
                }
                else
                    throw new ArgumentException("[plight]入力値の個数が足りません:" + data);
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        //位置
        public Vector3D getPosition()
        {
            return pos;
        }

        //強度
        public double getIntensity()
        {
            return ii;
        }
    }
}
