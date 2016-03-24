using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RayTracer
{
    class Material
    {
        protected double ka;	//環境光係数
        protected double kd;	//拡散反射係数
        protected double ks;	//鏡面反射係数
        protected double n;		//Phong係数
        protected Color color;	//色

        const int NUM_DATA = 7;

        //コンストラクタ
        public Material()
        {
            ka = 0;
            kd = 0;
            ks = 0;
            n = 0;
            color = Color.FromArgb(0, 0, 0);
        }

        //値指定コンストラクタ
        public Material(double ka, double kd, double ks,
                        double n, Color col)
        {
            this.ka = ka;
            this.kd = kd;
            this.ks = ks;
            this.n = n;
            this.color = col;
        }

        //データが書かれた文字列で初期化
        //マテリアルデータフォーマット:r g b ka kd ks n
        public Material(String data)
        {
            int r, g, b;

            try
            {
                //マテリアルデータフォーマット(ka kd ks n color-r g b)
                string[] delimiter = { " " };
                string[] s = data.Split(delimiter, StringSplitOptions.None);//スペースごとに文字列分割
                if (s.Length >= NUM_DATA)
                {						//入力された数が必要個以上なら読み込み
                    r = Int32.Parse(s[0]);
                    g = Int32.Parse(s[1]);
                    b = Int32.Parse(s[2]);

                    color = Color.FromArgb(r, g, b);

                    ka = Double.Parse(s[3]);
                    kd = Double.Parse(s[4]);
                    ks = Double.Parse(s[5]);
                    n = Double.Parse(s[6]);
                }
                else
                    throw new ArgumentException("[Material]入力値の個数が足りません:" + data);
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        //環境光強度
        public double getKa()
        {
            return ka;
        }

        //拡散光強度
        public double getKd()
        {
            return kd;
        }

        //鏡面反射光強度
        public double getKs()
        {
            return ks;
        }

        //Phong係数
        public double getN()
        {
            return n;
        }

        //色
        public Color getColor()
        {
            return color;
        }
    }
}
