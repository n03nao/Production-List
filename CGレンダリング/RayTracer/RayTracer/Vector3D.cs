using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    //ベクトルクラス
    class Vector3D
    {
        public double x, y, z;

        //コンストラクタ
        public Vector3D()
        {
            x = 0.0;
            y = 0.0;
            z = 0.0;
        }

        //コピーコンストラクタ
        public Vector3D(Vector3D v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        //値初期化可能コンストラクタ
        public Vector3D(double X, double Y, double Z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        //vの内容を自分にコピー
        public void copy(Vector3D v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        //値をセット
        public void setVector(double X, double Y, double Z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        //正規化
        static public Vector3D normalize(Vector3D v)
        {
            double leng = Vector3D.length(v);
            Vector3D tmp = new Vector3D(v);

            tmp.x /= leng;
            tmp.y /= leng;
            tmp.z /= leng;

            return tmp;
        }

        //長さを求める
        static public double length(Vector3D v)
        {
            return (Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z));
        }

        //スケーリング
        static public Vector3D scale(double a, Vector3D v)
        {
            Vector3D tmp = new Vector3D(v);
            tmp.x *= a;
            tmp.y *= a;
            tmp.z *= a;

            return tmp;
        }

        //ベクトル足し算
        static public Vector3D add(Vector3D a, Vector3D b)
        {
            Vector3D v = new Vector3D();

            v.x = a.x + b.x;
            v.y = a.y + b.y;
            v.z = a.z + b.z;

            return v;
        }

        //ベクトル引き算
        static public Vector3D sub(Vector3D a, Vector3D b)
        {
            Vector3D v = new Vector3D();

            v.x = a.x - b.x;
            v.y = a.y - b.y;
            v.z = a.z - b.z;

            return v;
        }

        //内積
        static public double dotProduct(Vector3D a, Vector3D b)
        {
            return (a.x * b.x + a.y * b.y + a.z * b.z);
        }

        //外積
        static public Vector3D crossProduct(Vector3D a, Vector3D b)
        {
            Vector3D v = new Vector3D();

            v.x = a.y * b.z - a.z * b.y;
            v.y = a.z * b.x - a.x * b.z;
            v.z = a.x * b.y - a.y * b.x;

            return v;
        }
    }

}
